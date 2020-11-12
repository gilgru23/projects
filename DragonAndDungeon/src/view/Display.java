package view;

import java.util.List;
import java.util.Scanner;

import javax.xml.stream.events.Characters;

import board.Board;
import board.ActionReader;

public class Display {
	
	
	public void PrintMessages(List<String> msgs)
	{
		for(String s : msgs)
			System.out.println(s);
	}
	public void PrintStatement(String msg)
	{
			System.out.println(msg);
	}
	public int[] ChoosePlayers(String[] characters, int n,ActionReader actionGen)
	{
		int[] selections =new int[n];
		int i=0;
		Scanner scan= new Scanner(System.in);
		while (i<n) 
		{
			System.out.println("Player #"+(i+1)+", select character:");
			for(String c:characters)
			{
				if(c!=null) 
				{
					System.out.println(c);
				}
			}
			int k = getSelections(characters, scan,actionGen);
			selections[i]=k+1;
			System.out.println("You have selected:");
			System.out.println(characters[k]);
			characters[k]=null;
			i++;
		}
		return selections ;
	}
	private int getSelections(String[] characters, Scanner scan, ActionReader actionGen) {
		if(!actionGen.getDet()) 
		{
			int k=scan.nextInt()-1;
			while(characters.length<=k|k<0)
			{
				System.out.println("Please enter a number between 1 and"+characters.length);
				k=scan.nextInt()-1;
			}
			return k;
		}
		else 
		{
			int k=0;
			try 
			{
				k=Integer.parseInt(actionGen.nextAction())-1;
			}
			catch(NumberFormatException e) {}
			return k;
		}
	}


	public String nextAction() {
		Scanner scan= new Scanner(System.in);
		String input=scan.next().toLowerCase();
		while(!(input.equals("d")|input.equals("e")|input.equals("a")|
				input.equals("s")|input.equals("w")|input.equals("q")))
		{
			System.out.println("You should press either 'w','a','s','d','e' or 'q'");
			input=scan.next().toLowerCase();
		}
		return input;
	}
	public void printBoard(Board board)
	{
		System.out.println(board.toString());
	}
	
}
