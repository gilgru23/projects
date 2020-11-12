package board;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.Random;
import java.util.Scanner;
import view.Display;

public class ActionGen implements ActionReader {

	private boolean det;
	private String path;
	private Scanner plannedAction;
	private static ActionGen actiongen=null;
	private Display disp;
	
	private ActionGen(String path,boolean det,Display disp)
	{
		this.det=det;
		this.path=path+"/user_actions.txt";
		this.disp=disp;
		StartAction();
	}
	public static ActionGen getActionGen(String path,boolean det,Display disp) 
	{
		if(actiongen==null) 
			actiongen=new ActionGen(path, det,disp);
		return actiongen;
	}
	public static ActionGen getActionGen() 
	{
		return actiongen;
	}
	private void StartAction() 
	{
		{
			try 
			{
				plannedAction=new Scanner(new File(path));
			}
			catch (FileNotFoundException e)
			{
				e.printStackTrace();
			}
		}
	}
	public boolean getDet()
	{
		return det;
	}	
	@Override
	public String nextAction() 
	{
		if(det) 
		{
			while (plannedAction.hasNext()) 
			{
				return plannedAction.nextLine();
			}
			if(!plannedAction.hasNext()) 
			{
				StartAction();
			}
			return nextAction();
		}
		else 
		{
			return disp.nextAction();
		}
	}
}
