using UnityEngine;
using System.Collections;

public class map_manager : MonoBehaviour
{
	public GameObject tile_snow1;
	public GameObject tile_road1;
	public GameObject tile_housefloor1;
	public GameObject tile_wall1;
	public GameObject tile_tree1;
	public GameObject tile_house1;
	public GameObject template_grid;
	
	private const int MAP_WIDTH = 16;
	private const int MAP_HEIGHT = 16;
	private int[,] map = new int[MAP_WIDTH, MAP_HEIGHT];
	
	private int number_of_objects = 0;
	private const int MAX_NUMBER_OF_OBJECTS = 100;
	private Vector2[] object_position = new Vector2[MAX_NUMBER_OF_OBJECTS];
	private Vector2[] object_radius = new Vector2[MAX_NUMBER_OF_OBJECTS];



	void Start()
	{
		instantiate_map();
		
		//number_of_objects = 0;
		for (int a = 0; a < MAX_NUMBER_OF_OBJECTS; a++)
		{
			object_position[a] = new Vector2(0.0f, 0.0f);
			object_radius[a] = new Vector2(0.0f, 0.0f);
		}
	}



	void Update()
	{
		//Debug.Log("obejcts="+number_of_objects);
	}



	public int map_value(float x, float z)
	{
		for (int a = 0; a < number_of_objects; a++)
		{
			if (x > object_position[a].x - object_radius[a].x &&
				x < object_position[a].x + object_radius[a].x &&
				z > object_position[a].y - object_radius[a].y &&
				z < object_position[a].y + object_radius[a].y) return 100;
		}
		if (x >= 0.0f && x < MAP_WIDTH && z <= 0.0f && z > -MAP_HEIGHT) return map[(int)x, (int)(-z)];
		else return 100;
	}



	public int map_value_tiles_only(float x, float z)
	{
		if (x >= 0.0f && x < MAP_WIDTH && z <= 0.0f && z > -MAP_HEIGHT) return map[(int)x, (int)(-z)];
		else return 100;
	}



	public int report_new_object(float x, float y, float radius_x, float radius_y)
	{
		
		object_position[number_of_objects] = new Vector2(x, y);
		object_radius[number_of_objects] = new Vector2(radius_x, radius_y);
		number_of_objects++;
		//Debug.Log("REPORT  obejcts="+number_of_objects);
		int value = number_of_objects - 1;
		return value;
	}



	public void update_object_position(int id, float x, float y)
	{
		//Debug.Log("UPDATING POS id="+id+"  "+x+","+y+"    --------   objects="+number_of_objects);
		object_position[id] = new Vector2(x, y);
	}



	private void instantiate_map()
	{
		string[] map_data =
		{
			"----------------",
			"-rrrrrrr-THHH---",
			"-r--T--rT-H1H---",
			"rrW--W-r---r-T--",
			"--W----rrrrr----",
			"---WW--r-HHH--T-",
			"-T-----r-H3HT---",
			"HHH--T-r--r-----",
			"H2HT---rrrr-W---",
			"-r--rrrr--TW----",
			"-rWWr----W------",
			"-rrrr-------TT-T",
			"----r-----TT-T-T",
			"----rrrrrrTT-T-T",
			"----------TT---T",
			"----------TTTTTT"
		};
		
		Destroy(template_grid);
		for (int x = 0; x < MAP_WIDTH; x++)
		{
			for (int y = 0; y < MAP_HEIGHT; y++)
			{
				GameObject next_tile = null;
				Vector3 tile_position = new Vector3(x, 0.0f, -y);
				Quaternion tile_rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
				
				if (map_data[y][x] == '-')
				{
					next_tile = (GameObject)Instantiate(tile_snow1, tile_position, tile_rotation);
					map[x, y] = 0;
				}
				if (map_data[y][x] == 'r')
				{
					next_tile = (GameObject)Instantiate(tile_road1, tile_position, tile_rotation);
					map[x, y] = 0;
				}
				if (map_data[y][x] == 'W')
				{
					next_tile = (GameObject)Instantiate(tile_wall1, tile_position, tile_rotation);
					map[x, y] = 100;
				}
				if (map_data[y][x] == 'T')
				{
					next_tile = (GameObject)Instantiate(tile_tree1, tile_position, tile_rotation);
					map[x, y] = 100;
				}
				if (map_data[y][x] == 'H')
				{
					next_tile = (GameObject)Instantiate(tile_housefloor1, tile_position, tile_rotation);
					map[x, y] = 100;
				}
				if (map_data[y][x] == '1')
				{
					next_tile = (GameObject)Instantiate(tile_house1, tile_position, tile_rotation);
					next_tile.transform.SetParent(transform);
					next_tile = (GameObject)Instantiate(tile_housefloor1, tile_position, tile_rotation);
					map[x, y] = 1;
				}
				if (map_data[y][x] == '2')
				{
					next_tile = (GameObject)Instantiate(tile_house1, tile_position, tile_rotation);
					next_tile.transform.SetParent(transform);
					next_tile = (GameObject)Instantiate(tile_housefloor1, tile_position, tile_rotation);
					map[x, y] = 2;
				}
				if (map_data[y][x] == '3')
				{
					next_tile = (GameObject)Instantiate(tile_house1, tile_position, tile_rotation);
					next_tile.transform.SetParent(transform);
					next_tile = (GameObject)Instantiate(tile_housefloor1, tile_position, tile_rotation);
					map[x, y] = 3;
				}
				
				next_tile.transform.SetParent(transform);
			}
		}
	}

}
