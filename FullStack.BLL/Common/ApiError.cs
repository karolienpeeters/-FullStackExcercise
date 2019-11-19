using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Westwind.Utilities;

namespace FullStack.BLL.Common

{
    public class ApiError 
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
        public string Detail { get; set; }
        public object Data { get; set; }

        public IList<ValidationFailure> Errors { get; set; }

        public ApiError(string message )
        {
            this.Message = message;
            IsError = true;
        }

      

       
    }
}
