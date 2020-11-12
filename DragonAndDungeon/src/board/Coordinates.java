package board;

public class Coordinates {
	public int x;
	public int y;
	
	public Coordinates(int x, int y) 
	{
		this.x=x;
		this.y=y;
	}
	public boolean equals(Coordinates other)
	{
		return (this.x==other.x&this.y==other.y);
	}
}
