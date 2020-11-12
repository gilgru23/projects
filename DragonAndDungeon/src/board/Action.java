package board;

public enum Action {
	
	w (1), //Up
	d (2), //Right
	s (3), //Down
	a (4), //Left
	q (0), //Wait
	e (null); //Cast Special Abillity
	
	private Integer step;
	Action(Integer step) 
	{
		this.step=step;
	}
	
	public void play(Board board,Player p) 
	{
		if (step==null) 
		{
			p.specialAbility(board);
		}
		else if(step>0)
		{
			p.moveUnit(board, step);
		}
	}
	
	public static Action valueOfChar(String input) 
	{
		if(input!=null &&(input.equals("d")|input.equals("e")|input.equals("a")|
				input.equals("s")|input.equals("w")|input.equals("q")))
			return Action.valueOf(input);
		else
			return Action.q; //return 'wait'
	}
}
