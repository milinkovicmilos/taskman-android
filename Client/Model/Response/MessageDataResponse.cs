namespace Client.Model.Response;

public class MessageDataResponse<T>
{
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}