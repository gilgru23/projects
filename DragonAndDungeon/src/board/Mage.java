package board;

import static java.util.stream.Collectors.toList;

import java.util.List;

public class Mage extends Player{
	private Integer spellpower;
	private Integer manapool;
	private Integer currentmana;
	private Integer cost;
	private Integer hitTimes;
	private Integer range;
	public Mage(Integer spellpower,Integer manapool,Integer cost,Integer hitTimes,Integer range,char tile, String name, Integer healthPool,Integer attackPoints,Integer defensePoints, Integer x_pos, Integer y_pos)
	{
		super( tile,  name,  healthPool, attackPoints, defensePoints,  x_pos,  y_pos);
		this.spellpower=spellpower;
		this.manapool=manapool;
		currentmana=manapool/4;
		this.cost=cost;
		this.hitTimes=hitTimes;
		this.range=range;
	}
	public String levelUpCostum(int[] abilities)
	{
		int mana=25*lvl;
		manapool+=mana;
		currentmana=Math.min(currentmana+(manapool/4), manapool);
		int spl=10*lvl;
		spellpower+=spl;
		return "Level up: +"+abilities[0]+" Health, +"+abilities[1]+" Attack, +"
		+abilities[2]+" Defense"+System.lineSeparator()+"	+"+mana+" Maximum Mana +"+spl+" Spell Power";
	}
	public void specialAbility(Board board)
	{
		if(currentmana<cost)
		{
			board.addMessage("You don't have enough mana points to use Blizzard");
		}
		else
		{
			board.addMessage(name+" cast Blizzard.");
			currentmana-=cost;
			
			List<Enemy> enemies = board.getEnemies().stream().
					filter((nme) -> range(nme,this)<range).collect(toList());
			Integer hits=0;
			while(hits<hitTimes&enemies.size()>0) 
			{
				RandomGenerator random = RandomGen.getRandomGen();
				Enemy nme = enemies.get(random.nextInt(enemies.size()-1));
				if (!nme.attackers.contains(this))
					nme.attackers.add(this);
				
				int atk= spellpower;
				int def= random.nextInt(nme.defensePoints);
				board.addMessage(nme.name+" rolled "+def+" defense points.");
				int damage = Math.max(atk-def,0);
				board.addMessage(name+" hit "+nme.name+" for "+damage+" ability damage.");
				if(nme.takeDamage(board,damage))
				{
					nme.death(board);
				}
				hits++;
			}
		}
	}
	@Override
	public void onTick() {
		currentmana=Math.min(manapool, currentmana+1);
		
	}
	@Override
	public String getStatus() {
		return 		name+"			Health: "+currentHealth+"		Attack damage: "+attackPoints+"		Defense: "+defensePoints+System.lineSeparator()+
				"	Level: "+lvl+"		Experience: "+xp+"/"+(50*lvl)+"		SpellPower:"+spellpower+"		Mana: "+currentmana+"/"+manapool;
	}


	

}
