using System.Collections.Generic;
using System.Data.SqlClient;

using System;


namespace PirateShip.Objects
{
  public class Ship
  {
    private int _id;
    private string _name;


    public Ship(string Name,  int Id = 0 )
    {
      _id = Id;
      _name = Name;

    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }



    public void SetId(int id)
    {
      _id= id;
    }

    public void SetName(string name)
    {
      _name= name;
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
        Ship newShip = new Ship(ShipName, ShipId);
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

      SqlCommand cmd = new SqlCommand("INSERT INTO ships (name) OUTPUT INSERTED.id VALUES (@ShipName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@ShipName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
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

  }
}
