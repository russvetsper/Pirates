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

   Ship testShip = new Ship("blah","long");

   testShip.Save();
   Ship savedShip = Ship.GetAll()[0];

   int result = savedShip.GetId();
   int testId = testShip.GetId();

   Assert.Equal(testId, result);
   }

   [Fact]
  public void Test3_UpdateShipName()
  {
    Ship newShip = new Ship("pearl", "long");
    newShip.Save();
    newShip.Update("sage");
    string result = newShip.GetName();

    Assert.Equal("sage", result);
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


   public void Dispose()
     {
       Ship.DeleteAll();
     }


  }
}
