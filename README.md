# Battleship State Tracker

### Background
Battleship is based on the classic game "Battleship". The code implements a state tracker for a single player.

### Prerequisite 

The code was created and tested on Visual Studio for Mac - version `8.4.1`   

The code is expected run on Visual Studio for Windows without any major changes to the project.

### Installation 

Battleship State tracker doesn't have an interface to run yet. The code can be verified by either implementing the an interface class utilising the `Board` and `Ship` class or by running unit tests to validate classes. 


### Testing

Unit test are created using Xunit. To validate the Battleship state tracker, run all unit test in Visual Studio. There are currently 21 unit tests testing both Board and ship classes.

---

# Project Notes

### Project files and structure

The project contains 2 files

*	`Board.cs`
* 	`Ship.cs`

The board can be thought as a 2D grid and where ships are placed.

### Project Requirements

* **Create a board** - Board constructor allows to create a board with any number of rows and columns.
* **Add a battleship to the board** - `Board.addShip(Ship, ShipPlacement)` function lets players add battleship to the board.
* **Take an “attack” at a given position** `Board.attackOnPosition(x,y)` allows players to take an attack on given position. The function returns a boolean indicating whether the attack was a Hit or a Miss.
* **Return whether the player has lost the game yet** `Board.areAllShipsSunk()` returns a boolean indicating whether all ships have sunk or not. This will indicate whether the player has lost the game or not.


### Technical notes

* For better scalability, a dictionary is used rather than a 2D grid array to represent the board.
* The code is broken down into multiple smaller unit testable function.
* XML comments as well as inline comments are used throughout the code in clean format.
* The Code is very loosly coupled to support testability and extensibility.


###Todos

Currently, the Ship class holds it's x/y coordinates. To further support loose coupling, ship's coordinates should be moved out of Ship's class. 

