package tests;

import static org.junit.jupiter.api.Assertions.*;

import java.util.ArrayList;
import java.util.List;

import org.junit.Assert;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import board.*;
import view.Display;

class TestBoard {

	
	@Test
	void testBoard(String arg[]) {
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
		ActionGen actiongen = ActionGen.getActionGen(arg[0],true,new Display());
		board.playTurn();
		
		assertEquals(board.getMap()[3][3].toString(),"@");
	}
	
	}


