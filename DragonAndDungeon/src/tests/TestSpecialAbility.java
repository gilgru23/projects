
package tests;

import static org.junit.jupiter.api.Assertions.*;

import java.util.ArrayList;
import java.util.List;

import org.junit.Assert;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import board.*;
import view.Display;

class TestSpecialAbility {

	@Test
	void testSpecialAbility(String arg[]) {
	
			char[][] map=  {{'#','#','#','#','#','#','#','#','#'},
							{'#','.','.','.','.','.','.','.','#'},
							{'#','.','.','#','#','#','.','.','#'},
							{'#','.','.','.','#','.','.','.','#'},
							{'#','.','.','#','#','#','.','.','#'},
							{'#','.','.','.','#','.','.','.','#'},
							{'#','.','.','#','#','#','.','.','#'},
							{'#','.','.','.','.','.','.','.','#'},
							{'#','#','#','#','#','#','#','#','#'}};
			List<Player> players =new ArrayList<>();
	
		Enemy nme1=new Monster('s',"Lannister Solider",80,8,3,3,3,2,4);
		players.add(new Mage(40,300,30,5,6,'@',"Melisandre",100,10,1,2,3));
		players.add(new Warrior(6,'@',"Jon Snow",200 ,30,4,4,1));
		players.add(new Rogue(20,'@',"Arya Stark ",100 ,40,2,5,1));
		Enemy nme2=new Monster('s',"Lannister Solider",80,8,3,3,3,5,2);
		Board board = new Board(map,players);
		players.get(0).specialAbility(board);
		players.get(1).specialAbility(board);
		players.get(2).specialAbility(board);
		assertTrue(players.get(1).getHealth()>100&&nme1.getHealth()<80&&nme2.getHealth()<80);
		
	}
	
	}