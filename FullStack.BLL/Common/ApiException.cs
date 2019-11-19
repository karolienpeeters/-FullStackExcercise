using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Westwind.Utilities;

namespace FullStack.BLL.Common
{
    public class ApiException:Exception
    {
        public int StatusCode { get; set; }

        public IList<ValidationFailure> Errors { get; set; }

        public ApiException(string message,
            int statusCode = 500,
            IList<ValidationFailure> errors = null) :
            base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
        public ApiException(Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }
}
