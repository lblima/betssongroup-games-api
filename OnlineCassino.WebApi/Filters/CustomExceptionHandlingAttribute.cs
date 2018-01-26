using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace OnlineCassino.WebApi.Filters
{
    public class CustomExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// I used Log4Net, but I also like to use Serilog.
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            log.ErrorFormat("An exception occurred at URI: {0}. Error: {1}", actionExecutedContext.Request.RequestUri.AbsoluteUri, actionExecutedContext.Exception.Message);
            
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