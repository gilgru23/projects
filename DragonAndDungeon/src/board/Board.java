package board;

import java.util.ArrayList;
import java.util.List;

public class Board {
	private List<Enemy> enemies;
	private List<Player> players;//setting foundations for multiplayer
	private GamePiece[][] map;
	private List<GameUnit>[] deadUnits;
	private List<String> messages;
	public Board (char[][] cMap,List<Player> players)
	{
		this.messages=new ArrayList<>();
		this.players=players;
		this.enemies= new ArrayList<>();
		deadUnits= new List[2];
		deadUnits[0]=new ArrayList<>();//players
		deadUnits[1]=new ArrayList<>();//enemies
		this.map = new GamePiece[cMap.length][cMap[0].length];
		int plyr = 0;
		
		for(int i=0;i<cMap.length;i++)
		{
			for(int j=0;j<cMap[i].length;j++)
			{
				if (cMap[i][j]=='#') 
				{
					map[i][j]= new GamePiece('#');
				}
				else if (cMap[i][j]=='.') 
				{
					map[i][j]= new GamePiece('.');
				}
				else if (cMap[i][j]=='@') 
				{
					if (plyr < players.size()) 
					{
						Player plr =players.get(plyr);
						plr.setPosition(j,i);
						map[i][j] = plr;
						plyr++;
					}
				}
				else 
				{
					try 
					{
						Foe foe = Foe.valueOf(""+cMap[i][j]);
						Enemy bad = foe.createFoe(cMap[i][j], j, i);
						map[i][j]=bad;
						enemies.add(bad);
					}
					catch(IllegalArgumentException e)
					{
						map[i][j]= new GamePiece('.');
					}
				}
			}
		}
	}
	
	public List<Player> getPlayers()
	{
		return players;
	}
	public List<Enemy> getEnemies()
	{
		return enemies;
	}
	public GamePiece[][] getMap()
	{
		return map;
	}
	public void addMessage(String msg) 
	{
		messages.add(msg);
	}
	public List<String> takeMessages()
	{
		List<String> output= messages;
		messages= new ArrayList<>();
		return output;
	}
	
	//toString is used for testing purposes
	public String toString()  
	{
		String s="";
		for(int i=0;i<map.length;i++)
		{
			s=s+System.getProperty("line.separator");
			for(int j=0;j<map[i].length;j++)
			{
				String c=""+map[i][j].tile;
				s=s+c;
			}
		}
		return s;
	}
	public void death(Player p) 
	{
		deadUnits[0].add(p);
		if(p.tile==map[p.pos.x][p.pos.y].tile) 
		{
			GamePiece deadPlayer =new GamePiece('.');
			deadPlayer.tile='X';
			map[p.pos.y][p.pos.x]=deadPlayer;
		}
	}
	public void death(Enemy nme) 
	{
		deadUnits[1].add(nme);
		if(nme.tile==map[nme.pos.y][nme.pos.x].tile) {
			map[nme.pos.y][nme.pos.x]=null;
			map[nme.pos.y][nme.pos.x]=new GamePiece('.');
			}
	}

	public void removeDeadUnits() 
	{
		for(GameUnit player : deadUnits[0]) 
		{
			players.remove(player);
		}
		for(GameUnit enemy : deadUnits[1]) 
		{
			enemies.remove(enemy);
		}
		deadUnits[0]=new ArrayList<>();
		deadUnits[1]=new ArrayList<>();
	}
	
	public int playTurn() 
	{
		int end = 0;
		
		// make each player/enemy play its turn
		for(Player p : players) 
		{
			if(p.isAlive()) 
			{
				p.onTick();
				p.playTurn(this);
			}
		}
		for (Enemy nme : enemies) 
		{
			if(nme.isAlive())
				nme.onTurn(this);
		}
		removeDeadUnits();
		
		// after round is over check whether game or level is over
		if (players.size()==0) 
		{
			end = -1; //game-over
		}
		else if (enemies.size()==0) 
		{
			end = 1; //level won
		}
		
		return end;
	}
}
