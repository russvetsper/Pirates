using System;
using Xunit;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PirateShip;
using PirateShip.Objects;

namespace PirateShip
{
  public class ShipTest : IDisposable
  {
    public ShipTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=pirates_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test1_DataBaseEmpty_True()
    {
      int table = Ship.GetAll().Count;

      Assert.Equal(0, table);
    }

    [Fact]
    public void Test2_Save()
    {

      Ship testShip = new Ship("Dying Gul","long");

      testShip.Save();
      Ship savedShip = Ship.GetAll()[0];

      int result = savedShip.GetId();
      int testId = testShip.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test3_UpdateShipName()
    {
      Ship newShip = new Ship("Black Pearl", "long");
      newShip.Save();
      newShip.Update("flying dutchman");
      string result = newShip.GetName();

      Assert.Equal("flying dutchman", result);
    }

    [Fact]
    public void Test4_DeleteOneShip()
    {
      Ship firstShip = new Ship("sage","long",1);
      firstShip.Save();

      Ship secondShip = new Ship("blue","short",1);
      secondShip.Save();

      firstShip.Delete();
      List<Ship>allShip = Ship.GetAll();
      List<Ship>afterDeleteFirstShip = new List<Ship>{secondShip};

      Assert.Equal(afterDeleteFirstShip,allShip);
    }

    [Fact]
    public void Test5_AddPirateToShip()
    {

      Ship testShip = new Ship("Black Pearl","long");
      testShip.Save();

      Pirate testPirate1 = new Pirate("Jack Sparow","Captain");
      testPirate1.Save();

      Pirate testPirate2 = new Pirate("Jones","Captain");
      testPirate2.Save();


      testShip.AddPirate(testPirate1);
      testShip.AddPirate(testPirate2);

      List<Pirate> result = testShip.GetPirate();
      List<Pirate> testList = new List<Pirate>{testPirate1, testPirate2};


      Assert.Equal(testList, result);
    }


    [Fact]
    public void Test7_FindShip()
    {

      Ship testShip = new Ship("ayaya","cap");
      testShip.Save();

      Ship foundShip = Ship.Find(testShip.GetId());

      Assert.Equal(testShip, foundShip);
    }

    

    public void Dispose()
    {
      Ship.DeleteAll();
      Pirate.DeleteAll();
    }

  }
}
