using System.Collections.Generic;
using System.Data.SqlClient;
using System;


namespace PirateShip.Objects
{
  public class Pirate 
  {
    private int _id;
    private string _name;
    private string _rank;

    public Pirate(string name, string rank, int id = 0 )
    {
      _id = id;
      _name = name;
      _rank= rank;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetRank()
    {
      return _rank;
    }

    public void SetId(int id)
    {
      _id= id;
    }

    public void SetName(string name)
    {
      _name= name;
    }

    public void SetRank(string rank)
    {
      _rank= rank;
    }

    public static List<Pirate> GetAll()
    {
      List<Pirate> allPirate = new List<Pirate>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      string statement = "SELECT * FROM pirates;";
      SqlCommand cmd = new SqlCommand(statement, conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int PirateId = rdr.GetInt32(0);
        string PirateName = rdr.GetString(1);
        string Rank = rdr.GetString(2);
        Pirate newPirate = new Pirate(PirateName, Rank, PirateId);
        allPirate.Add(newPirate);

      }

      if (rdr !=null)
      {
        rdr.Close();
      }
      if (conn !=null)
      {
        conn.Close();
      }

      return allPirate;
    }


    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO pirates (name, rank) OUTPUT INSERTED.id VALUES (@PirateName, @Rank);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@PirateName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      SqlParameter rankParameter = new SqlParameter();
      rankParameter.ParameterName = "@Rank";
      rankParameter.Value = this.GetRank();
      cmd.Parameters.Add(rankParameter);

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

    public override bool Equals(System.Object otherPirate)
    {
      if (!(otherPirate is Pirate))
      {
        return false;
      }
      else
      {
        Pirate newPirate = (Pirate) otherPirate;
        bool idEquality = (this.GetId() == newPirate.GetId());
        bool nameEquality = (this.GetName() == newPirate.GetName());
        bool rank = (this.GetRank() == newPirate.GetRank());
        return (idEquality && nameEquality);
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM pirates;", conn);
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

         SqlCommand cmd = new SqlCommand("UPDATE pirates SET name = @pirateName output inserted.name WHERE id = @pirateId;", conn);
         SqlParameter PirateNameParameter = new SqlParameter();
         PirateNameParameter.ParameterName = "@pirateName";
         PirateNameParameter.Value = Name;

         SqlParameter PirateIdParameter = new SqlParameter();
         PirateIdParameter.ParameterName = "@pirateId";
         PirateIdParameter.Value = this.GetId();

         cmd.Parameters.Add(PirateNameParameter);
         cmd.Parameters.Add(PirateIdParameter);

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

         SqlCommand cmd = new SqlCommand("DELETE FROM pirates WHERE id = @PirateId;", conn);

         SqlParameter PirateIdParameter = new SqlParameter();
         PirateIdParameter.ParameterName= "PirateId";
         PirateIdParameter.Value = this.GetId();
         cmd.Parameters.Add(PirateIdParameter);
         cmd.ExecuteNonQuery();

         if (conn !=null)
         {
           conn.Close();
         }
       }




  }
}
