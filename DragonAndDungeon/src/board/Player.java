package board;


public abstract class Player extends GameUnit {
	protected Integer xp;
	protected Integer lvl;
	private Action action;
	public Player(char tile, String name, Integer healthPool,Integer attackPoints,Integer defensePoints, Integer x_pos, Integer y_pos){
		super(tile, name, healthPool, attackPoints,defensePoints, x_pos, y_pos);
		
		xp=0;
		lvl=1;
	}
	public boolean isAlive() 
	{
		return currentHealth>0;
	}
	
	public void setAction(Action newAction)
	{
		action=newAction;
	}
	public String getName() { return name; }
	public int getLevel() {return lvl;}
	
	public void playTurn(Board board) 
	{
		if (action!=null) 
		{
			action.play(board, this);
		}
	}
	
	public String earnXP(int addedXp) 
	{
		String msg="";
		this.xp += addedXp;
		int i=0;
		while(xp>=50*this.lvl) 
		{
			if(i>0)
				msg+=System.lineSeparator();
			msg+=levelUp();
			i++;
		}
		return msg;
	}
	public String levelUp()
	{
		xp=xp-(50*lvl);
		lvl++;
		int heal=10*lvl;
		healthPool=healthPool+heal;
		currentHealth=healthPool;
		int atk=5*lvl;
		attackPoints=attackPoints+atk;
		int def=2*lvl;
		defensePoints=defensePoints+def;
		String msg="";
		int[] abilities = {heal,atk,def};
		msg=levelUpCostum(abilities);
		return msg;
	}
	public void death(Board board)
	{
		board.death(this);
	}
	
	
	
	//----visitor pattern engagment check---
	public boolean tryEngage(GamePiece other) 
	{
		return other.engagedBy(this);
	}
	
	public boolean engagedBy(Enemy enemy) 
	{
		return true;
	}
	public boolean engagedBy(Player player) 
	{
		return false;
	}
	//----------------------------------
	
	
	public abstract void specialAbility(Board board);
	public abstract String levelUpCostum(int[] abilities);
	public abstract void onTick();
	public abstract String getStatus();
	

}
