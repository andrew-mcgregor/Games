//Filename:  Connect4Game.cs
//Namespace: ConnectFour
//Class: Connect4Game
//Version: 1.2
//Authur: Andrew McGregor (C)
//Date: 18/09/2016

using System;


namespace ConnectFour
{
    public class Connect4Game 
    {
        static void Main() 
        {
            Console.WriteLine("Hello Welcome to Connect 4!");
			Connect4Player player1 = new Connect4Player(); 
			Connect4Player player2 = new Connect4Player(); 
			int players = 0;
			
			bool check = true;
			while(check){
				Console.WriteLine("How many players will there be? Type 1 or 2?  Or Q to quit.");
				
				string input = Console.ReadLine();

				if(Int32.TryParse(input, out players)){
					
					if (players == 1 ){
						Console.WriteLine("Enter Player one name...");
						player1 = new Connect4Player(Console.ReadLine().ToString());
						
						player2 = new Connect4Player("ZComputer");
						check = false;
					}
					else if (players == 2){
						
						Console.WriteLine("Enter Player one name...");
						player1 = new Connect4Player(Console.ReadLine().ToString());
						Console.WriteLine("Enter Player two name...");
						player2 = new Connect4Player(Console.ReadLine().ToString());
						check = false;						
					}
				}
				else if(input.Equals("Q") || input.Equals("q")) {
					Environment.Exit(0);
				}
			}
			
			Connect4Board gameBoard = new Connect4Board();
			gameBoard.displayBoard();

			bool win = true;
			while(win){

				Console.WriteLine("\nPlayer1 move");
				
				gameBoard.getMove(player1);
				win = !gameBoard.validateWin(player1);
				gameBoard.displayBoard();

				if(win){
					Console.WriteLine("\nPlayer2 move");
					gameBoard.getMove(player2);
					win = !gameBoard.validateWin(player2);
					gameBoard.displayBoard();				
				}
			}
			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
			
        }	
    }
	
}