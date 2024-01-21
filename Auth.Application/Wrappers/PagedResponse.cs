namespace Auth.Application.Wrappers;

public class PagedResponse<T> : ResponseR<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public PagedResponse(T data, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Data = data;
        Message = null;
        Status = true;
    }
}

