using Nancy;
using System.Collections.Generic;
using Nancy.ViewEngines.Razor;


namespace PirateShip.Objects
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        return View["index.cshtml"];
      };


      Get["/ships"] = _ =>
      {
        List<Ship> AllShips = Ship.GetAll();
        return View["ships.cshtml", AllShips];
      };
    }
  }
}
