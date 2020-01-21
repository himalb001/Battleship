using System;
using Xunit;
using Battleship;

namespace BattleshipTest
{

  public class BoardTest
  {
    /// <summary>
    /// Normal board initialisation
    /// </summary>
    [Fact]
    public void TestBoardConstructor()
    {
      Board board = new Board(12, 15);
      Assert.Equal(board.horizontalLength, 12);
      Assert.Equal(board.verticalLength, 15);
    }


    /// <summary>
    /// Board initialisation with negative values should throw exception
    /// </summary>
    [Fact]
    public void TestBoardConstructorWithNegativeValue()
    {
      Assert.Throws<Exception>(() => { new Board(-1, 10); });
      Assert.Throws<Exception>(() => { new Board(10, -10); });
      
    }


    /// <summary>
    /// #addShip() should add ship at correct orientation and within bounds
    /// </summary>
    [Fact]
    public void AddShipShouldAddShipWithinBounds()
    {
      Board board = new Board(20, 20);
      Ship ship = new Ship(11,10,5);

      board.addShip(ship, ShipPlacement.VERTICAL);

      
      Assert.True(board.hasShipAtCoordinate(11, 10));
      Assert.True(board.hasShipAtCoordinate(11, 14));
      Assert.False(board.hasShipAtCoordinate(11, 15));
      Assert.False(board.hasShipAtCoordinate(12, 10));
    }


    /// <summary>
    /// #addShip() should work with 1 cell ship at board's bounds
    /// </summary>
    [Fact]
    public void AddShipShouldAddShipAtBoundsWithLength1()
    {
      Board board = new Board(10, 10);
      Ship ship = new Ship(10, 10, 1);

      board.addShip(ship, ShipPlacement.VERTICAL);

      Assert.True(board.hasShipAtCoordinate(10, 10));
      
    }


    /// <summary>
    /// #addShip() should not add any ship if it's length exceeds
    /// board's bounds
    /// </summary>
    [Fact]
    public void AddShipShouldIgnoreTooLongShip()
    {
      Board board = new Board(10, 10);
      Ship ship = new Ship(10, 10, 2);

      board.addShip(ship, ShipPlacement.VERTICAL);

      Assert.False(board.hasShipAtCoordinate(10, 10));
    }



    /// <summary>
    /// #addShip() should ignore ship's addition if another ship
    /// exists on it's path
    /// </summary>
    [Fact]
    public void AddShipShouldIgnoreAddingShipIfAnotherExists()
    {
      Board board = new Board(10, 10);
      Ship shipA = new Ship(5, 3, 5);
      Ship shipB = new Ship(3, 5, 5);

      Assert.True(board.addShip(shipA, ShipPlacement.VERTICAL));
      Assert.False(board.addShip(shipB, ShipPlacement.HORIZONTAL));

      Assert.False(board.hasShipAtCoordinate(3, 5));
    }

    /// <summary>
    /// #addShip() should not add ship with coordinate bigger
    /// than board's bound
    /// </summary>
    [Fact]
    public void AddShipShouldIgnoreOutOfBoundShip()
    {
      Board board = new Board(10, 10);
      Ship ship = new Ship(11,11,5);

      Assert.False(board.addShip(ship, ShipPlacement.VERTICAL));
    }



    /// <summary>
    /// #attackOnPosition() should notify if attack resulted in a HIT
    /// </summary>
    [Fact]
    public void AttackOnPositionShoudNotifyOnHit()
    {
      Board board = new Board(20, 20);
      Ship ship = new Ship(10, 10, 9);

      board.addShip(ship, ShipPlacement.VERTICAL);

      Assert.True(board.attackOnPosition(10, 10));
      Assert.True(board.attackOnPosition(10, 11));
      Assert.True(board.attackOnPosition(10, 13));
      Assert.True(board.attackOnPosition(10, 18));
    }


    /// <summary>
    /// #attackOnPosition() should notify if attack resulted in a MISS
    /// </summary>
    [Fact]
    public void AttackOnPositionShouldNotifyOnMiss()
    {
      Board board = new Board(20, 20);
      Ship ship = new Ship(10, 10, 9);

      board.addShip(ship, ShipPlacement.VERTICAL);

      Assert.False(board.attackOnPosition(10, 9));
      Assert.False(board.attackOnPosition(11, 10));
      Assert.False(board.attackOnPosition(10, 19));
      Assert.False(board.attackOnPosition(19, 18));
    }



    /// <summary>
    /// #attackOnPosition should ignore repeated attack on same coordinate
    /// </summary>
    [Fact]
    public void AttackOnPositionShouldIgnoreRepeatedAttack()
    {
      Board board = new Board(20, 20);
      Ship ship = new Ship(10, 10, 9);

      board.addShip(ship, ShipPlacement.VERTICAL);

      Assert.True(board.attackOnPosition(10, 10));
      Assert.False(board.attackOnPosition(10, 10));
    }


    /// <summary>
    /// #attackOnPosition should ignore negative numbers
    /// </summary>
    [Fact]
    public void AttackOnPositionShouldIgnoreNegativeNumber()
    {
      Board board = new Board(20, 20);
      Ship ship = new Ship(10, 10, 9);

      board.addShip(ship, ShipPlacement.VERTICAL);
      Assert.False(board.attackOnPosition(-1, 10));

    }


    /// <summary>
    /// #areAllShipsSunk() should return true when all ships are sunk
    /// </summary>
    [Fact]
    public void AreAllShipSunkShouldReturnWhenShipsAreSunk()
    {
      Board board = new Board(20, 20);
      Ship shipA = new Ship(10, 10, 2);
      Ship shipB = new Ship(11, 11, 1);

      board.addShip(shipA, ShipPlacement.VERTICAL);
      board.addShip(shipB, ShipPlacement.HORIZONTAL);

      // ships are in [10,10] , [10,11] and [11,11]
      board.attackOnPosition(10, 10);
      Assert.False(board.areAllShipsSunk());

      board.attackOnPosition(10, 11);
      Assert.False(board.areAllShipsSunk());

      board.attackOnPosition(11, 11);
      Assert.True(board.areAllShipsSunk());
    }

    /// <summary>
    /// #areAllShipsSunk() should return false if there are no
    /// ships added on the board
    /// </summary>
    [Fact]
    public void AreAllShipSunkShouldReturnFalseOnBlankBoard()
    {
      Board board = new Board(20, 20);
      Assert.False(board.areAllShipsSunk());

    }

    /// <summary>
    /// #isShipSunk() should return true when a ship is destroyed
    /// </summary>
    [Fact]
    public void isShipSunkShouldReturnTrueWhenShipIsSunk()
    {
      Board board = new Board(10, 10);
      Ship ship = new Ship(6, 6, 2);

      board.addShip(ship, ShipPlacement.VERTICAL);

      board.attackOnPosition(6, 6);
      board.attackOnPosition(6, 7);

      Assert.True(board.isShipSunk(ship));
    }


    /// <summary>
    /// #isShipSunk() should return false when ship hasn't yet been destroyed
    /// </summary>
    [Fact]
    public void AreAllShipSunkShouldReturnFalseWhenShipIsntSunk()
    {
      Board board = new Board(10, 10);
      Ship ship = new Ship(6, 6, 2);

      board.addShip(ship, ShipPlacement.VERTICAL);

      board.attackOnPosition(6, 6);
      board.attackOnPosition(6, 6);
      board.attackOnPosition(6, 6);
      Assert.False(board.isShipSunk(ship));

    }







  }
}
