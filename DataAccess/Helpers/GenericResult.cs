namespace DataAccess.Helpers;
public class GenericResult
{
    public GenericResult(bool isSuccess, string msg, object data = null, int response = 200)
    {
        this.isSuccess = isSuccess;
        message = msg;
        responseData = data;
        responseCode = response;
    }
    public bool isSuccess { get; set; }
    public string message { get; set; }
    public object responseData { get; set; }
    public int responseCode { get; set; }
}
