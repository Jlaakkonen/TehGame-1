using UnityEngine;
using System.Collections;

public class map_character_script : MonoBehaviour
{
	private float x;
	private float y;
	private float ukko_speed_x;
	private float ukko_speed_y;
	private const float UKKO_ACCELERATION = 10.0f;
	private const float UKKO_MAX_SPEED = 4.0f;
	private const float UKKO_RADIUS_X = 0.4f;
	private const float UKKO_DODGE_RADIUS_X = 0.1f;
	private const float UKKO_RADIUS_Y = 0.2f;
	private const float UKKO_DODGE_RADIUS_Y = 0.05f;
	private const float ANIMATION_SPEED = 10.0f;
	private map_camera_script camera;
	private map_manager map;
	private int id = -1;
	
	private Material image;
	private int spritesheet_square_x;
	private int spritesheet_square_y;
	private float spritesheet_animation_timer;
	private map_ui_script map_ui;
	
	//private GAME_STATE game_state;



	void Start()
	{
		/*
		GameObject global_scripts = GameObject.Find("_GLOBAL_SCRIPTS");						// get the pointer to
		if (!global_scripts) Application.LoadLevel("start_scene");							// the global game manager.
		else game_state = GameObject.Find("_GLOBAL_SCRIPTS").GetComponent<GAME_STATE>();	//
		*/
		camera = GameObject.Find("Main Camera").GetComponent<map_camera_script>();
		map = GameObject.Find("tilemap_parent").GetComponent<map_manager>();
		map_ui = GameObject.Find("HUD Camera").GetComponent<map_ui_script>();
		
		//x = game_state.get_map_character_position().x;
		//y = game_state.get_map_character_position().y;
		x = 1f;
		y = -1.5f;
		camera.set_camera_target(x, y, true);
		
		image = transform.FindChild("character_image").gameObject.GetComponent<Renderer>().material;
		spritesheet_square_x = 0;
		spritesheet_square_y = 0;
		spritesheet_animation_timer = 0.0f;
	}



	void Update()
	{
		if (id == -1) id = map.report_new_object(x, y, UKKO_RADIUS_X, UKKO_RADIUS_Y);
		
		apply_controls();
		
		move_character();
		animate_character();
		camera.set_camera_target(x, y, false);
		
		//game_state.set_map_character_position(new Vector2(x, y));
		
		open_message_box();
		enter_level();
		//if (Input.GetKeyDown(KeyCode.Escape)) game_state.load_level(GAME_STATE.LEVEL.MAINMENU_SCENE);
		
	}



	private void apply_controls()
	{
		float dt = Time.deltaTime;
		
		Vector2 click = new Vector2(0.0f, 0.0f);
		receive_input(ref click);
		const float SENSITIVITY = 0.1f;
		
		if (click.y > SENSITIVITY || click.y < -SENSITIVITY || click.x < -SENSITIVITY || click.x > SENSITIVITY)
			spritesheet_animation_timer += dt * ANIMATION_SPEED;
		else
			spritesheet_animation_timer = 0.0f;
		
		if (click.y > SENSITIVITY)
		{
			ukko_speed_y += UKKO_ACCELERATION * Time.deltaTime * click.y;
			spritesheet_square_y = 0;
			if (ukko_speed_y > 0.0f) // moving up
			{
				if (map.map_value(x - UKKO_RADIUS_X, y + UKKO_RADIUS_Y + ukko_speed_y * dt) < 100 &&
					map.map_value(x + UKKO_RADIUS_X, y + UKKO_RADIUS_Y + ukko_speed_y * dt) >= 100 &&
					map.map_value(x + UKKO_DODGE_RADIUS_X, y + UKKO_RADIUS_Y + ukko_speed_y * dt) < 100)
					if (ukko_speed_x > -UKKO_MAX_SPEED * 0.3f)
						ukko_speed_x = -UKKO_MAX_SPEED * 0.3f; // dodge left
				if (map.map_value(x + UKKO_RADIUS_X, y + UKKO_RADIUS_Y + ukko_speed_y * dt) < 100 &&
					map.map_value(x - UKKO_RADIUS_X, y + UKKO_RADIUS_Y + ukko_speed_y * dt) >= 100 &&
					map.map_value(x - UKKO_DODGE_RADIUS_X, y + UKKO_RADIUS_Y + ukko_speed_y * dt) < 100)
					if (ukko_speed_x < UKKO_MAX_SPEED * 0.3f)
						ukko_speed_x = UKKO_MAX_SPEED * 0.3f; // dodge right
			}
		}
		else if (click.y < -SENSITIVITY)
		{
			ukko_speed_y += UKKO_ACCELERATION * Time.deltaTime * click.y;
			spritesheet_square_y = 1;
			if (ukko_speed_y < 0.0f) // moving down
			{
				if (map.map_value(x - UKKO_RADIUS_X, y - UKKO_RADIUS_Y + ukko_speed_y * dt) < 100 &&
					map.map_value(x + UKKO_RADIUS_X, y - UKKO_RADIUS_Y + ukko_speed_y * dt) >= 100 &&
					map.map_value(x + UKKO_DODGE_RADIUS_X, y - UKKO_RADIUS_Y + ukko_speed_y * dt) < 100)
					if (ukko_speed_x > -UKKO_MAX_SPEED * 0.3f)
						ukko_speed_x = -UKKO_MAX_SPEED * 0.3f; // dodge left
				if (map.map_value(x + UKKO_RADIUS_X, y - UKKO_RADIUS_Y + ukko_speed_y * dt) < 100 &&
					map.map_value(x - UKKO_RADIUS_X, y - UKKO_RADIUS_Y + ukko_speed_y * dt) >= 100 &&
					map.map_value(x - UKKO_DODGE_RADIUS_X, y - UKKO_RADIUS_Y + ukko_speed_y * dt) < 100)
					if (ukko_speed_x < UKKO_MAX_SPEED * 0.3f)
						ukko_speed_x = UKKO_MAX_SPEED * 0.3f; // dodge right
			}
		}
		else
		{
			if (ukko_speed_y < 0.0f)
			{
				ukko_speed_y += UKKO_ACCELERATION * Time.deltaTime;
				if (ukko_speed_y > 0.0f) ukko_speed_y = 0.0f;
			}
			if (ukko_speed_y > 0.0f)
			{
				ukko_speed_y -= UKKO_ACCELERATION * Time.deltaTime;
				if (ukko_speed_y < 0.0f) ukko_speed_y = 0.0f;
			}
		}
		if (click.x < -SENSITIVITY)
		{
			ukko_speed_x += UKKO_ACCELERATION * Time.deltaTime * click.x;
			if (Mathf.Abs(click.x) > Mathf.Abs(click.y)) spritesheet_square_y = 2;
			if (ukko_speed_x < 0.0f) // moving left
			{
				if (map.map_value(x - UKKO_RADIUS_X + ukko_speed_x * dt, y + UKKO_RADIUS_Y) < 100 &&
					map.map_value(x - UKKO_RADIUS_X + ukko_speed_x * dt, y - UKKO_RADIUS_Y) >= 100 &&
					map.map_value(x - UKKO_RADIUS_X + ukko_speed_x * dt, y - UKKO_DODGE_RADIUS_Y) < 100)
					if (ukko_speed_y < UKKO_MAX_SPEED * 0.3f)
						ukko_speed_y = UKKO_MAX_SPEED * 0.3f; // dodge up
				if (map.map_value(x - UKKO_RADIUS_X + ukko_speed_x * dt, y - UKKO_RADIUS_Y) < 100 &&
					map.map_value(x - UKKO_RADIUS_X + ukko_speed_x * dt, y + UKKO_RADIUS_Y) >= 100 &&
					map.map_value(x - UKKO_RADIUS_X + ukko_speed_x * dt, y + UKKO_DODGE_RADIUS_Y) < 100)
					if (ukko_speed_y > -UKKO_MAX_SPEED * 0.3f)
						ukko_speed_y = -UKKO_MAX_SPEED * 0.3f; // dodge down
			}
		}
		else if (click.x > SENSITIVITY)
		{
			ukko_speed_x += UKKO_ACCELERATION * Time.deltaTime * click.x;
			if (Mathf.Abs(click.x) > Mathf.Abs(click.y)) spritesheet_square_y = 3;
			if (ukko_speed_x > 0.0f) // moving left
			{
				if (map.map_value(x + UKKO_RADIUS_X + ukko_speed_x * dt, y + UKKO_RADIUS_Y) < 100 &&
					map.map_value(x + UKKO_RADIUS_X + ukko_speed_x * dt, y - UKKO_RADIUS_Y) >= 100 &&
					map.map_value(x + UKKO_RADIUS_X + ukko_speed_x * dt, y - UKKO_DODGE_RADIUS_Y) < 100)
					if (ukko_speed_y < UKKO_MAX_SPEED * 0.3f)
						ukko_speed_y = UKKO_MAX_SPEED * 0.3f; // dodge up
				if (map.map_value(x + UKKO_RADIUS_X + ukko_speed_x * dt, y - UKKO_RADIUS_Y) < 100 &&
					map.map_value(x + UKKO_RADIUS_X + ukko_speed_x * dt, y + UKKO_RADIUS_Y) >= 100 &&
					map.map_value(x + UKKO_RADIUS_X + ukko_speed_x * dt, y + UKKO_DODGE_RADIUS_Y) < 100)
					if (ukko_speed_y > -UKKO_MAX_SPEED * 0.3f)
						ukko_speed_y = -UKKO_MAX_SPEED * 0.3f; // dodge down
			}
		}
		else
		{
			if (ukko_speed_x < 0.0f)
			{
				ukko_speed_x += UKKO_ACCELERATION * Time.deltaTime;
				if (ukko_speed_x > 0.0f) ukko_speed_x = 0.0f;
			}
			if (ukko_speed_x > 0.0f)
			{
				ukko_speed_x -= UKKO_ACCELERATION * Time.deltaTime;
				if (ukko_speed_x < 0.0f) ukko_speed_x = 0.0f;
			}
		}
		//Debug.Log("ukko=("+x+", "+y+")   speed=("+ukko_speed_x+", "+ukko_speed_y+")");
	}



	private void receive_input(ref Vector2 click) // getting input from mouse or keyboard?
	{
		click = map_ui.get_arrow_click_position();
		
		if (Input.GetKey(KeyCode.UpArrow)) click.y = 1.0f;
		if (Input.GetKey(KeyCode.DownArrow)) click.y = -1.0f;
		if (Input.GetKey(KeyCode.LeftArrow)) click.x = -1.0f;
		if (Input.GetKey(KeyCode.RightArrow)) click.x = 1.0f;
	}



	private void move_character()
	{
		if (ukko_speed_x < -UKKO_MAX_SPEED) ukko_speed_x = -UKKO_MAX_SPEED;
		if (ukko_speed_x > UKKO_MAX_SPEED) ukko_speed_x = UKKO_MAX_SPEED;
		if (ukko_speed_y < -UKKO_MAX_SPEED) ukko_speed_y = -UKKO_MAX_SPEED;
		if (ukko_speed_y > UKKO_MAX_SPEED) ukko_speed_y = UKKO_MAX_SPEED;
		
		float dt = Time.deltaTime;
		
		if (ukko_speed_y > 0.0f) // moving up
		{
			if (map.map_value(x - UKKO_RADIUS_X, y + UKKO_RADIUS_Y + ukko_speed_y * dt) < 100 &&
				map.map_value(x + UKKO_RADIUS_X, y + UKKO_RADIUS_Y + ukko_speed_y * dt) < 100)
				y += ukko_speed_y * dt;
			else
				ukko_speed_y = 0.0f;
		}
		if (ukko_speed_y < 0.0f) // moving down
		{
			if (map.map_value(x - UKKO_RADIUS_X, y - UKKO_RADIUS_Y + ukko_speed_y * dt) < 100 &&
				map.map_value(x + UKKO_RADIUS_X, y - UKKO_RADIUS_Y + ukko_speed_y * dt) < 100)
				y += ukko_speed_y * Time.deltaTime;
			else
				ukko_speed_y = 0.0f;
		}
		if (ukko_speed_x < 0.0f) // moving left
		{
			if (map.map_value(x - UKKO_RADIUS_X + ukko_speed_x * dt, y + UKKO_RADIUS_Y) < 100 &&
				map.map_value(x - UKKO_RADIUS_X + ukko_speed_x * dt, y - UKKO_RADIUS_Y) < 100)
				x += ukko_speed_x * Time.deltaTime;
			else
				ukko_speed_x = 0.0f;
		}
		if (ukko_speed_x > 0.0f) // moving right
		{
			if (map.map_value(x + UKKO_RADIUS_X + ukko_speed_x * dt, y + UKKO_RADIUS_Y) < 100 &&
				map.map_value(x + UKKO_RADIUS_X + ukko_speed_x * dt, y - UKKO_RADIUS_Y) < 100)
				x += ukko_speed_x * Time.deltaTime;
			else
				ukko_speed_x = 0.0f;
		}
		transform.position = new Vector3(x, 0.0f, y);
		map.update_object_position(id, x, y);
	}



	private void animate_character()
	{
		const float SPRITESHEET_W = 8;
		const float SPRITESHEET_H = 8;
		
		if (spritesheet_animation_timer >= 4.0f) spritesheet_animation_timer -= 4.0f;
		
		int square_x = spritesheet_square_x + (int)spritesheet_animation_timer;
		
		int square_y = spritesheet_square_y;
		if (spritesheet_animation_timer > 0.0f) square_y += 4;
		
		image.mainTextureScale = new Vector2(1.0f / SPRITESHEET_W, 1.0f / SPRITESHEET_H);
		image.mainTextureOffset = new Vector2(square_x / SPRITESHEET_W, -(square_y + 1.0f) / SPRITESHEET_H);
	}



	private void enter_level()
	{
		if (map.map_value_tiles_only(x, y) == 1)
		{
			//game_state.load_level(GAME_STATE.LEVEL.VELHOPELI);
			//game_state.set_map_character_position(new Vector2(x, y - 0.5f));
			GlobalGameManager.GGM.StartGame(0);
		}
		if (map.map_value_tiles_only(x, y) == 2)
		{
			//game_state.load_level(GAME_STATE.LEVEL.MAP_SCENE);
			//game_state.set_map_character_position(new Vector2(x, y - 0.5f));
			GlobalGameManager.GGM.StartGame(1);
		}
		if (map.map_value_tiles_only(x, y) == 3)
		{
			//game_state.load_level(GAME_STATE.LEVEL.MAINMENU_SCENE);
			//game_state.set_map_character_position(new Vector2(x, y - 0.5f));

		}
	}



	public void get_map_character_position(ref Vector2 position, ref Vector2 size)
	{
		position = new Vector2(x, y);
		size = new Vector2(UKKO_RADIUS_X, UKKO_RADIUS_Y);
	}



	private int last_message_id = 0;
	private int last_message = 0;
	private void open_message_box()
	{
		int current_message = 0;
		if (map.map_value(x, y + 1.0f) == 1) current_message = 1;
		else if (map.map_value(x, y + 1.0f) == 2) current_message = 2;
		else if (map.map_value(x, y + 1.0f) == 3) current_message = 3;
		
		if (current_message != last_message)
		{
			if (current_message == 0) MAP_MESSAGES.HIDE_MESSAGE(last_message_id);
			if (current_message == 1) last_message_id = MAP_MESSAGES.SHOW_MESSAGE("TALO 1");
			if (current_message == 2) last_message_id = MAP_MESSAGES.SHOW_MESSAGE("TALO 2");
			if (current_message == 3) last_message_id = MAP_MESSAGES.SHOW_MESSAGE("TALO 3");
			
						if (current_message > 0)Debug.Log("MAP_CHARACTER_SCRIPT: näytetään message "+current_message+" (id="+last_message_id+")");
						else Debug.Log("MAP_CHARACTER_SCRIPT: piilotetaan message "+last_message_id);
			
			last_message = current_message;
		}
									if (Input.GetKeyDown(KeyCode.Z))
									{
										MAP_MESSAGES.HIDE_MESSAGE();
										Debug.Log("piilotetaan mikä hyvänsä viesti");
									}
									if (Input.GetKeyDown(KeyCode.X))
									{
										int id = MAP_MESSAGES.SHOW_MESSAGE("TEST TEST");
										Debug.Log("näytetään viesti (id="+id+")");
									}
		
	}
	
}

