using CoreModule.Facade.Courses;
using CoreModule.Facade.Orders;
using DigiLearn.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionModule.Domain;
using TransactionModule.Services;
using TransactionModule.Services.DTOs.Commands;
using TransactionModule.Services.ZarinPal;
using UserModule.Core.Commands.Users.Wallet.DecreaseBalance;
using UserModule.Core.Services;
using ZarinPal.Class;

namespace DigiLearn.Web.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IOrderFacade _orderFacade;
        private readonly IZarinPalService _zarinPalService;
        private readonly IUserTransactionService _transactionService;
        private readonly ICourseFacade _courseFacade;
        private readonly ILogger<TransactionController> _logger;
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        private IUserFacade _userFacade;
        public TransactionController(IZarinPalService zarinPalService,
                                     IUserTransactionService transactionService,
                                     ILogger<TransactionController> logger,
                                     ICourseFacade courseFacade,
                                     IOrderFacade orderFacade,
                                     IUserFacade userFacade)
        {
            _zarinPalService = zarinPalService;
            _transactionService = transactionService;
            _logger = logger;
            _courseFacade = courseFacade;
            _orderFacade = orderFacade;

            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
            _userFacade = userFacade;
        }

        [Authorize]
        public async Task<IActionResult> CreateTransaction(CreateTransactionCommand command)
        {
            var order = await _orderFacade.GetCurrentOrder(User.GetUserId());
            if (order == null)
                throw new InvalidDataException("Order Is Null");

            command.PaymentAmount = order.FinallyPrice;
            command.UserId = User.GetUserId();

            // بررسی درگاه پرداخت انتخاب شده
            if (command.PaymentGateway == PaymentGateway.Wallet)
            {
                // دریافت اطلاعات کاربر (نیاز به پیاده‌سازی _userService)
                var user = await _userFacade.GetById(User.GetUserId());
                if (user.Wallet < order.FinallyPrice)
                {
                    TempData["Error"] = "موجودی کیف پول کافی نمی باشد.";
                    return Redirect("/cart");
                }

                await _userFacade.DecreaseWalletBalance(new DecreaseWalletBalanceCommand()
                {
                    Amount=order.FinallyPrice,
                    UserId = User.GetUserId()
                });
                // ایجاد تراکنش جدید
                var transactionId = await _transactionService.CreateTransaction(command);

                // ثبت تراکنش به عنوان پرداخت موفق
                await _transactionService.PaymentSuccess(new TransactionPaymentSuccessCommand
                {
                    TransactionId = transactionId,
                    Authority = "Wallet",
                    CardPen = "",
                    RefId = 0
                });

                // نهایی‌سازی تراکنش (مثلاً نهایی کردن سفارش)
                var transaction = await _transactionService.GetTransactionById(transactionId);
                await FinallyTransaction(transaction);

                TempData["Success"] = true;
                return View("EndTransaction");
            }
            else
            {
                // پردازش پرداخت از طریق زرین پال (همانند کد قبلی)
                command.PaymentGateway = PaymentGateway.ZarinPal;
                var transactionId = await _transactionService.CreateTransaction(command);

                var payment = await _zarinPalService.CreatePaymentRequest(
                     command.PaymentAmount, $"پرداخت تراکنش شماره {transactionId}",
                     $"https://localhost:7161/Transaction/Zarinpal-Verify/{transactionId}");

                if (payment?.Status == 100)
                {
                    return Redirect(payment.GateWayUrl);
                }
                return Redirect("/");
            }
        }


        [Route("/Transaction/Zarinpal-Verify/{id}")]
        [Authorize]
        public async Task<IActionResult> ZarinPalVerify(string authority, string status, Guid id)
        {
            if (string.IsNullOrWhiteSpace(status) || status.ToLower() == "nok" || string.IsNullOrWhiteSpace(authority))
            {
                await PayError(new TransactionPaymentErrorCommand()
                {
                    Authority = authority,
                    RefId = 0,
                    ErrorMessage = "Payment Canceled Or Canceled With Error - Status = NOK",
                    TransactionId = id,
                    Canceled = true
                });
                return View("EndTransaction");
            }

            var transaction = await _transactionService.GetTransactionById(id);

            if (transaction == null)
                return NotFound();

            if (User.GetUserId() != transaction.UserId)
                return BadRequest();

            if (transaction.Status == TransactionStatus.PaymentSuccess)
                return BadRequest();

            try
            {
                await FinallyTransaction(transaction);

                var result = await _zarinPalService.CreateVerificationRequest(authority, transaction.PaymentAmount);
                if (result.Status == 100)
                {
                    TempData["Success"] = true;
                    await _transactionService.PaymentSuccess(new TransactionPaymentSuccessCommand()
                    {
                        Authority = authority,
                        CardPen = result.CardPan,
                        RefId = result.RefId,
                        TransactionId = id
                    });
                    await FinallyTransaction(transaction);
                }
                else
                {
                    await PayError(new TransactionPaymentErrorCommand
                    {
                        RefId = result.RefId,
                        TransactionId = id,
                        ErrorMessage = result.Message,
                        Authority = authority
                    });
                }
            }
            catch (Exception e)
            {
                await PayError(e, id, authority);
            }
            return View("EndTransaction");
        }

        #region Utilities

        private async Task FinallyTransaction(UserTransaction transaction)
        {
            switch (transaction.TransactionFor)
            {

                case TransactionFor.CourseOrder:
                    {
                        var order = await _orderFacade.GetCurrentOrder(transaction.UserId);
                        if (order.FinallyPrice != transaction.PaymentAmount)
                            throw new InvalidDataException("اطلاعات پرداخت با اطلاعات سفارش همخوانی ندارند");

                        await _orderFacade.FinallyOrder(order.Id);
                        foreach (var item in order.OrderItems)
                        {
                            await _courseFacade.AddStudent(item.CourseId, order.UserId);
                        };
                        break;
                    }

            }
        }

        private async Task PayError(Exception e, Guid id, string authority)
        {
            TempData["Error"] = true;
            await _transactionService.PaymentError(new TransactionPaymentErrorCommand
            {
                RefId = 0,
                TransactionId = id,
                ErrorMessage = e.Message,
                Authority = authority
            });
            _logger.LogError(e.Message, e);
        }
        private async Task PayError(TransactionPaymentErrorCommand command)
        {
            TempData["Error"] = true;
            await _transactionService.PaymentError(command);
        }

        //private async Task AddCourseStudent(CourseOrder order)
        //{
        //    foreach (var detail in order.CourseOrderDetails)
        //    {
        //        await _courseFacade.AddCourseStudent(
        //            new AddStudentCommand(order.UserId, detail.CourseId, true));
        //    }
        //}
        #endregion
    }
}
