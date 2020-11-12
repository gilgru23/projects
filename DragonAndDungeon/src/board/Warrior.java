package board;

public class Warrior extends Player {
	private Integer cooldown;
	private Integer remaining;
	public Warrior(Integer cooldown,char tile, String name, Integer healthPool,Integer attackPoints,Integer defensePoints, Integer x_pos, Integer y_pos)
	{
		super( tile,  name,  healthPool, attackPoints, defensePoints,  x_pos,  y_pos);
		this.cooldown=cooldown;
		remaining=0;
	}
	public String levelUpCostum(int[] abilities)
	{
		remaining=0;
		int heal=5*lvl;
		healthPool+=heal;
		int def=lvl;
		defensePoints+=def;
		abilities[0]+=heal;
		abilities[2]+=def;
		return "Level up: +"+abilities[0]+" Health, +"+abilities[1]+" Attack, +"
		+abilities[2]+" Defense";
	}
	public void specialAbility(Board board)
	{
		if(remaining>0)
		{
			board.addMessage("You need to wait until cool down is over to use special ability");
			}
		else
		{
			remaining=cooldown;
			int heal=currentHealth;
			currentHealth=Math.min(currentHealth+(2*defensePoints), healthPool);
			heal=currentHealth-heal;
			board.addMessage(name+" cast Heal. Healing for "+heal);
		}
	}
	@Override
	public void onTick() {
		{
			if(remaining>0)
				remaining-=1;
		}
		
	}
	@Override
	public String getStatus() {
		return name+"			Health: "+currentHealth+"		Attack damage: "+attackPoints+"		Defense: "+defensePoints+System.lineSeparator()+
				"	Level: "+lvl+"		Experience: "+xp+"/"+(50*lvl)+"		Ability cooldown: "+cooldown+"		Remaining: "+remaining;
	}
}
