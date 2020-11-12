package board;

public class GamePiece {
	protected char tile;
	protected boolean passable;
	public GamePiece(char tile)
	{
		this.tile=tile;
		passable=(tile=='.');
	}
	
	public char getTile() 
	{
		return tile;
	}
	
	public boolean engagedBy(Enemy enemy) 
	{
		return false;
	}
	public boolean engagedBy(Player player) 
	{
		return false;
	}
	public boolean engagedBy(GamePiece piece) 
	{
		return false;
	}
}
