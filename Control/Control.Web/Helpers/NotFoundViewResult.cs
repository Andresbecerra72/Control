namespace Control.Web.Helpers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    public class NotFoundViewResult : ViewResult //esta clase permite el direccionamiento a una pagina NOT FOUND predeterminada
    {
        public NotFoundViewResult(string viewName)
        {
            ViewName = viewName;
            StatusCode = (int)HttpStatusCode.NotFound;
        }
    }

}
