namespace TelefonRehberi.APIGateway.Models.Responses
{
    public class ApiBaseResponse
    {
        public bool Success { get; set; }
        protected ApiBaseResponse(bool success) { Success = success; }
    }
}
