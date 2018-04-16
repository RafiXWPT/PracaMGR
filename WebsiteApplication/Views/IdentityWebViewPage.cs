using System.Web.Mvc;
using WebsiteApplication.CodeBehind.Classess;

namespace WebsiteApplication.Views
{
    public abstract class IdentityWebViewPage<T> : WebViewPage<T>
    {
        public ApplicationPrincipal ApplicationPrincipal => new ApplicationPrincipal(User);
    }

    public abstract class IdentityWebViewPage : IdentityWebViewPage<object> { }
}