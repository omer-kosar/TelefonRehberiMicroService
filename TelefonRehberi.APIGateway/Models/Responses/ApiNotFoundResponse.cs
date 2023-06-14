using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelefonRehberi.APIGateway.Models.Responses;

namespace Entities.Responses
{
    public class ApiNotFoundResponse : ApiBaseResponse
    {
        public string Message { get; set; }
        public ApiNotFoundResponse(string message) : base(false)
        {
            Message = message;
        }
    }
}
