using OnlineShop.API.Features;

namespace OnlineShop.API.Specification;
public class GetUsersByFilterSpecification : BaseSpecification<User>
{
    public GetUsersByFilterSpecification(
        string? firstName,
        string? lastName,
        string? nationalCode,
        OrderType? orderType,
        int? pageSize,
        int? pageNumber)
    {
        AddCriteria(u => u.IsActive);

        if (!string.IsNullOrWhiteSpace(firstName))
            AddCriteria(u => u.FirstName.Contains(firstName));

        if (!string.IsNullOrWhiteSpace(lastName))
            AddCriteria(u => u.LastName.Contains(lastName));

        if (!string.IsNullOrWhiteSpace(nationalCode))
            AddCriteria(u => u.NationalCode.Contains(nationalCode));

        if (orderType.HasValue)
            AddOrderBy(u => u.Id, orderType.Value);

        if (pageSize.HasValue && pageNumber.HasValue)
            AddPagination(pageSize.Value, pageNumber.Value);
    }
}

