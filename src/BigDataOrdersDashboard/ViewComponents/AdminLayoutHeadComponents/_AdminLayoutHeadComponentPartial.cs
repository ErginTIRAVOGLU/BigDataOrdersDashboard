using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.Components.AdminLayoutHeadComponents;
    
public class _AdminLayoutHeadComponentPartial:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }

}
