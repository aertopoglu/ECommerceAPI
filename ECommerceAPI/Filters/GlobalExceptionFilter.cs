using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerceAPI.UI.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new
            {
                Success = false,
                Message = context.Exception.Message
            };


            context.Result = new JsonResult(response)
            {
                StatusCode = 500
            };
           
            context.ExceptionHandled = true;
        }
    }
}
