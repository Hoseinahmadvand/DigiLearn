using Common.Application;
using CoreModule.Application.Discounts.Apply;
using CoreModule.Application.Discounts.Create;
using CoreModule.Application.Discounts.Delete;
using CoreModule.Query.Categories._DTOs;
using CoreModule.Query.Categories.GetAll;
using CoreModule.Query.Dicounts._DTOs;
using CoreModule.Query.Dicounts.GetAll;
using MediatR;

namespace CoreModule.Facade.Discounts;

public interface IDiscountFacade
{
    Task<OperationResult> Create(CreateDiscountCodeCommand command);
    Task<OperationResult> Delete(DeleteDiscountCommand command);
    Task<OperationResult> ApplyDicount(ApplyDicountDicountCommand command);

    Task<List<DiscountCodeDto>> GetAll();


}
class DiscountFacade : IDiscountFacade
{
    private readonly IMediator _mediator;

    public DiscountFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> ApplyDicount(ApplyDicountDicountCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Create(CreateDiscountCodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(DeleteDiscountCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<List<DiscountCodeDto>> GetAll()
    {
        return await _mediator.Send(new GetAllDiscountCodeQuery());
    }
}
