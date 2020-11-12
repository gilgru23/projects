package board;

import java.util.ArrayList;
import java.util.List;

public abstract class Enemy extends GameUnit {
	
	
	protected Integer xpValue;
	protected List<Player> attackers;
	
	
	public Enemy(char tile, String name, Integer healthPool,Integer attackPoints,Integer defensePoints, Integer x_pos, Integer y_pos, Integer xpValue){
		super(tile, name, healthPool, attackPoints,defensePoints, x_pos, y_pos);
		this.xpValue=xpValue;
		this.attackers= new ArrayList();
	}
	
	
	
	public abstract void onTurn(Board b);
	
	public boolean isAlive() 
	{
		return currentHealth>0;
	}
	public String getStatus() 
	{
		return 	name+"			Health: "+currentHealth+"		Attack damage: "+attackPoints+"		Defense: "+defensePoints;
	}
	public void death(Board board)
	{
		// removes this enemy from the map and Enemy_list on Board 
		board.death(this);

		
		//	allocate XP value among attackers
		int share = Math.round(xpValue/attackers.size());
		String receivers ="";
		String[] lvlUp=new String[attackers.size()];
		int i=0;
		for (Player p : attackers) 
		{
			lvlUp[i]=p.earnXP(share);
			receivers+=" "+p.name+" gained "+share+" experience!";
			i++;
		}
		board.addMessage(name+" died."+receivers);
		for(String s:lvlUp) 
		{
			if(s.length()>0)
				board.addMessage(s);
		}
		
	}
	
	//----visitor pattern engagment check---
	
	public boolean tryEngage(GamePiece other) 
	{
		return other.engagedBy(this);
	}
	public boolean engagedBy(Enemy enemy) 
	{
		return false;
	}
	public boolean engagedBy(Player player) 
	{
		if (!attackers.contains(player))
			attackers.add(player);
		return true;
	}
	//----------------------------------------

}
