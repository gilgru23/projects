package board;

import static java.util.stream.Collectors.toList;

import java.util.List;

public class Rogue extends Player {
	private Integer cost;
	private Integer currentEnergy;
	public Rogue(Integer cost,char tile, String name, Integer healthPool,Integer attackPoints,Integer defensePoints, Integer x_pos, Integer y_pos)
	{
		super( tile,  name,  healthPool, attackPoints, defensePoints,  x_pos,  y_pos);
		this.cost=cost;
		currentEnergy=100;
	}
	@Override
	public void specialAbility(Board board) {
	   if(currentEnergy<cost)
	   { 
		   board.addMessage("You don't enough energy left to use Fan Of Knives");
	   }
	   else
	   {
		   board.addMessage(name+" cast Fan Of Knives");
		   currentEnergy-=cost;
		   
		   List<Enemy> enemies = board.getEnemies().stream().
					filter((nme) -> range(nme,this)<2).collect(toList());
		   for (Enemy nme: enemies) 
		   {
			    if (!nme.attackers.contains(this))
					nme.attackers.add(this);
			    RandomGenerator random = RandomGen.getRandomGen();			   int atk= attackPoints;
				int def= random.nextInt(nme.defensePoints);
				board.addMessage(nme.name+" rolled "+def+" defense points.");
				int damage = Math.max(atk-def,0);
				board.addMessage(name+" hit "+nme.name+" for "+damage+" ability damage.");
				if(nme.takeDamage(board,damage))
				{
					nme.death(board);
				}
		   }
	   }
		
	}

	@Override
	public String levelUpCostum(int[] abilities) {
		currentEnergy=100;
		int atk=3*lvl;
		attackPoints+=atk;
		abilities[1]+=atk;
		return "Level up: +"+abilities[0]+" Health, +"+abilities[1]+" Attack, +"
		+abilities[2]+" Defense";
	}
	
	@Override
	public void onTick() {
		currentEnergy=Math.min(currentEnergy+10, 100);
	}
	@Override
	public String getStatus() {
		return name+"			Health: "+currentHealth+"		Attack damage: "+attackPoints+"		Defense: "+defensePoints+System.lineSeparator()+
				"	Level: "+lvl+"		Experience: "+xp+"/"+(50*lvl)+"		Energy: "+currentEnergy+"/100";
	}
	

}
