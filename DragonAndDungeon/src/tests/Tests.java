package tests;

import static org.junit.Assert.*;

import java.util.ArrayList;
import java.util.List;

import org.junit.Before;
import org.junit.Test;

import board.ActionGen;
import board.Board;
import board.Coordinates;
import board.Enemy;
import board.Mage;
import board.Monster;
import board.Player;
import view.Display;

public class Tests {

	private Board board;
	
	public Tests() {this.board=null;}
	
	@Before
	public void setUp() throws Exception {
		char[][] map=  {{'#','#','#','#','#','#','#','#','#'},
						{'#','.','.','.','.','.','.','.','#'},
						{'#','@','.','#','#','#','.','.','#'},
						{'#','.','.','.','#','.','.','s','#'},
						{'#','.','.','#','#','#','.','.','#'},
						{'#','.','.','.','#','.','.','.','#'},
						{'#','.','.','#','#','#','.','.','#'},
						{'#','.','.','.','s','.','.','.','#'},
						{'#','#','#','#','#','#','#','#','#'}};
		
		List<Player> players =new ArrayList<>();
		players.add(new Mage(40,300,30,5,6,'@',"Melisandre",160,10,1,2,3));
		Board board = new Board(map,players);
		this.board= board;
	}
	
	
	@Test
	public void testMoveUnit() {
		Player meli =board.getPlayers().get(0);
		Coordinates start = new Coordinates(meli.getPos().x,meli.getPos().y);
		meli.moveUnit(board, 2);
		Coordinates finish =meli.getPos();
		assertTrue(board.getMap()[2][1].getTile()=='.');
		assertTrue(board.getMap()[2][2].getTile()=='@');
		assertTrue(start.x==finish.x-1 & start.y==finish.y);
	}
	@Test
	public void testEngagedBy() {
		Player meli =board.getPlayers().get(0);
		Enemy Lannister1= board.getEnemies().get(0);
		Enemy Lannister2= board.getEnemies().get(1);
		assertTrue(Lannister1.engagedBy(meli));
		assertTrue(meli.engagedBy(Lannister1));
		assertFalse(Lannister1.engagedBy(Lannister2));
	}
	@Test
	public void testLevelUp() {
		Player meli =board.getPlayers().get(0);
		meli.earnXP(50);
		assertTrue(meli.getLevel()==2);
	}
	@Test
	public void testEnemyDeath() {
		Enemy Lannister =board.getEnemies().get(0);
		Player meli =board.getPlayers().get(0);
		Lannister.engagedBy(meli);
		Lannister.death(board);
		board.removeDeadUnits();
		assertTrue(board.getEnemies().size()==1);
	}
	@Test
	public void testEnemyChase() 
	{
		Enemy Lannister =board.getEnemies().get(0);
		Lannister.getPos().x=1;
		Lannister.getPos().y=4;
		Lannister.onTurn(board);
		assertTrue(Lannister.getPos().y==3);
	}
	

}
