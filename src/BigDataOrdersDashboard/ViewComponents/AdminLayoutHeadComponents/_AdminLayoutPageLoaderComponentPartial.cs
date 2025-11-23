using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.ViewComponents.AdminLayoutHeadComponents;

public class _AdminLayoutPageLoaderComponentPartial:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }

}
