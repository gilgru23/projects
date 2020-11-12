package control;

import javax.xml.stream.events.Characters;

import board.Mage;
import board.Player;
import board.Rogue;
import board.Warrior;
import control.GameControl.*;

public enum Character {
	
	Char1 (1, "Jon Snow" ,300,30,4,6,0),
	Char2 (2, "The Hound" ,400,20,6,4,0),
	Char3 (3, "Melisandre", 160,10,1,40,75,300,30,5,6,1),
	Char4 (4, "Thoros of Myr", 250,25,3,15,37,150,50,3,3,1),
	Char5(5, "Arya Stark" ,150,40,2,20,2),
	Char6(6, "Bronn" ,250,35,3,60,2);
	
	int index;
	String name;
	int Health;
	int Attack;
	int Defense;
	int Cooldown;
	int playerType;
	Character(int index, String name, int Health, int Attack, 
				int Defense, int Cooldown, int playerType) 
	{
		this.index=index; this.name=name;
		this.Health=Health; this.Attack=Attack;
		this.Defense=Defense; this.Cooldown=Cooldown;
		this.playerType=playerType; //Warrior / Rouge
	}
	int SpellPower;
	int ManaPool;
	int InitMana;
	int ManaCost;
	int HitTimes;
	int Range;
	Character(int index, String name, int Health, int Attack, 
			int Defense, int SpellPower, int ManaPool, int InitMana,
			int ManaCost,int HitTimes ,int Range , int playerType) 
	{
	this.index=index; this.name=name;
	this.Health=Health; this.Attack=Attack;
	this.Defense=Defense; this.SpellPower=SpellPower;
	this.ManaPool=ManaPool; this.ManaCost=ManaCost;
	this.HitTimes=HitTimes; this.Range=Range;
	this.playerType=playerType; //Mage
}
	public static int getSize() 
	{
		return 6;
	}
	public String toString() 
	{
		if (playerType==0) 
		{
			return index+". "+name+"		Health: "+Health+"		Attack damage: "+Attack+"		Defense: "+Defense+System.lineSeparator()+
					"	Level: 1		Experience: 0/50		Ability cooldown: "+Cooldown+"		Remaining: 0";
		}
		else if (playerType==1) 
		{
			return index+". "+name+"		Health: "+Health+"		Attack damage: "+Attack+"		Defense: "+Defense+System.lineSeparator()+
					"	Level: 1		Experience: 0/50		SpellPower:"+SpellPower+"		Mana: "+(ManaPool/4)+"/"+ManaPool;
		}
		
		else//playerType==2 
		{
			return index+". "+name+"		Health: "+Health+"		Attack damage: "+Attack+"		Defense: "+Defense+System.lineSeparator()+
					"	Level: 1		Experience: 0/50		Energy: 100/100";
		}
	}
	public Player createPlayer() 
	{
		Player player=null;
		if (playerType==0) 
		{
			player= new Warrior(Cooldown,'@',name,Health,Attack,Defense,0, 0);
		}
		else if (playerType==1) 
		{
			player= new Mage(SpellPower,ManaPool,ManaCost,HitTimes,Range,'@',name,Health,Attack,Defense,0,0);
		}
		else //playerType==2  
		{
			player= new Rogue(Cooldown,'@',name,Health,Attack,Defense,0, 0);
		}
		return player;
	}
}
