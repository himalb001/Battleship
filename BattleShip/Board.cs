using System;
using System.Collections.Generic;

namespace Battleship
{

  class Board
  {
    /// <summary>
    /// Total number of ships on board.
    /// </summary>
    int totalShips = 0;

    /// <summary>
    /// Total number of ships on board that have sunk
    /// </summary>
    int totalShipSunk = 0;


    /// <summary>
    /// A Dictionary is used instead of 2D array representation to represent the
    /// grid
    /// </summary>
    Dictionary<string, Ship> grid = new Dictionary<string, Ship>();

    public int verticalLength = 0;
    public int horizontalLength = 0;



    /// <summary>
    /// Initialises board with horizontal/vertical Length
    /// </summary>
    /// 
    /// <param name="horizontalLength">Horizontal Length of the board</param>
    /// <param name="verticalLength"> Vertical Length of the board</param>
    public Board(int horizontalLength, int verticalLength)
    {
      // Board's length needs to be positive.      
      if (horizontalLength <= 0 || verticalLength <= 0)
      {
        throw new Exception("Invalid argument. Vertical and horizontal " +
          "length should be a positive number. ");
      }

      this.verticalLength = verticalLength;
      this.horizontalLength = horizontalLength;
    }






    /// <summary>
    /// Add ship to the board
    /// </summary>
    /// <param name="ship">Ship with x/y coordinate and length </param>
    /// <param name="placement"> Placement can be horizontal or vertical </param>
    /// <returns> True if ship is added successfully, false if not </returns>
    public bool addShip(Ship ship, ShipPlacement placement)
    {

      int endXCoordinate = ship.x;
      int endYCoordinate = ship.y;

      // Find the end coordinate of the ship depending on it's
      // placement
      switch (placement)
      {
        case ShipPlacement.HORIZONTAL:
          endXCoordinate += (ship.length - 1);
          break;
        case ShipPlacement.VERTICAL:
          endYCoordinate += (ship.length - 1);
          break;
        default:
          throw new Exception("Invalid ship placement");
      }




      // Check whether the end coordinate is out of bound
      // Start coordinate isn't checked here as it's coordinate
      // and length is checked in Ship

      if (endXCoordinate <= this.horizontalLength &&
        endYCoordinate <= this.verticalLength)
      {
        // If coordinates are within board's bound


        // Start by sssuming that Ship can be placed in the coordinate
        bool shipCanBePlaced = true;

        // Check each individual cell and mark shipCanBePlaced as false if
        // there's a ship in any of the coordinate
        switch (placement)
        {
          case ShipPlacement.HORIZONTAL:
            for (int idx = ship.x; idx <= endXCoordinate; idx++)
            {
              if (this.hasShipAtCoordinate(idx, ship.y))
              {
                shipCanBePlaced = false;
                break;
              }
            }
            break;
          case ShipPlacement.VERTICAL:
            for (int idx = ship.y; idx <= endYCoordinate; idx++)
            {
              if (this.hasShipAtCoordinate(ship.x, idx))
              {
                shipCanBePlaced = false;
                break;
              }
            }
            break;
          default:
            // Exception - Indicates that Placement functionality was updated
            // but not yet implemented here
            throw new Exception("Invalid ship placement");
        }





        // If ship can be placed
        if (shipCanBePlaced)
        {


          // Iterate through each cell and assign the ship to those cell.
          switch (placement)
          {
            case ShipPlacement.HORIZONTAL:
              for (int idx = ship.x; idx <= endXCoordinate; idx++)
              {
                this.addShipToCoordinate(ship, idx, ship.y);
              }
              break;
            case ShipPlacement.VERTICAL:
              for (int idx = ship.y; idx <= endYCoordinate; idx++)
              {
                this.addShipToCoordinate(ship, ship.x, idx);
              }
              break;
            default:
              // Exception - Indicates that Placement functionality was updated
              // but not yet implemented here
              throw new Exception("Invalid ship placement");
          }
        }
        else
        {
          //Ship cannot be placed - Notify caller
          return false;
        }

        // Ship is added - increment the number of ship 
        this.totalShips++;

        return true; // Notify caller

      }
      else
      {

        // Ship cannot be placed - Ship is Out of bound
        return false; // Notify caller
      }

    }





    /// <summary>
    /// Tells whether a ship exist at given coordinate
    /// </summary>
    /// <param name="x"> X Coordinate of the board</param>
    /// <param name="y"> Y Coordinate of the board</param>
    /// <returns>True if ship exists at the given coordinate</returns>
    public bool hasShipAtCoordinate(int x, int y)
    {

      string key = getKeyFromPosition(x, y);

      // If dictionary contains anything at the coordinate
      // return true
      if (this.grid.ContainsKey(key))
      {
        return true;
      }
      else
      {
        return false;
      }
    }





    /// <summary>
    /// Adds ship to a coordinate.
    ///
    /// This function doesn't add entire ship to the board.
    /// It assigns individual cell to a ship.
    /// </summary>
    /// <param name="ship">Ship to be added</param>
    /// <param name="x"> X Coordinate </param>
    /// <param name="y"> Y Coordinate </param>
    public void addShipToCoordinate(Ship ship, int x, int y)
    {

      string key = getKeyFromPosition(x, y);

      // If grid doesn't contain anything at the coordinate,
      // then only add the ship
      if (!this.grid.ContainsKey(key)) { }
      {
        this.grid.Add(key, ship);
      }
    }


    /// <summary>
    /// Attack the ship in the grid at given position
    /// </summary>
    /// <param name="x"> X position of attack </param>
    /// <param name="y"> Y position of attack </param>
    /// <returns></returns>
    public bool attackOnPosition(int x, int y)
    {

      string key = getKeyFromPosition(x, y);

      // Check if the dictionary contains value at the coordinate
      if (this.grid.ContainsKey(key))
      {

        // Dictionary contains a value
        Ship currentShip = this.grid[key];

        // Notify the ship that it's been hit
        currentShip.shipHasBeenHit();

        // Remove the ship from grid to avoid multiple hit on same coordinate
        this.grid.Remove(key);


        if (currentShip.isSunk())
        {
          // Increase totalShipSunk if the ship's sunk
          this.totalShipSunk++;
        }

        // Notify the caller that it's a hit
        return true;
      }
      else
      {
        // Notify the caller that it's a miss
        return false;
      }
    }



    /// <summary>
    /// Tells whether the given ship is destroyed
    /// </summary>
    /// <param name="ship"> Ship to query</param>
    /// <returns> True if ship has been destroyed, false if it hasn't</returns>
    public bool isShipSunk(Ship ship)
    {
      if (ship != null)
      {

        // Ask the ship directly
        return ship.isSunk();
      }
      else
      {
        // Ship doesn't exist?
        // We might need an exception here instead
        return false;
      }
    }





    /// <summary>
    /// Tells whether all ships on board are sunk
    /// </summary>
    /// <returns> True if all ships are sunk, false otherwise</returns>
    public bool areAllShipsSunk()
    {

      // If dictionary is empty and ship sunk equals total ship
      // #bugfix totalShipSunk needs to be >0 so blank board doesn't return true
      if (this.grid.Count <= 0 &&
        this.totalShipSunk > 0 &&
        this.totalShipSunk == this.totalShips)
      {
        return true;
      }
      else
      {
        return false;
      }
    }



    /// <summary>
    /// Helper function to generate key for Dictionary
    /// </summary>
    /// <param name="x"> X position</param>
    /// <param name="y">Y position</param>
    /// <returns> Unique String to represent each cell in the grid</returns>
    private string getKeyFromPosition(int x, int y)
    {
      
      // This returns unique string representation for each grid.
      // adding 'x' avoids minor bug where 11,1 and 1,11 may return same result
      return x + "x" + y;
    }
  }
}

