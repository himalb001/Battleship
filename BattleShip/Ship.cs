using System;


namespace Battleship
{

  /// <summary>
  /// Ship Placement
  /// Ship can only be placed horizontally or vertically
  /// </summary>
  public enum ShipPlacement
  {
    HORIZONTAL,
    VERTICAL
  }


  class Ship
  {

    // X/Y Position of the ship.
    // @todo - Coordinates should be moved out of ship
    // as ship doesn't need to be aware of coordinate
    public int x = 0;
    public int y = 0;

    /// <summary>
    /// Ship's length
    /// </summary>
    public int length = 0;

    /// <summary>
    /// Total number of hit that ship has taken
    /// </summary>
    private int totalHit = 0;


    /// <summary>
    /// Ship's Constructor
    /// </summary>
    /// <param name="x"> X Coordinate</param>
    /// <param name="y"> Y Coordinate</param>
    /// <param name="length"> Length of Ship</param>
    public Ship(int x, int y, int length)
    {
      // Bound check
      if (x <= 0 || y <= 0 || length <= 0)
      {
        throw new Exception("Invalid argument. Vertical and horizontal length should be a positive number.");
      }

      this.x = x;
      this.y = y;
      this.length = length;

    }



    /// <summary>
    /// Notifies the ship that it has been hit
    /// </summary>
    public void shipHasBeenHit()
    {

      // Update the number of hit that the ship has taken
      this.totalHit++;
      
    }



    /// <summary>
    /// Tells whether the ship has sunk or not
    /// </summary>
    /// <returns> whether the ship has sunk or not</returns>
    public bool isSunk()
    {


      // Total hit needs to be equal to ship's length for it
      // to sink
      return this.totalHit >= this.length;

    }


    /// <summary>
    /// Gives the remaining number of un-hit cells
    /// </summary>
    /// <returns>Number of cells that hasn't yet been hit</returns>
    public int remainingActiveCells()
    {
      return this.length - this.totalHit;
    }
  }
}