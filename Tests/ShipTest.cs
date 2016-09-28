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
   public void Test3_UpdateShip()
   {

     string name = "yes";
     Ship testShip = new Ship(name);
     testShip.Save();
     string newName = "no";

     testShip.Update(newName);
     string result = testShip.GetName();

     Assert.Equal(newName, result);
   }

   public void Dispose()
     {
       Ship.DeleteAll();
     }


  }
}
