namespace Catalog.Application.Wrappers;

public class Response<T>
{
    public Response()
    {
    }

    public Response(T data, string message = null)
    {
        Status = true;
        Message = message;
        Data = data;
    }

    public Response(bool status, string message)
    {
        Status = status;
        Message = message;
    }

    public bool Status { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}

