package board;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.Random;
import java.util.Scanner;
public class RandomGen implements RandomGenerator {
	
	private String path;
	private Scanner plannedNumber;
	private boolean det;
	private static RandomGen randomgen=null;
	
	private RandomGen(String path ,boolean det)
	{
		this.det=det;
		this.path=path+"/random_numbers.txt";
		StartPlannedNumber();
	}
	public static RandomGenerator getRandomGen(String path , boolean det) 
	{
		if(randomgen==null) 
			randomgen=new RandomGen(path,det);
		return randomgen;
	}
	public static RandomGenerator getRandomGen() 
	{
		return randomgen;
	}
	
	private void StartPlannedNumber() 
	{
		try 
		{
			plannedNumber=new Scanner(new File(path));
		}
		catch (FileNotFoundException e)
		{
			e.printStackTrace();
		}
	}
	
	@Override
	public int nextInt(int n) {
		if(!det) 
		{
			return new Random().nextInt(n+1);
		}
		else
		{
			while (plannedNumber.hasNext()) 
			{
				return Integer.parseInt(plannedNumber.next());
			}
			if(!plannedNumber.hasNext()) 
			{
				StartPlannedNumber();
			}
			return nextInt(0);
		}
	}

}
