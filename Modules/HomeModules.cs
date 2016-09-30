using Nancy;
using System.Collections.Generic;
using Nancy.ViewEngines.Razor;

namespace PirateShip
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
    }
  }
}      
