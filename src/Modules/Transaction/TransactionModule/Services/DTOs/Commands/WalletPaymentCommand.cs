namespace TransactionModule.Services.DTOs.Commands
{
    public class WalletPaymentCommand
    {
        public Guid TransactionId { get; set; }
        public int Amount { get; set; }
        public Guid UserId { get; set; }
    }
}