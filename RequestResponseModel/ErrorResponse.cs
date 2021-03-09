using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsStacks.RequestResponseModel
{
    public class ErrorResponse
    {
        public string errorMessage { get; set; }
        public string stackTrace { get; set; }

    }
}
