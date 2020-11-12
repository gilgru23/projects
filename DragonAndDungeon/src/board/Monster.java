package board;

public class Monster extends Enemy  {
	private Integer visionRange;
	public Monster(char tile, String name, Integer healthPool,Integer attackPoints,Integer defensePoints, Integer x_pos, Integer y_pos, Integer xpValue, Integer visionRange)
	{
		super(tile, name, healthPool, attackPoints,
				defensePoints, x_pos, y_pos, xpValue);
		this.visionRange=visionRange;
	}
	
	final static int Stay = 0 ;
	final static int Up = 1 ;
	final static int Right = 2 ;
	final static int Down = 3 ;
	final static int Left = 4 ;
	
	
	@Override
	public void onTurn(Board board) {
		Player p=null;
		double lastRng = Double.POSITIVE_INFINITY;
		for (Player pl : board.getPlayers()) 
		{
			double rng =range(this,pl);
			if(rng<visionRange&rng<lastRng)
			{	
				p=pl;
				lastRng=rng;
			}
		}
		if(p!=null)
		{
			int dx=this.pos.x-p.pos.x;
			int dy=p.pos.y-this.pos.y;
			if(Math.abs(dx)>Math.abs(dy))
			{
				if(dx>0)
				{
					if(!moveUnit(board, Left))
					{
						if(dy>0)
							moveUnit(board, Down);
						else
							moveUnit(board, Up);
					}
				}
				else
				{
					if(!moveUnit(board, Right))
					{
						if(dy>0)
							moveUnit(board, Down);
						else
							moveUnit(board, Up);
					}
				}
			}
			else
			{
				if(dy>0)
				{
					if(!moveUnit(board, Down)) 
					{
						if(dx>0)
							moveUnit(board, Left);
						else
							moveUnit(board, Right);
					}
				}
				else
				{
					if(!moveUnit(board, Up)) 
					{
						if(dx>0)
							moveUnit(board, Left);
						else
							moveUnit(board, Right);
					}
				}
			}
		}
		else
		{
			RandomGenerator random =RandomGen.getRandomGen();
			boolean canMove=false;
			while(!canMove)
			{
				int step = random.nextInt(4);
				
				if (step==Stay)
					canMove=true;
				else if (moveUnit(board,step))
					canMove=true;
			}
			
		}	
	}
	
	

}
