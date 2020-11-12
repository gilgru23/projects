package board;

public enum Foe {
	//Monsters:
	s ("Lannister Solider",80,8,3,3,25,false),
	k ("Lannister Knight",200,14,8,4,50,false),
	q ("Queen’s Guard",400,20,15,5,100,false),
	z ("Wright",600,30,15,3,100,false),
	b ("Bear-Wright",1000,75,30,4,250,false),
	g ("Giant-Wright",1500,100,40,5,500,false),
	w ("White Walker",2000,150,50,6,1000,false),
	M ("The Mountain",1000,60,25,6,500,false),
	C ("Queen Cersei ",100,10,10,1,1000,false),
	K ("Night’s King",5000,300,150,8,5000,false),
	//Traps:
	B ("Bonus Trap",1,1,1,5,250,6,2,true),
	Q ("Queen’s Trap",250,50,10,4,100,10,4,true),
	D ("Death Trap",500,100,20,6,250,10,3,true);
	
	private String name;
    private int health;
    private int attack; 
    private int defense;
    private int range;
    private int xpValue;
    private int respawn;
    private int visibility;
    private boolean isTrap;
    Foe(String name, int health, int attack, int defense,
    		int range, int xpValue, boolean isTrap ) {//for monster
        this.name = name;
        this.health= health;
        this.attack= attack;
        this.defense= defense;
        this.range= range;
        this.xpValue= xpValue;
        this.isTrap=isTrap;
    }
    Foe(String name, int health, int attack, int defense, int range,
    		int xpValue,int respawn,int visibility, boolean isTrap ) {//for trap
        this.name = name;
        this.health= health;
        this.attack= attack;
        this.defense= defense;
        this.range= range;
        this.xpValue= xpValue;
        this.respawn=respawn;
        this.visibility=visibility;
        this.isTrap=isTrap;
    }
    
    Enemy createFoe(char c, int x_pos, int y_pos) 
    {
    	if(!isTrap)
    		return new Monster(c,name,health,attack,defense, x_pos, y_pos, xpValue, range);
    	else
    		return new Trap(c, name,  health, attack,
    				 defense,x_pos, y_pos,  xpValue,
    				 range,respawn, visibility);

    }
    
    
}
