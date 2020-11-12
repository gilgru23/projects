package control;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

import view.Display;
import board.Action;
import board.ActionGen;
import board.ActionReader;
import board.Board;
import board.Player;
import board.RandomGen;

public class GameControl {
	
	private Display disp;
	private List<Player> players;
	private List<char [][]> levels;
	private boolean det;
	private ActionReader actionGen;
	
	public GameControl(String path,boolean det) 
	{
		this.det=det;
		this.disp = new Display();
		this.players=null;
		ReadLevels(path);
		RandomGen.getRandomGen(path,det);
		actionGen = ActionGen.getActionGen(path,det,disp);
	}
	public void StartGame()
	{
		players=choosePlayers();
		disp.PrintStatement("Use w/s/a/d to move."+System.lineSeparator()+
							" Use e for special ability or q to pass.");
		int lvl=0;
		boolean gameWon=false;
		Board currLevel=ParseBoard(lvl);
		while(!gameWon && playLevel(currLevel))
		{
			lvl++;
			if(lvl==levels.size())
				gameWon=true;
			else 
			{
				currLevel=ParseBoard(lvl);
			}
		}
		if(gameWon)
		{
			disp.PrintStatement("Last level is finished. YOU WON!");
		}
		else
		{
			disp.PrintStatement("To start a new round please restart the game");
		}
		
	}
	 
	private List<Player> choosePlayers()
	{
		List<Player> newPlayers = new ArrayList<>();
		// View -> ask for # of players
		int n=1; //# of players
		String[] characters= new String[Character.getSize()];
		for (int i=0;i<characters.length;i++) 
		{
			characters[i]=Character.valueOf("Char"+(i+1)).toString();
		}
		int[] selections=disp.ChoosePlayers(characters, n,actionGen);
		for(int i=0;i<selections.length;i++) 
		{
			Character crt= Character.valueOf("Char"+selections[i]);
			newPlayers.add(crt.createPlayer());
		}
		return newPlayers;
	}
	private Board ParseBoard(int i) 
	{
		return new Board(levels.get(i),players);
	}
	private void ReadLevels(String path)
	{
		levels=new ArrayList<>();
		File levelFolder=new File(path);
	    for (File level : levelFolder.listFiles()) {
	    	if(level.getName().toLowerCase().contains("level"))
	    	{
	    	int [] dime=getDime(level);
	    	if(dime[0]>0 & dime[1]>0)
	    	{
	    		char[][] newLevel=new char[dime[0]][dime[1]];
	    		Scanner sc=null;
	    		try
	    		{
	    			sc=new Scanner(level);
	    		}
		    	catch (FileNotFoundException e) 
	    		{
				e.printStackTrace();
	    		}
	    		int i=0;
	    		while(sc!=null && sc.hasNext())
	    		{
	    			newLevel[i]=sc.next().toCharArray();
	    			i++;
	    		}
	    		if(newLevel[0][0]>-1)
	    		{
	    			//levels.add(newLevel);
	    			String levelName=level.getName();
	    			int index=Math.min(levels.size(), Integer.parseInt(levelName.substring(levelName.toLowerCase().indexOf("level")+6,levelName.length()-4)));
	    			levels.add(index, newLevel);
	    		}
	    	}
	       }
	    }

	}
	private int[] getDime(File f)
	{
		int [] dime =new int[2];
		
		Scanner sc=null;
		try {
			 sc=new Scanner(f);
			 while(sc.hasNext())
			 {
				 dime[0]++;
				 dime[1]=sc.next().length();
			 }
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		}
		return dime;
		
	}
	private boolean playLevel(Board currLevel)
	{
		int end=0;
		while(end==0) 
		{
			disp.printBoard(currLevel);
			for (Player p: players) 
			{
				disp.PrintStatement(p.getStatus());
				disp.PrintStatement("Enter next move for "+p.getName()+":");
				String input=actionGen.nextAction();
				Action newAction= Action.valueOfChar(input);
				p.setAction(newAction);
			}
			end=currLevel.playTurn();
			disp.PrintMessages(currLevel.takeMessages());			
		}
		if (end==1)
			disp.PrintStatement("Level is finished. You won!");
		else if (end==-1)
			disp.PrintStatement("You are dead. Game Over!");
		return end==1;
	}
	
	public static void main(String [ ] args)
	{
		GameControl gc=null;
		if(args.length==1)
			{
			 gc=new GameControl(args[0],false);
			}
		else if(args.length>1&&args[1].equals("-D"))
			{
			 gc=new GameControl(args[0],true);
			}
		if(gc!=null)
		{
			gc.StartGame();
		}
		
		
	}
	
}


	
	

