namespace Auth.Application.Wrappers;

public class ResponseR<T>
{
    public ResponseR()
    {
    }

    public ResponseR(T data, string message = null)
    {
        Status = true;
        Message = message;
        Data = data;
    }

    public ResponseR(bool status, string message)
    {
        Status = status;
        Message = message;
    }

    public bool Status { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}

