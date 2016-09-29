using System;
using Xunit;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PirateShip;
using PirateShip.Objects;

namespace PirateShip
{
  public class PirateTest : IDisposable
  {
    public PirateTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=pirates_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test1_DataBaseEmpty_True()
    {
      int table = Pirate.GetAll().Count;

      Assert.Equal(0, table);
    }

    [Fact]
    public void Test2_Save()
    {

      Pirate testPirate = new Pirate("blah","long");

      testPirate.Save();
      Pirate savedPirate = Pirate.GetAll()[0];

      int result = savedPirate.GetId();
      int testId = testPirate.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test3_UpdatePirateName()
    {
      Pirate newPirate = new Pirate("pearl", "long");
      newPirate.Save();
      newPirate.Update("sage");
      string result = newPirate.GetName();

      Assert.Equal("sage", result);
    }

    [Fact]
    public void Test4_DeleteOnePirate()
    {
      Pirate firstPirate = new Pirate("sage","long");
      firstPirate.Save();

      Pirate secondPirate = new Pirate("blue","short");
      secondPirate.Save();

      firstPirate.Delete();
      List<Pirate>allPirate = Pirate.GetAll();
      List<Pirate>afterDeleteFirstPirate = new List<Pirate>{secondPirate};

      Assert.Equal(afterDeleteFirstPirate,allPirate);
    }


    public void Dispose()
    {
      Pirate.DeleteAll();
    }


  }
}
