using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace OnlineCassino.WebApi.Filters
{
    public class CustomExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //Do some log here...

            if (actionExecutedContext.Exception is System.ApplicationException)
            {
                throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message)
                });
            }

            throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An unexpected error occurred, please contact the Betsson group support")
            });
        }
    }
}