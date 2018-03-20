using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOS.Presentation.WebAPIs.Filters
{
    public class GlobalExceptionFilter: ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        public GlobalExceptionFilter(
           IHostingEnvironment hostingEnvironment,
           IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }
        public override void OnException(ExceptionContext context)
        {
            if (!_hostingEnvironment.IsDevelopment())
            {
                // work 
                // do nothing
                return;
            }
            

            var response = new ErrorResponseModel()
            {
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace,
                ErrorCode = 500
            };
            context.Result = new ObjectResult(response)
            {
                StatusCode = response.ErrorCode,
                DeclaredType = typeof(ErrorResponseModel)
            };
            WriteLog(response);
        }
        void WriteLog(ErrorResponseModel response)
        {
            System.IO.File.WriteAllText("Log_GlobalException.txt", "Code: "+response.ErrorCode.ToString()
                +"\n"+ "Stack Trace: " + response.StackTrace
                +"\n"+ "Message: " + response.Message);
        }
        public class ErrorResponseModel
        {
            public string Message { get; set; }

            public string StackTrace { get; set; }

            public int ErrorCode { get; set; }
        }
    }
}
