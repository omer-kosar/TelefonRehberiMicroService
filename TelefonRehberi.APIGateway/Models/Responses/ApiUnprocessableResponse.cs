namespace TelefonRehberi.APIGateway.Models.Responses
{
    public class ApiUnprocessableResponse: ApiBaseResponse
    {
        public string Message { get; set; }
        public ApiUnprocessableResponse(string message) : base(false)
        {
            Message = message;
        }
    }
}
