
package tests;

import static org.junit.jupiter.api.Assertions.*;

import java.util.ArrayList;
import java.util.List;

import org.junit.Assert;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import board.*;
import view.Display;

class TestEngae {

	
	@Test
	void testEngage(String arg[]) {
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
		Enemy nme=new Monster('s',"Lannister Solider",80,8,3,3,3,25,3);
		nme.engage(board, players.get(0));
		assertTrue(players.get(0).getHealth()<160||nme.getHealth()<80);
	}
	
	}