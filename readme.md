# Noughts and Crosses Game

This is a reusable framework for two-player board games  developed in C# .NET using Visual Studio. It is implemented using inheritance and abstract classes, and the current implementation is for Tic-tac-toe. 
The framework allows for human versus human, computer versus human, and two levels of computer play involving random selection or look-ahead scoring functions.

# Installation
To run this application, you will need to have the following software installed on your computer:

* .NET Framework (version 4.7.2 or later)
* Visual Studio (version 2019 or later)
Once you have these dependencies installed, you can download the source code from this repository and open the project file (TicTacToe.sln) in Visual Studio.

# Features
* Human versus human gameplay
* Computer versus human gameplay with two levels of difficulty: Random and Look-ahead bot
* Human player moves checked for validity
* Alpha-beta algorithm for look-ahead bot
* Game saved and restored from any state of play (except the first 2 moves)
* Moves undoable and replayable
* 2D user interface
* Primitive online help system to assist users with available commands

# Usage
To play the game, simply build and run the application from within Visual Studio. 
The game board will be displayed in the console window, and you will be prompted to enter your moves by specifying the row and column of the cell you want to place your symbol (X or O) in.

The game will continue until one player wins or the game ends in a draw. The application will then prompt you to play again or exit the game.

# Project Structure
This project is structured as follows:

***Program.cs***: contains the main entry point for the application.
***Game.cs***: contains data structures for representing the game board and game play.
***PlayOrder.cs***: contains the game logic and ordering play and players turns.
***Player.cs***: contains the data structure for representing a player.

# Contributing
If you have any suggestions or find any bugs, please feel free to open an issue or submit a pull request. Contributions are always welcome!

# License
This project is licensed under the MIT License. See the LICENSE file for details.
