namespace OnlineShop.API.Features;
public class PaginationResult<T>(int pageSize, int pageNumber, int totalCount, List<T> data)
{
    public int PageSize { get; } = pageSize;
    public int PageNumber { get; } = pageNumber;
    public int TotalCount { get; } = totalCount;
    public List<T> Data { get; } = data;

    public static PaginationResult<T> Create(int pageSize, int pageNumber, int totalCount, List<T> data)
        => new(pageSize, pageNumber, totalCount, data);
}


