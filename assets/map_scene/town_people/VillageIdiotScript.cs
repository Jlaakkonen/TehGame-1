using UnityEngine;
using System.Collections;

public class VillageIdiotScript : MonoBehaviour
{
	private const float RADIUS_X = 0.4f;
	private const float RADIUS_Y = 0.3f;
	private const float MAX_SPEED = 1.0f;
	private float x;
	private float y;
	private float speed_x;
	private float speed_y;
	private int state;
	private float state_timer;
	
	private map_manager map;
	private int id = -1;
	
	private const float ANIMATION_SPEED = 16.0f;
	private Material image;
	private int spritesheet_square_x;
	private int spritesheet_square_y;
	private float spritesheet_animation_timer;
	
	private map_character_script player;
	private bool sees_player;
	private int sentence;



	void Start()
	{
		x = transform.position.x;
		y = transform.position.z;
		speed_x = 0.0f;
		speed_y = 0.0f;
		state = 0;
		state_timer = 0.0f;
		map = GameObject.Find("tilemap_parent").GetComponent<map_manager>();
		
		player = GameObject.Find("map_character").GetComponent<map_character_script>();
		sees_player = false;
		sentence = 0;
		
		image = transform.FindChild("image").gameObject.GetComponent<Renderer>().material;
		spritesheet_square_x = 0;
		spritesheet_square_y = 1;
		spritesheet_animation_timer = 0.0f;
	}
	
	
	
	void Update()
	{
		if (id == -1) id = map.report_new_object(x, y, RADIUS_X, RADIUS_Y);
		
		if (!sees_player)
		{
			move();
		}
		animate();
		talk_to_character();
	}



	private void move()
	{
		float dt = Time.deltaTime;
		
		if (speed_y > 0.0f) // moving up
		{
			if (map.map_value(x - RADIUS_X, y + RADIUS_Y + speed_y * dt) < 100 &&
			    map.map_value(x, y + RADIUS_Y + speed_y * dt) < 100 &&
			    map.map_value(x + RADIUS_X, y + RADIUS_Y + speed_y * dt) < 100)
				y += speed_y * dt;
			else
				speed_y = 0.0f;
		}
		if (speed_y < 0.0f) // moving down
		{
			if (map.map_value(x - RADIUS_X, y - RADIUS_Y + speed_y * dt) < 100 &&
			    map.map_value(x, y - RADIUS_Y + speed_y * dt) < 100 &&
			    map.map_value(x + RADIUS_X, y - RADIUS_Y + speed_y * dt) < 100)
				y += speed_y * Time.deltaTime;
			else
				speed_y = 0.0f;
		}
		if (speed_x < 0.0f) // moving left
		{
			if (map.map_value(x - RADIUS_X + speed_x * dt, y + RADIUS_Y) < 100 &&
			    map.map_value(x - RADIUS_X + speed_x * dt, y) < 100 &&
			    map.map_value(x - RADIUS_X + speed_x * dt, y - RADIUS_Y) < 100)
				x += speed_x * Time.deltaTime;
			else
				speed_x = 0.0f;
		}
		if (speed_x > 0.0f) // moving right
		{
			if (map.map_value(x + RADIUS_X + speed_x * dt, y + RADIUS_Y) < 100 &&
			    map.map_value(x + RADIUS_X + speed_x * dt, y) < 100 &&
			    map.map_value(x + RADIUS_X + speed_x * dt, y - RADIUS_Y) < 100)
				x += speed_x * Time.deltaTime;
			else
				speed_x = 0.0f;
		}
		transform.position = new Vector3(x, 0.0f, y);
		map.update_object_position(id, x, y);
		
		state_timer += dt;
		if (state == 0 && (speed_y == 0.0f || state_timer > 8.0f))
		{
			state = 3;
			spritesheet_square_y = 3;
			state_timer = 0.0f;
			speed_x = MAX_SPEED;
			speed_y = 0.0f;
		}
		if (state == 1 && (speed_y == 0.0f || state_timer > 8.0f))
		{
			state = 2;
			spritesheet_square_y = 2;
			state_timer = 0.0f;
			speed_x = -MAX_SPEED;
			speed_y = 0.0f;
		}
		if (state == 2 && (speed_x == 0.0f || state_timer > 8.0f))
		{
			state = 0;
			spritesheet_square_y = 0;
			state_timer = 0.0f;
			speed_x = 0.0f;
			speed_y = MAX_SPEED;
		}
		if (state == 3 && (speed_x == 0.0f || state_timer > 8.0f))
		{
			state = 1;
			spritesheet_square_y = 1;
			state_timer = 0.0f;
			speed_x = 0.0f;
			speed_y = -MAX_SPEED;
		}
	}
	
	
	
	private void animate()
	{
		const float SPRITESHEET_W = 5;
		const float SPRITESHEET_H = 4;
		
		spritesheet_animation_timer += Time.deltaTime * ANIMATION_SPEED;
		if (spritesheet_animation_timer >= 5.0f) spritesheet_animation_timer = 1.0f;
		if (sees_player) spritesheet_animation_timer = 0.0f;
		
		int square_x = (int)spritesheet_animation_timer;
		int square_y = spritesheet_square_y;
		
		image.mainTextureScale = new Vector2(1.0f / SPRITESHEET_W, 1.0f / SPRITESHEET_H);
		image.mainTextureOffset = new Vector2(square_x / SPRITESHEET_W, -(square_y + 1.0f) / SPRITESHEET_H);
	}



	int talk_id = -1;
	float talk_timer = 0.0f;
	private void talk_to_character()
	{
		Vector2 pos = new Vector2(0, 0);
		Vector2 size = new Vector2(0, 0);
		player.get_map_character_position(ref pos, ref size);
		
		sees_player = false;
		Vector2 seeing_point  = new Vector2(x, y + 2 * RADIUS_Y); // looking up
		if (spritesheet_square_y == 1) seeing_point = new Vector2(x, y - 2 * RADIUS_Y); // looking down
		if (spritesheet_square_y == 2) seeing_point = new Vector2(x - 2 * RADIUS_X, y); // looking left
		if (spritesheet_square_y == 3) seeing_point = new Vector2(x + 2 * RADIUS_X, y); // looking right
		
		if (spritesheet_square_y == 0 || spritesheet_square_y == 1) // looking up or down
		{
			if (pos.x + size.x > seeing_point.x - 0.5f &&
				pos.x - size.x < seeing_point.x + 0.5f &&
				pos.y + size.y > seeing_point.y &&
				pos.y - size.y < seeing_point.y)
				sees_player = true;
		}
		if (spritesheet_square_y == 2 || spritesheet_square_y == 3) // looking left or right
		{
			if (pos.x + size.x > seeing_point.x &&
				pos.x - size.x < seeing_point.x &&
				pos.y + size.y > seeing_point.y - 0.5f &&
				pos.y - size.y < seeing_point.y + 0.5f)
				sees_player = true;
		}
		
		
		
		talk_timer += Time.deltaTime;
		if (sees_player && talk_id == -1 && talk_timer > 0.1f)
		{
			Debug.Log("REPLIIKKI!!! character pos="+pos+"   size="+size+ "  sees="+sees_player);
			
			if (sentence == 0)
				talk_id = MAP_MESSAGES.SHOW_MESSAGE("HEI OLEN KYLAN","AINOA IDIOOTTI.","TAI ONHAN TOI","NORSUKIN AIKA TYHMEA.");
			if (sentence == 1)
				talk_id = MAP_MESSAGES.SHOW_MESSAGE("POIS EESTA !!!!!!", "!!!!!!!!!!!");
			if (sentence == 2)
				talk_id = MAP_MESSAGES.SHOW_MESSAGE("GOOD AFTERNOON. I AM","PLEASE TO MEET YOU","STRANGE GREEN MAN.");
			if (sentence == 3)
				talk_id = MAP_MESSAGES.SHOW_MESSAGE("OSAAN SANOA","MONTA ERI ASIAA", "KOSKA OLEN AIKA","SMART.");
			
			sentence++;
			if (sentence > 3) sentence = 0;
			talk_timer = 0.0f;
			spritesheet_animation_timer = 0.0f;
		}
		if (!sees_player && talk_id != -1 && talk_timer > 0.5f)
		{
			MAP_MESSAGES.HIDE_MESSAGE(talk_id);
			talk_id = -1;
			talk_timer = 0.0f;
		}
	}


}
