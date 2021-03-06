using System.Collections.Generic;
using System.Data.SqlClient;
using System;


namespace PirateShip.Objects
{
  public class Ship
  {
    private int _id;
    private string _name;
    private string _shipType;

    public Ship(string Name, string ShipType, int Id = 0 )
    {
      _id = Id;
      _name = Name;
      _shipType= ShipType;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetShipType()
    {
      return _shipType;
    }

    public void SetId(int id)
    {
      _id= id;
    }

    public void SetName(string name)
    {
      _name= name;
    }

    public void SetShipType(string shipType)
    {
      _shipType= shipType;
    }

    public static List<Ship> GetAll()
    {
      List<Ship> allShip = new List<Ship>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      string statement = "SELECT * FROM ships;";
      SqlCommand cmd = new SqlCommand(statement, conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int ShipId = rdr.GetInt32(0);
        string ShipName = rdr.GetString(1);
        string ShipType = rdr.GetString(2);
        Ship newShip = new Ship(ShipName, ShipType, ShipId);
        allShip.Add(newShip);

      }

      if (rdr !=null)
      {
        rdr.Close();
      }
      if (conn !=null)
      {
        conn.Close();
      }

      return allShip;
    }


    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO ships (name, shiptype) OUTPUT INSERTED.id VALUES (@ShipName, @ShipType);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@ShipName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      SqlParameter shiptypeParameter = new SqlParameter();
      shiptypeParameter.ParameterName = "@ShipType";
      shiptypeParameter.Value = this.GetShipType();
      cmd.Parameters.Add(shiptypeParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public override bool Equals(System.Object otherShip)
    {
      if (!(otherShip is Ship))
      {
        return false;
      }
      else
      {
        Ship newShip = (Ship) otherShip;
        bool idEquality = (this.GetId() == newShip.GetId());
        bool nameEquality = (this.GetName() == newShip.GetName());
        bool shipType = (this.GetShipType() == newShip.GetShipType());
        return (idEquality && nameEquality);
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM ships;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public void Update(string Name)
       {
         SqlConnection conn = DB.Connection();
         conn.Open();

         SqlCommand cmd = new SqlCommand("UPDATE ships SET name = @shipName output inserted.name WHERE id = @shipId;", conn);
         SqlParameter ShipNameParameter = new SqlParameter();
         ShipNameParameter.ParameterName = "@shipName";
         ShipNameParameter.Value = Name;

         SqlParameter ShipIdParameter = new SqlParameter();
         ShipIdParameter.ParameterName = "@shipId";
         ShipIdParameter.Value = this.GetId();

         cmd.Parameters.Add(ShipNameParameter);
         cmd.Parameters.Add(ShipIdParameter);

         SqlDataReader rdr = cmd.ExecuteReader();

         while(rdr.Read())
         {
           this._name = rdr.GetString(0);
         }

         if (rdr != null)
         {
           rdr.Close();
         }

         if (rdr != null)
         {
           conn.Close();
         }
       }

       public void Delete()
        {
         SqlConnection conn = DB.Connection();
         conn.Open();

         SqlCommand cmd = new SqlCommand("DELETE FROM ships WHERE id = @ShipId;", conn);

         SqlParameter ShipIdParameter = new SqlParameter();
         ShipIdParameter.ParameterName= "ShipId";
         ShipIdParameter.Value = this.GetId();
         cmd.Parameters.Add(ShipIdParameter);
         cmd.ExecuteNonQuery();

         if (conn !=null)
         {
           conn.Close();
         }
       }

       public void AddPirate(Pirate newPirate)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO pirates_ships (pirates_id, ships_id) VALUES (@PirateId, @ShipId);", conn);
      SqlParameter ShipIdParameter = new SqlParameter();
      ShipIdParameter.ParameterName = "@ShipId";
      ShipIdParameter.Value = this.GetId();
      cmd.Parameters.Add(ShipIdParameter);


      SqlParameter PirateIdParameter = new SqlParameter();
      PirateIdParameter.ParameterName = "@PirateId";
      PirateIdParameter.Value = newPirate.GetId();
      cmd.Parameters.Add(PirateIdParameter);



      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Pirate> GetPirate()
     {
       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("SELECT pirates.* FROM ships JOIN pirates_ships ON (ships.id = pirates_ships.ships_id) JOIN pirates ON (pirates_ships.pirates_id = pirates.id) WHERE ships.id = @ShipsId",conn);

       SqlParameter ShipIdParameter = new SqlParameter();
       ShipIdParameter.ParameterName= "@ShipsId";
       ShipIdParameter.Value = this.GetId();
       cmd.Parameters.Add(ShipIdParameter);

       SqlDataReader rdr = cmd.ExecuteReader();
       List<Pirate> Pirate = new List<Pirate> {};

       while(rdr.Read())
       {
         int PirateId = rdr.GetInt32(0);
         string PirateName = rdr.GetString(1);
         string PirateRank = rdr.GetString(2);
         Pirate newPirate = new Pirate(PirateName,PirateRank,PirateId);
         Pirate.Add(newPirate);
       }

       if(rdr !=null)
       {
         rdr.Close();
       }

       if (conn != null)
       {
         conn.Close();
       }
       return Pirate;
     }

     public static Ship Find(int id)
     {
       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("SELECT * FROM ships WHERE id = @ShipId;", conn);
       SqlParameter shipIdParameter = new SqlParameter();
       shipIdParameter.ParameterName = "@ShipId";
       shipIdParameter.Value = id.ToString();
       cmd.Parameters.Add(shipIdParameter);
       SqlDataReader rdr = cmd.ExecuteReader();

       int foundShipId = 0;
       string foundShipName = null;
       string foundShipType = null;
       while(rdr.Read())
       {
         foundShipId = rdr.GetInt32(0);
         foundShipName = rdr.GetString(1);
         foundShipType = rdr.GetString(2);
       }
       Ship foundShip = new Ship(foundShipName,foundShipType, foundShipId);

       if (rdr != null)
       {
         rdr.Close();
       }
       if (conn != null)
       {
         conn.Close();
       }
       return foundShip;
     }









  }
}
