//Gameboard
//Filename:  Connect4Board.cs
//Namespace: ConnectFour
//Class: Connect4Board
//Version: 1.3
//Authur: Andrew McGregor (C)
//Date: 18/09/2016

using System;


namespace ConnectFour
{
	public class Connect4Board 
	{	
		private int boardWidth = 7;
		private int boardHeight = 6;
		private char[][] board = new char[7][];
		private int[] colMax;
		
		public Connect4Board(){
			
			initBoard();
		}

		public void initBoard(){
			
			//each row
			for (int c = 0; c < boardWidth; c++){
			
				board[c] = new char[boardHeight];

				//each column
				for (int r = 0; r < boardHeight; r++){

					board[c][r] = 'O'; 					
				}
				
			}
			colMax = new int[boardWidth];
		}		

		public void displayBoard() {
			
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			//loop though each row starting at highest
			for (int i = boardHeight-1; i >= 0; i--){
				string row = "";

				for (int r = 0; r <= boardWidth-1; r++){
					
					row = row+" | "+board[r][i].ToString();
				}

				Console.WriteLine(" -----------------------------");
				Console.WriteLine(row+" |");
			}
			Console.WriteLine(" -----------------------------");
			Console.WriteLine(" -----------------------------");
			Console.WriteLine(" | 1 | 2 | 3 | 4 | 5 | 6 | 7 |");
			Console.ResetColor();
				
		}

		public void getMove(Connect4Player player){
			
			int dropCol=0;

			string input = "";
			bool check = true;

			while(check){
				if(player.getReal()){

					Console.WriteLine(player.getName()+" Enter move... ");
					input = Console.ReadLine();
					
					//check number  is entered
					if(Int32.TryParse(input, out dropCol)){
						//check number is less than board width and greateer than 0
						if (dropCol <= boardWidth && dropCol > 0){
								
							dropCol--;  //reduce to account for array startingat zero
							if(colMax[dropCol] < boardHeight){ //check that column is not full
				
								//Console.WriteLine("dropCol = "+dropCol+" colMax[dropCol] = "+colMax[dropCol]);
								board[dropCol][colMax[dropCol]] = player.getToken();  //set player token to board
								
								colMax[dropCol]=colMax[dropCol]+1;  //increase max row for column
								player.setLastPosition(colMax[dropCol]);
								dropCol++;	//added on to adjust back to 1-7
								//Console.WriteLine("dropCol = "+dropCol+" colMax[dropCol] = "+player.getLastPosition());
								player.setMove(dropCol); 
								check = false;
							}
						}
					}
				}
				else{
					Console.WriteLine("Computer");
					Random rnd = new Random();
					dropCol = rnd.Next(1,boardHeight); //needs code for if board gets full
					//check = false;
					if(colMax[dropCol] < boardHeight){ //check that column is not full
						
						//Console.WriteLine("dropCol = "+dropCol+" colMax[dropCol] = "+colMax[dropCol]);
						board[dropCol][colMax[dropCol]] = player.getToken();  //set player token to board						
						
						colMax[dropCol]=colMax[dropCol]+1;  //increase max row for column
						player.setLastPosition(colMax[dropCol]);
						dropCol++;	//added on to adjust back to 1-7
						//Console.WriteLine("dropCol = "+dropCol+" colMax[dropCol] = "+player.getLastPosition());
						player.setMove(dropCol); 
						check = false;
					}
				}								
			}
		}
		
		public bool validateWin(Connect4Player player) {
			int tokenCount = 0;
			bool result=false;
			//check win diagonal 1
			//Console.WriteLine("playerlastpos = "+player.getLastPosition()+" | playermove = "+player.getMove());
			if(checkS(player) == 4){
				Console.WriteLine("WINNER");
				result = true;
			}
			
			tokenCount = checkNE(player); //deduct 1 for always having one match by the dropped counter
			//Console.WriteLine("tokenCount="+tokenCount);
			
			if(tokenCount >= 4){
				
				Console.WriteLine("WINNER");
				result = true;
			}
			
			//check 5 to account for the token always resulting in one match
			else if((checkSW(player)+tokenCount) >= 5){
				
				Console.WriteLine("WINNER");
				result = true;
			}
			
			if(result){
				Console.WriteLine("WINNER");
				return result;
			}
			//no win diagonal 1, try 2
			tokenCount = checkNW(player);
			//Console.WriteLine("tokenCount="+tokenCount);
			if(tokenCount >= 4){
				
				Console.WriteLine("WINNER");
				result = true;
			}
			//check 5 to account for the token always resulting in one match
			if((checkSE(player)+tokenCount) >= 5){
				
				Console.WriteLine("WINNER");
				result = true;
			}
			else {
				Console.WriteLine("NOT YET");
			}
			
			if(result){
				Console.WriteLine("WINNER");
				return result;
			}
			
			//no diagonal 2 win, try horizontal
			tokenCount = checkW(player);
			//Console.WriteLine("tokenCount="+tokenCount);
			if(tokenCount >= 4){
				
				Console.WriteLine("WINNER");
				result = true;
			}
			//check 5 to account for the token always resulting in one match
			if((checkE(player)+tokenCount) >= 5){
				
				Console.WriteLine("WINNER");
				result = true;
			}
			else {
				Console.WriteLine("NOT YET");
			}
			
			return result;
		
		}

		private int checkS(Connect4Player player){
			//Console.WriteLine("checkS");
			int count=0;
			int goBack=1;  //account for array starting 0
			bool quit=true;
			//Console.WriteLine("player.getLastPosition()"+player.getLastPosition());

			for(int i = player.getLastPosition(); i >= 0 && quit; i--){
				//Console.WriteLine("goBack="+goBack);
				//Console.WriteLine("i"+i);

				int row  = (player.getLastPosition()-goBack);
				int col = player.getMove()-1;
				//Console.WriteLine("row="+row);
				//Console.WriteLine("i="+i);				
				//Console.WriteLine("player.getMove()="+player.getMove()+" col = "+col);
				//Console.WriteLine("player.getToken()="+player.getToken());
				if(row >= 0 && board[col][row] == player.getToken()){ //check for row going negative due to 
					
					//Console.WriteLine("BFgoBack="+goBack);
					count++;
					goBack++;
					//Console.WriteLine("AFgoBack="+goBack);
					//Console.WriteLine("count="+count);
				}
				else {
					quit=false;
				}
			}

			return count;
		}
		
		private int checkNE(Connect4Player player){
			//Console.WriteLine("checkNE");
			//Console.WriteLine("LP"+player.getLastPosition());
			//Console.WriteLine("LM"+player.getMove());
			
			int row = player.getLastPosition()-1;
			int col = player.getMove()-1;
			bool exit = false;
			int count = 0;
			
			//Console.WriteLine("col="+col);
			//Console.WriteLine("row="+row);
			//Console.WriteLine("count="+count);
			
			while(exit == false && col < boardWidth && row < boardHeight){
			
			//Console.WriteLine("col="+col);
			//Console.WriteLine("row="+row);
			//Console.WriteLine("count="+count);
			//Console.WriteLine("board[col][row]="+board[col][row]);
				if(board[col][row]==player.getToken()){
					
					row++;
					col++;
					count++;
				}
				else{
					
					exit = true;
				}
			}
			
			return count;			
		}
		
		private int checkNW(Connect4Player player){
			//Console.WriteLine("checkNW");
			//Console.WriteLine("LP"+player.getLastPosition());
			//Console.WriteLine("LM"+player.getMove());
			
			int row = player.getLastPosition()-1;
			int col = player.getMove()-1;
			bool exit = false;
			int count = 0;
			
			//Console.WriteLine("col="+col);
			//Console.WriteLine("row="+row);
			//Console.WriteLine("count="+count);
			
			while(exit == false && col >= 0 && row < boardHeight){
			
			//Console.WriteLine("col="+col);
			//Console.WriteLine("row="+row);
			//Console.WriteLine("count="+count);
			//Console.WriteLine("board[col][row]="+board[col][row]);
				if(board[col][row]==player.getToken()){
					
					row++;
					col--;
					count++;
				}
				else{
					
					exit = true;
				}
			}
			
			return count;
		}
		
		private int checkSE(Connect4Player player){
			//Console.WriteLine("checkSE");
			//Console.WriteLine("LP"+player.getLastPosition());
			//Console.WriteLine("LM"+player.getMove());
			
			int row = player.getLastPosition()-1;
			int col = player.getMove()-1;
			bool exit = false;
			int count = 0;
			
			//Console.WriteLine("col="+col);
			//Console.WriteLine("row="+row);
			//Console.WriteLine("count="+count);
			
			while(exit == false && col < boardWidth  && row >= 0){
			
			//Console.WriteLine("col="+col);
			//Console.WriteLine("row="+row);
			//Console.WriteLine("count="+count);
			//Console.WriteLine("board[col][row]="+board[col][row]);
				if(board[col][row]==player.getToken()){
					
					row--;
					col++;
					count++;
				}
				else{
					
					exit = true;
				}
			}

			return count;
		}
		
		private int checkSW(Connect4Player player){
			//Console.WriteLine("checkSW");			
			//Console.WriteLine("LP"+player.getLastPosition());
			//Console.WriteLine("LM"+player.getMove());
			
			int row = player.getLastPosition()-1;
			int col = player.getMove()-1;
			bool exit = false;
			int count = 0;
			
			//Console.WriteLine("col="+col);
			//Console.WriteLine("row="+row);
			//Console.WriteLine("count="+count);
			
			while(exit == false && col >= 0 && row >= 0){
			
			//Console.WriteLine("col="+col);
			//Console.WriteLine("row="+row);
			//Console.WriteLine("count="+count);
			//Console.WriteLine("board[col][row]="+board[col][row]);
				if(board[col][row]==player.getToken()){
					
					row--;
					col--;
					count++;
				}
				else{
					
					exit = true;
				}
			}

			return count;
		}
		
		private int checkW(Connect4Player player){
			//Console.WriteLine("checkSW");			
			//Console.WriteLine("LP"+player.getLastPosition());
			//Console.WriteLine("LM"+player.getMove());
			
			int row = player.getLastPosition()-1;
			int col = player.getMove()-1;
			bool exit = false;
			int count = 0;
			
			//Console.WriteLine("col="+col);
			//Console.WriteLine("row="+row);
			//Console.WriteLine("count="+count);
			
			while(exit == false && col >= 0){
			
			//Console.WriteLine("col="+col);
			//Console.WriteLine("row="+row);
			//Console.WriteLine("count="+count);
			//Console.WriteLine("board[col][row]="+board[col][row]);
				if(board[col][row]==player.getToken()){
					
					col--;
					count++;
				}
				else{
					
					exit = true;
				}
			}

			return count;
		}
		
		private int checkE(Connect4Player player){
			//Console.WriteLine("checkSW");			
			//Console.WriteLine("LP"+player.getLastPosition());
			//Console.WriteLine("LM"+player.getMove());
			
			int row = player.getLastPosition()-1;
			int col = player.getMove()-1;
			bool exit = false;
			int count = 0;
			
			//Console.WriteLine("col="+col);
			//Console.WriteLine("row="+row);
			//Console.WriteLine("count="+count);
			
			while(exit == false && col < boardWidth){
			
			//Console.WriteLine("col="+col);
			//Console.WriteLine("row="+row);
			//Console.WriteLine("count="+count);
			//Console.WriteLine("board[col][row]="+board[col][row]);
				if(board[col][row]==player.getToken()){
					
					col++;
					count++;
				}
				else{
					
					exit = true;
				}
			}

			return count;
		}
	}
}