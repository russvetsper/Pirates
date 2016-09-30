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
     public void Test3_SavePirate()
     {
       //Arrange
       Pirate testPirate = new Pirate("Joe","Cap");
       testPirate.Save();

       //Act
       List<Pirate> result = Pirate.GetAll();
       List<Pirate> testList = new List<Pirate>{testPirate};

       //Assert
       Assert.Equal(testList, result);
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

    [Fact]
    public void Test5_AddShipToPirate()
    {

      Pirate testPirate = new Pirate("ayaya","cap");
      testPirate.Save();

      Ship testShip = new Ship("grr","small");
      testShip.Save();

      testPirate.AddShip(testShip);

      List<Ship> result = testPirate.GetShips();
      List<Ship> testList = new List<Ship>{testShip};

      Assert.Equal(testList, result);
    }

    [Fact]
      public void Test6_ReturnPiratesOnShip()
      {

        Pirate testPirate = new Pirate("ayaya","cap");
        testPirate.Save();

        Ship testShip1 = new Ship("black","long");
        testShip1.Save();

        Ship testShip2 = new Ship("white","short");
        testShip2.Save();

        testPirate.AddShip(testShip1);
        List<Ship> result = testPirate.GetShips();
        List<Ship> testList = new List<Ship> {testShip1};

        Assert.Equal(testList, result);
      }

      [Fact]
  public void Test7_FindPirate()
  {
    
    Pirate testPirate = new Pirate("ayaya","cap");
    testPirate.Save();

    Pirate foundPirate = Pirate.Find(testPirate.GetId());

    Assert.Equal(testPirate, foundPirate);
  }




    public void Dispose()
    {
      Pirate.DeleteAll();
      Ship.DeleteAll();

    }


  }
}
