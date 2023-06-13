using Entities.Responses;

namespace TelefonRehberi.APIGateway.Models.Responses
{
    public class PersonNotFoundResponse:ApiNotFoundResponse
    {
        public PersonNotFoundResponse(string message) : base(message)
        {

        }
    }
}
