using System;
using Xunit;
using Battleship;

namespace BattleshipTest
{

  public class ShipTest
  {


    /// <summary>
    /// Ship constructor should accept normal values
    /// </summary>
    [Fact]
    public void ShipConstructorShouldAcceptNormalValues()
    {
      Ship ship = new Ship(10, 10, 4);
      ship.shipHasBeenHit();
      Assert.Equal(ship.remainingActiveCells(),3);
    }

    /// <summary>
    /// Ship constructor should throw exception if length is negative
    /// </summary>
    [Fact]
    public void ShipConstructorShouldntAcceptNegativeLength()
    {
      Assert.Throws<Exception>(() => { new Ship(10,10,-14); });      
    }



    /// <summary>
    /// Ship constructor should throw exception if coordinates are negative
    /// </summary>
    [Fact]
    public void ShipConstructorShouldntAcceptNegativeCoordinates()
    {
      Assert.Throws<Exception>(() => { new Ship(0, 10, 4); }); // x = zero
      Assert.Throws<Exception>(() => { new Ship(-10, 10, 4); }); // x = negative
      Assert.Throws<Exception>(() => { new Ship(10, 0, 4); }); // y = 0
      Assert.Throws<Exception>(() => { new Ship(10, -10, 4); }); // y = negative
    }



    /// <summary>
    /// Ship constructor should be independent of board
    /// Ship sohuld still be able to be constructed regardless
    /// of board's size
    /// </summary>
    [Fact]
    public void ShipConstructorShouldBeIndependentOfBoard()
    {
      Board board = new Board(5, 5);
      Ship ship = new Ship(10, 10, 10);
      Assert.Equal(ship.length, 10);
    }

    /// <summary>
    /// Ship should be sunk after all it's active cells are hit
    /// </summary>
    [Fact]
    public void ShipShouldBeSunkWhenAllCellsAreHit()
    {
      Ship ship = new Ship(10, 10, 2);
      ship.shipHasBeenHit();
      Assert.False(ship.isSunk()); // Not yet
      ship.shipHasBeenHit();
      Assert.True(ship.isSunk()); // Should be sunk 
    }


    /// <summary>
    /// Remaining active cells should return correct value once
    /// the ship has been hit
    /// </summary>
    [Fact]
    public void remainingActiveCellsShouldReturnCorrectValue()
    {
      Ship ship = new Ship(10, 10, 4);
      ship.shipHasBeenHit();
      Assert.Equal(ship.remainingActiveCells(), 3);
    }

  }
}