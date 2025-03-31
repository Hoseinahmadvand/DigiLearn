using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Dicounts._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Dicounts.GetAll;

public class GetAllDiscountCodeQuery : IQuery<List<DiscountCodeDto>>
{
}
class GetAllDiscountCodeQueryHandler : IQueryHandler<GetAllDiscountCodeQuery, List<DiscountCodeDto>>
{
    private readonly QueryContext _context;

    public GetAllDiscountCodeQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<List<DiscountCodeDto>> Handle(GetAllDiscountCodeQuery request, CancellationToken cancellationToken)
    {
        var codes =await _context.DiscountCodes.Select(x => new DiscountCodeDto
        {
            Id = x.Id,
            Code = x.Code,
            CreationDate = x.CreationDate,
            ExpirationDate = x.ExpirationDate,
            CurrentUsed=x.CurrentUsed,
            MaxUsed=x.MaxUsed,
            IsActive=x.IsActive,
            DiscountType=x.Type,
            Value=x.Value

        } ).ToListAsync(cancellationToken); 
        return codes.ToList();
    }
}
