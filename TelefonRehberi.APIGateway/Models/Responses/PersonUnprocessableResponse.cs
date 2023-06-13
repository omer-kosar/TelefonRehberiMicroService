using Entities.Responses;

namespace TelefonRehberi.APIGateway.Models.Responses
{
    public class PersonUnprocessableResponse : ApiUnprocessableResponse
    {
        public PersonUnprocessableResponse(string message) : base(message)
        {
        }
    }
}
