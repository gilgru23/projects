package board;

import java.util.ArrayList;
import java.util.List;

public class Trap extends Enemy {
	private Integer relocationRange;
	private Integer relocationTime;
	private Integer visibilityTime;
	private Integer ticksCount;
	final char permTile;
	
	public Trap(char tile, String name, Integer healthPool,Integer attackPoints,
				Integer defensePoints, Integer x_pos, Integer y_pos, Integer xpValue,
				Integer relocationRange,Integer relocationTime,Integer visibilityTime)
	{
		super(tile, name, healthPool, attackPoints, defensePoints, x_pos, y_pos, xpValue);
		this.relocationRange=relocationRange;
		this.relocationTime=relocationTime;
		this.visibilityTime=visibilityTime;
		this.ticksCount=0;
		this.permTile=tile;
	}
	
	public void visible()
	{
		if (ticksCount<visibilityTime)
			tile=permTile;
		else
			tile='.';
	}
	
	@Override
	public void onTurn(Board board) {
		if(ticksCount==relocationTime)
		{
			ticksCount=0;
			relocate(board);
		}
		else
		{
			ticksCount++;
				// in multiplayer mode - attacks all players in range
			for (Player pl : board.getPlayers()) 
			{
				if(range(this,pl)<2)
				{	
					engage(board, pl);
				}
			}
		}
		visible();
	}

	private void relocate(Board board) 
	{
		GamePiece[][] map = board.getMap();
		List<Coordinates> coords = getInRange(map, relocationRange);
		List<Coordinates> vacants =  new ArrayList<>();
		for (Coordinates pos : coords) 
		{
			if(map[pos.y][pos.x].passable)
				vacants.add(pos);
		}
		RandomGenerator random= RandomGen.getRandomGen();
		if (vacants.size()>0)
		{
			map[pos.y][pos.x]=new GamePiece('.');
			this.pos = vacants.get(random.nextInt(vacants.size()-1));
			map[pos.y][pos.x]=this;
		}
	}	
	
	
}
