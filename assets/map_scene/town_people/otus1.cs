using UnityEngine;
using System.Collections;

public class otus1 : MonoBehaviour
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
	
	private Material image;
	private int spritesheet_square_x;
	private int spritesheet_square_y;
	private float spritesheet_animation_timer;



	void Start()
	{
		x = transform.position.x;
		y = transform.position.z;
		speed_x = 0.0f;
		speed_y = 0.0f;
		state = 0;
		state_timer = 0.0f;
		map = GameObject.Find("tilemap_parent").GetComponent<map_manager>();
		
		image = transform.FindChild("image").gameObject.GetComponent<Renderer>().material;
		spritesheet_square_x = 0;
		spritesheet_square_y = 1;
		spritesheet_animation_timer = 0.0f;
	}



	void Update()
	{
		if (id == -1) id = map.report_new_object(x, y, RADIUS_X, RADIUS_Y);
		
		move();
		animate();
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
		if (state == 0 && (speed_y == 0.0f || state_timer > 5.0f))
		{
			state = 3;
			spritesheet_square_y = 3;
			state_timer = 0.0f;
			speed_x = MAX_SPEED;
			speed_y = 0.0f;
		}
		if (state == 1 && (speed_y == 0.0f || state_timer > 5.0f))
		{
			state = 2;
			spritesheet_square_y = 2;
			state_timer = 0.0f;
			speed_x = -MAX_SPEED;
			speed_y = 0.0f;
		}
		if (state == 2 && (speed_x == 0.0f || state_timer > 5.0f))
		{
			state = 0;
			spritesheet_square_y = 0;
			state_timer = 0.0f;
			speed_x = 0.0f;
			speed_y = MAX_SPEED;
		}
		if (state == 3 && (speed_x == 0.0f || state_timer > 5.0f))
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
		const float SPRITESHEET_W = 1;
		const float SPRITESHEET_H = 4;
		
		//if (spritesheet_animation_timer >= 4.0f) spritesheet_animation_timer -= 4.0f;
		
		int square_x = spritesheet_square_x;// + (int)spritesheet_animation_timer;
		int square_y = spritesheet_square_y;
		//if (spritesheet_animation_timer > 0.0f) square_y += 4;
		
		image.mainTextureScale = new Vector2(1.0f / SPRITESHEET_W, 1.0f / SPRITESHEET_H);
		image.mainTextureOffset = new Vector2(square_x / SPRITESHEET_W, -(square_y + 1.0f) / SPRITESHEET_H);
	}


}
