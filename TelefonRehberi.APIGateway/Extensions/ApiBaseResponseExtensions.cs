using Entities.Responses;
using TelefonRehberi.APIGateway.Models.Responses;

namespace TelefonRehberi.APIGateway.Models.Exceptions
{
    public static class ApiBaseResponseExtensions
    {
        public static TResultType GetResult<TResultType>(this ApiBaseResponse apiBaseResponse) => ((ApiOkResponse<TResultType>)apiBaseResponse).Result;
    }
}
