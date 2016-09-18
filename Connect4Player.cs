//Filename:  Connect4Player.cs
//Namespace: ConnectFour
//Class: Connect4Player
//Version: 1.2
//Authur: Andrew McGregor (C)
//Date: 18/09/2016

using System;


namespace ConnectFour
{
	public class Connect4Player
	{	
		private int[] winMoves = new int[7]; //to be implemented for performance and computer intelligence
		//set to seven as random
		private string name;
		private int move;
		private char token;
		private int lastPosition;
		private bool real = true;
		
		public Connect4Player(){}
		
		public Connect4Player(string name) {
			
			this.name = name;
			this.setToken(this.name);
			if(name.Equals("ZComputer")){
				this.real = false;
			}
		}
		
		//TODO better performance and greater computer intelligence
		public void setWinMove(int col) {
			bool added = false;
			
			for(int i = 0; i < winMoves.Length && !added; i++){
				if(winMoves[i]==0){
					
					winMoves[i]=col;
					added = true;
				}
			}
		}
		
		//TODO better performance and greater computer intelligence
		public int getWinMove(){
			int winCol = 0;
			return winCol;
		}
		//players selected column
		public void setMove(int movein){
			
			this.move = movein;
		}
		
		public int getMove(){
			
			return this.move;
		}
		//players token to be used in grid
		public char getToken(){
			
			return this.token;
		}
		
		private void setToken(string name){
			this.token = name[0];
		}
		
		//position of token after droped in move column
		public int getLastPosition(){
			
			return this.lastPosition;
		}
		
		public void setLastPosition(int lastPosition){
			this.lastPosition = lastPosition;
		}

		public string getName(){
			
			return this.name;
		}
		
		public bool getReal(){
			
			return this.real;
		}
	}
}