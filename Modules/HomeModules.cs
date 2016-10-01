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

      Get["pirates/{id}"] = parameters =>
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Pirate SelectedPirate = Pirate.Find(parameters.id);
        List<Ship> PirateShips = SelectedPirate.GetShips();
        List<Ship> AllShips = Ship.GetAll();
        model.Add("pirate", SelectedPirate);
        model.Add("pirateShips", PirateShips);
        model.Add("allShips", AllShips);
        return View["pirate.cshtml", model];
      };

      Get["ships/{id}"] = parameters =>
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Ship SelectedShip = Ship.Find(parameters.id);
        List<Pirate> ShipPirates = SelectedShip.GetPirate();
        List<Pirate> AllPirates = Pirate.GetAll();
        model.Add("ship", SelectedShip);
        model.Add("shipPirates", ShipPirates);
        model.Add("allPirates", AllPirates);
        return View["ship.cshtml", model];
      };

      Post["pirate/add_ship"] = _ =>
      {
        Ship ship = Ship.Find(Request.Form["ship-id"]);
        Pirate pirate = Pirate.Find(Request.Form["pirate-id"]);
        pirate.AddShip(ship);
        return View["index.cshtml"];
      };

      Post["ship/add_pirate"] = _ =>
      {
        Ship ship = Ship.Find(Request.Form["ship-id"]);
        Pirate pirate = Pirate.Find(Request.Form["pirate-id"]);
        ship.AddPirate(pirate);
        return View["index.cshtml"];
      };

      Post["/pirates/delete"] = _ =>
      {
        Pirate.DeleteAll();
        return View["index.cshtml"];
      };

      Get["ship/edit/{id}"] = parameters =>
      {
        Ship SelectedShip = Ship.Find(parameters.id);
        return View["ship_edit.cshtml", SelectedShip];
      };

      Patch["ship/edit/{id}"] = parameters =>
      {
        Ship SelectedShip = Ship.Find(parameters.id);
        SelectedShip.Update(Request.Form["ship-name"]);
        return View["index.cshtml"];
      };

      Get["ship/delete/{id}"] = parameters =>
      {
        Ship SelectedShip = Ship.Find(parameters.id);
        return View["ship_delete.cshtml", SelectedShip];
      };

      Delete["ship/delete/{id}"] = parameters =>
      {
        Ship SelectedShip = Ship.Find(parameters.id);
        SelectedShip.Delete();
        return View["index.cshtml"];
      };

      Post["/ships/delete"] = _ =>
      {
        Ship.DeleteAll();
        return View["index.cshtml"];
      };


    }
  }
}
