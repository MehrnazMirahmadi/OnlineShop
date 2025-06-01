namespace OnlineShop.API.Features;
public class PaginationResult<T>(int pageSize, int pageNumber, int totalCount, List<T> data)
{
    public int PageSize { get; init; } = pageSize;
    public int PageNumber { get; init; } = pageNumber;
    public int TotalCount { get; init; } = totalCount;
    public List<T> Data { get; init; } = data;

    public static PaginationResult<T> Create(int pageSize, int pageNumber, int totalCount, List<T> data)
    {
        return new PaginationResult<T>(pageSize, pageNumber, totalCount, data);
    }
}

