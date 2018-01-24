using Microsoft.AspNet.Identity;

namespace OnlineCassino.WebApi.Providers
{
    public class IdentityProvider : IIdentityProvider
    {
        public string GetUserId()
        {
            return System.Web.HttpContext.Current.User.Identity.GetUserId();
        }
    }
}