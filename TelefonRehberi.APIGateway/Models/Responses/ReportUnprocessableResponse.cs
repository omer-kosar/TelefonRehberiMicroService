using Entities.Responses;

namespace TelefonRehberi.APIGateway.Models.Responses
{
    public class ReportUnprocessableResponse : ApiUnprocessableResponse
    {
        public ReportUnprocessableResponse(string message) : base(message)
        {
        }
    }
}
