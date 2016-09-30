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
      

      Get["/pirates"] = _ =>
      {
        List<Pirate> AllPirates = Pirate.GetAll();
        return View["pirates.cshtml", AllPirates];
      };


      Get["/ships/new"] = _ =>
      {
       return View["ships_form.cshtml"];
      };

     Post["/ships/new"] = _ =>
      {
       Ship newShip = new Ship(Request.Form["ship-name"], Request.Form["ship-type"]);
       newShip.Save();
       return View["index.cshtml"];
      };


      Get["/pirates/new"] = _ =>
      {
      return View["pirates_form.cshtml"];
      };

      Post["/pirates/new"] = _ =>
      {
        Pirate newPirate = new Pirate(Request.Form["pirate-name"], Request.Form["pirate-rank"]);
        newPirate.Save();
        return View["index.cshtml"];
      };
    }
  }
}
