using System.Web.Mvc;

namespace MvcUi.Infrastructure
{
    public interface IUrlFlow
    {
        bool CanGo(string action);
        ActionResult GetRedirect();
    }
}