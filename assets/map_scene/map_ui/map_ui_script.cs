using UnityEngine;
using System.Collections;

public class map_ui_script : MonoBehaviour
{
	private float JOYSTICK_BOTTOM_MARGIN = 0.05f;
	private float JOYSTICK_LEFT_MARGIN = 0.05f;
	private float JOYSTICK_OUTER_SIZE = 0.7f;
	private float JOYSTICK_INNER_SIZE = 0.3f;
	
	private enum JOYSTICK_TYPE {VPAD_LEFT, VPAD_RIGHT, SCREEN_PAD}
	private JOYSTICK_TYPE joystick_type;
	
	private Transform arrow_up;
	private Transform arrow_down;
	private Transform arrow_left;
	private Transform arrow_right;
	private Transform arrow_collider;
	private Vector2 arrow_click_position;
	
	private bool screen_pad_clicked;
	private bool screen_pad_dragging;
	private Vector2 screen_pad_start;
	private Vector2 screen_pad_position;
	
	private float screen_width;
	private float screen_height;
	private float screen_ratio;
	private float camera_width;
	private float camera_height;
	
	private Camera ui_camera;



	void Start()
	{
		ui_camera = gameObject.GetComponent<Camera>();
		arrow_up = transform.FindChild("arrow_up");
		arrow_down = transform.FindChild("arrow_down");
		arrow_left = transform.FindChild("arrow_left");
		arrow_right = transform.FindChild("arrow_right");
		arrow_collider = transform.FindChild("arrow_collider");
		camera_height = ui_camera.orthographicSize;
		
		screen_pad_clicked = true;
		screen_pad_dragging = false;
		screen_pad_position = new Vector2(0.0f, 0.0f);
		screen_pad_start = new Vector2(0.0f, 0.0f);
	}



	void Update()
	{
		check_virtual_joystick_options();
		
		get_screen_size();
		set_virtual_joystick_parameters();
		check_virtual_joystick_click();
		
		update_textbox();
	}



	private void check_virtual_joystick_options()
	{
		joystick_type = JOYSTICK_TYPE.VPAD_LEFT;
	}



	private void get_screen_size()
	{
		screen_width = Screen.width;
		screen_height = Screen.height;
		screen_ratio = screen_width / screen_height;
		camera_width = camera_height * screen_ratio;
	}



	private void set_virtual_joystick_parameters()
	{
		Vector3 scale;
		Vector3 position;
		
		if (joystick_type == JOYSTICK_TYPE.VPAD_LEFT || joystick_type == JOYSTICK_TYPE.VPAD_RIGHT)
		{
			scale = arrow_up.localScale;
			scale.x = camera_height * JOYSTICK_OUTER_SIZE;
			scale.y = camera_height * 0.5f * (JOYSTICK_OUTER_SIZE - JOYSTICK_INNER_SIZE);
			arrow_up.localScale = scale;
			
			scale = arrow_down.localScale;
			scale.x = camera_height * JOYSTICK_OUTER_SIZE;
			scale.y = camera_height * 0.5f * (JOYSTICK_OUTER_SIZE - JOYSTICK_INNER_SIZE);
			arrow_down.localScale = scale;
			
			scale = arrow_left.localScale;
			scale.x = camera_height * 0.5f * (JOYSTICK_OUTER_SIZE - JOYSTICK_INNER_SIZE);
			scale.y = camera_height * JOYSTICK_OUTER_SIZE;
			arrow_left.localScale = scale;
			
			scale = arrow_right.localScale;
			scale.x = camera_height * 0.5f * (JOYSTICK_OUTER_SIZE - JOYSTICK_INNER_SIZE);
			scale.y = camera_height * JOYSTICK_OUTER_SIZE;
			arrow_right.localScale = scale;
			
			scale = arrow_collider.localScale;
			scale.x = camera_height * JOYSTICK_OUTER_SIZE;
			scale.y = camera_height * JOYSTICK_OUTER_SIZE;
			arrow_collider.localScale = scale;
			
			if (joystick_type == JOYSTICK_TYPE.VPAD_LEFT)
			{
				position = arrow_up.localPosition;
				position.x = -camera_width + 0.5f * arrow_up.localScale.x + camera_height * JOYSTICK_LEFT_MARGIN;
				position.y = -camera_height + 1.5f * arrow_up.localScale.y + camera_height * (JOYSTICK_BOTTOM_MARGIN + JOYSTICK_INNER_SIZE);
				arrow_up.localPosition = position;
				
				position = arrow_down.localPosition;
				position.x = -camera_width + 0.5f * arrow_down.localScale.x + camera_height * JOYSTICK_LEFT_MARGIN;
				position.y = -camera_height + 0.5f * arrow_down.localScale.y + camera_height * JOYSTICK_BOTTOM_MARGIN;
				arrow_down.localPosition = position;
				
				position = arrow_left.localPosition;
				position.x = -camera_width + 0.5f * arrow_left.localScale.x + camera_height * JOYSTICK_LEFT_MARGIN;
				position.y = -camera_height + 0.5f * arrow_left.localScale.y + JOYSTICK_BOTTOM_MARGIN * camera_height;
				arrow_left.localPosition = position;
				
				position = arrow_right.localPosition;
				position.x = -camera_width + 1.5f * arrow_right.localScale.x + camera_height * (JOYSTICK_LEFT_MARGIN + JOYSTICK_INNER_SIZE);
				position.y = -camera_height + 0.5f * arrow_right.localScale.y + JOYSTICK_BOTTOM_MARGIN * camera_height;
				arrow_right.localPosition = position;
				
				position = arrow_collider.localPosition;
				position.x = -camera_width + 0.5f * arrow_collider.localScale.x + JOYSTICK_LEFT_MARGIN * camera_height;
				position.y = -camera_height + 0.5f * arrow_collider.localScale.y + JOYSTICK_BOTTOM_MARGIN * camera_height;
				arrow_collider.localPosition = position;
			}
			else
			{
				position = arrow_up.localPosition;
				position.x = camera_width - 0.5f * arrow_up.localScale.x - camera_height * JOYSTICK_LEFT_MARGIN;
				position.y = -camera_height + 1.5f * arrow_up.localScale.y + camera_height * (JOYSTICK_BOTTOM_MARGIN + JOYSTICK_INNER_SIZE);
				arrow_up.localPosition = position;
				
				position = arrow_down.localPosition;
				position.x = camera_width - 0.5f * arrow_down.localScale.x - camera_height * JOYSTICK_LEFT_MARGIN;
				position.y = -camera_height + 0.5f * arrow_down.localScale.y + camera_height * JOYSTICK_BOTTOM_MARGIN;
				arrow_down.localPosition = position;
				
				position = arrow_left.localPosition;
				position.x = camera_width - 1.5f * arrow_left.localScale.x - camera_height * (JOYSTICK_LEFT_MARGIN + JOYSTICK_INNER_SIZE);
				position.y = -camera_height + 0.5f * arrow_left.localScale.y + JOYSTICK_BOTTOM_MARGIN * camera_height;
				arrow_left.localPosition = position;
				
				position = arrow_right.localPosition;
				position.x = camera_width - 0.5f * arrow_right.localScale.x - camera_height * JOYSTICK_LEFT_MARGIN;
				position.y = -camera_height + 0.5f * arrow_right.localScale.y + JOYSTICK_BOTTOM_MARGIN * camera_height;
				arrow_right.localPosition = position;
				
				position = arrow_collider.localPosition;
				position.x = camera_width - 0.5f * arrow_collider.localScale.x - JOYSTICK_LEFT_MARGIN * camera_height;
				position.y = -camera_height + 0.5f * arrow_collider.localScale.y + JOYSTICK_BOTTOM_MARGIN * camera_height;
				arrow_collider.localPosition = position;
			}
		}
		else
		{
			arrow_up.localPosition = new Vector3(1000.0f, 1000.0f, 0.0f);
			arrow_down.localPosition = new Vector3(1000.0f, 1000.0f, 0.0f);
			arrow_left.localPosition = new Vector3(1000.0f, 1000.0f, 0.0f);
			arrow_right.localPosition = new Vector3(1000.0f, 1000.0f, 0.0f);
			arrow_collider.localPosition = new Vector3(1000.0f, 1000.0f, 0.0f);
		}
	}



	private void check_virtual_joystick_click()
	{
		if (joystick_type == JOYSTICK_TYPE.VPAD_LEFT || joystick_type == JOYSTICK_TYPE.VPAD_RIGHT)
		{
			arrow_click_position = new Vector2(0.0f, 0.0f);
			if (Input.GetMouseButton(0))
			{
				Ray ray = ui_camera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, 10))
				{
					float xx = 2.0f * (hit.point.x - arrow_collider.position.x) / arrow_collider.localScale.x;
					float yy = 2.0f * (hit.point.y - arrow_collider.position.y) / arrow_collider.localScale.y;
					//Debug.Log("klikattiin="+hit.transform.name+"   position="+hit.point+"  xx="+xx);
					if (xx >= -1.0f && xx <= 1.0f && yy >= -1.0f && yy <= 1.0f)
						arrow_click_position = new Vector2(xx, yy);
				}
			}
		}
		else
		{
			if (Input.GetMouseButton(0))
			{
				if (!screen_pad_clicked)
				{
					screen_pad_clicked = true;
					screen_pad_dragging = true;
					screen_pad_start = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				}
				screen_pad_position = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - screen_pad_start;
				screen_pad_position *= (1.0f / screen_height);
				
			}
			else
			{
				screen_pad_clicked = false;
				screen_pad_dragging = false;
				screen_pad_position = new Vector2(0.0f, 0.0f);
			}
		}
	}



	public Vector2 get_arrow_click_position()
	{
		if (joystick_type == JOYSTICK_TYPE.VPAD_LEFT || joystick_type == JOYSTICK_TYPE.VPAD_RIGHT)
			return arrow_click_position;
		else
		{
			return screen_pad_position; 
		}
	}


	// ascii values 32...125 = 94 characters.
	private bool textbox_open = false;
	private float textbox_timer = 1.0f;
	private float text_timer = 0.0f;
	private Transform textbox;
	private Transform textbox_background;
	private const float TEXTBOX_SPEED = 4.0f;
	private const int TEXTBOX_WIDTH = 20;
	private const int TEXTBOX_HEIGHT = 5;
	private string[] textbox_line = new string[TEXTBOX_HEIGHT];
	private int textbox_x = 0;
	private int textbox_y = 0;
	public GameObject letter_prefab;
	private Material[,] letter_material = new Material[TEXTBOX_WIDTH, TEXTBOX_HEIGHT];
	bool initialize = true;
	private void update_textbox()
	{
		if (initialize)
		{
			textbox = transform.FindChild("textbox");
			textbox_background = textbox.FindChild("textbox_background");
			for (int x = 0; x < TEXTBOX_WIDTH; x++)
			{
				for (int y = 0; y < TEXTBOX_HEIGHT; y++)
				{
					GameObject temp = (GameObject)Instantiate(letter_prefab, new Vector3(0,0,0), Quaternion.identity);
					Transform letter = temp.transform;
					letter.SetParent(textbox_background);
					letter.localScale = new Vector3(1.0f / (TEXTBOX_WIDTH + 2.0f), 1.0f / (TEXTBOX_HEIGHT + 2.0f), 1.0f);
					letter.localPosition = new Vector3(	-0.5f + (x + 1) / (TEXTBOX_WIDTH + 2.0f) + letter.localScale.x * 0.5f,
														0.5f - (y + 1) / (TEXTBOX_HEIGHT + 2.0f) - letter.localScale.y * 0.5f,
														-0.5f);
					
					letter_material[x, y] = letter.gameObject.GetComponent<Renderer>().material;
					letter_material[x, y].mainTextureScale = new Vector2(1.0f / 94.0f, 1.0f);
					letter_material[x, y].mainTextureOffset = new Vector2(0 / 94.0f, 0.0f);
				}
			}
			initialize = false;
		}
		textbox.localPosition = new Vector3(0.0f, -textbox_timer * camera_height * 2.0f, 0.0f);
		
		Vector3 background_position = textbox_background.localPosition;
		if (joystick_type == JOYSTICK_TYPE.SCREEN_PAD)
			background_position.x = 0.0f;
		else if (joystick_type == JOYSTICK_TYPE.VPAD_LEFT)
			background_position.x = (arrow_right.localPosition.x + camera_width) * 0.5f;
		else if (joystick_type == JOYSTICK_TYPE.VPAD_RIGHT)
			background_position.x = (arrow_left.localPosition.x - camera_width) * 0.5f;
		
		textbox_background.localPosition = background_position;
		
		/*
		if (textbox_open == false)
		{
			if (MAP_MESSAGES.is_there_new_message())
			{
				textbox_open = true;
				textbox_timer = 1.0f;
				for (int a = 0; a < MAP_MESSAGES.get_new_message_lines(); a++)
					textbox_line[a] = MAP_MESSAGES.get_new_message_line(a);
				for (int x = 0; x < TEXTBOX_WIDTH; x++)
					for (int y = 0; y < TEXTBOX_HEIGHT; y++)
						letter_material[x, y].mainTextureOffset = new Vector2(0 / 94.0f, 0.0f);
				textbox_x = -1;
				textbox_y = 0;
			}
			else
			{
				textbox_timer += Time.deltaTime * TEXTBOX_SPEED;
				if (textbox_timer > 1.0f) textbox_timer = 1.0f;
			}
		}
		else
		*/
							if (MAP_MESSAGES.is_there_new_message())
							{
								textbox_open = true;
								textbox_timer = 1.0f;
								for (int a = 0; a < MAP_MESSAGES.get_new_message_lines(); a++)
									textbox_line[a] = MAP_MESSAGES.get_new_message_line(a);
								for (int x = 0; x < TEXTBOX_WIDTH; x++)
									for (int y = 0; y < TEXTBOX_HEIGHT; y++)
										letter_material[x, y].mainTextureOffset = new Vector2(0 / 94.0f, 0.0f);
								textbox_x = -1;
								textbox_y = 0;
							}
							else if (textbox_open == false)
							{
								textbox_timer += Time.deltaTime * TEXTBOX_SPEED;
								if (textbox_timer > 1.0f) textbox_timer = 1.0f;
							}
							if (textbox_open == true)

		{
			textbox_timer -= Time.deltaTime * TEXTBOX_SPEED;
			if (textbox_timer < 0.0f) textbox_timer = 0.0f;
			if (textbox_timer < 0.10f)
			{
				text_timer += Time.deltaTime;
				if (text_timer > 0.4f / TEXTBOX_SPEED)
				{
					textbox_x++;
					text_timer = 0.0f;
					if (textbox_x < textbox_line[textbox_y].Length && textbox_x < TEXTBOX_WIDTH)
					{
						//Debug.Log("textbox x,y="+textbox_x+","+textbox_y);
						int letter_value = (int)textbox_line[textbox_y][textbox_x] - 32;
						letter_material[textbox_x, textbox_y].mainTextureOffset = new Vector2(letter_value / 94.0f, 0.0f);
						//Debug.Log("printing new letter="+textbox_line[textbox_x]);
					}
					else
					{
						textbox_x = -1;
						textbox_y++;
						if (textbox_y > TEXTBOX_HEIGHT - 1) textbox_y = TEXTBOX_HEIGHT - 1;
					}
				}
			}
			if (MAP_MESSAGES.time_to_hide_message())
			{
				textbox_open = false;
			}
		}
		
	}


}



public class MAP_MESSAGES
{
	private const int TEXTBOX_HEIGHT = 5;
	private static bool new_message = false;
	private static bool hide = false;
	private static int current_message_id = 0;
	private static string[] textline = new string[5];



	public static int SHOW_MESSAGE(string line_1)
	{
		set_new_message(line_1, "", "", "", "");
		return current_message_id;
	}
	public static int SHOW_MESSAGE(string line_1, string line_2)
	{
		set_new_message(line_1, line_2, "", "", "");
		return current_message_id;
	}
	public static int SHOW_MESSAGE(string line_1, string line_2, string line_3)
	{
		set_new_message(line_1, line_2, line_3, "", "");
		return current_message_id;
	}
	public static int SHOW_MESSAGE(string line_1, string line_2, string line_3, string line_4)
	{
		set_new_message(line_1, line_2, line_3, line_4, "");
		return current_message_id;
	}
	public static int SHOW_MESSAGE(string line_1, string line_2, string line_3, string line_4, string line_5)
	{
		set_new_message(line_1, line_2, line_3, line_4, line_5);
		return current_message_id;
	}



	private static void set_new_message(string line_1, string line_2, string line_3, string line_4, string line_5)
	{
		new_message = true;
		hide = false;
		if (textline[0] != line_1 ||
			textline[1] != line_2 ||
			textline[2] != line_3 ||
			textline[3] != line_4 ||
			textline[4] != line_5)
			current_message_id++;
			
		textline[0] = line_1;
		textline[1] = line_2;
		textline[2] = line_3;
		textline[3] = line_4;
		textline[4] = line_5;
	}



	public static void HIDE_MESSAGE()
	{
		for (int a = 0; a < TEXTBOX_HEIGHT; a++) textline[a] = "";
		new_message = false;
		hide = true;
	}



	public static void HIDE_MESSAGE(int id)
	{
		for (int a = 0; a < TEXTBOX_HEIGHT; a++) textline[a] = "";
		if (current_message_id == id)
		{
			new_message = false;
			hide = true;
		}
	}



	public static bool is_there_new_message()
	{
		bool value = new_message;
		new_message = false;
		return value;
	}



	public static bool time_to_hide_message()
	{
		bool value = hide;
		hide = false;
		return value;
	}


	public static int get_new_message_lines()
	{
		return TEXTBOX_HEIGHT;
	}



	public static string get_new_message_line(int index)
	{
		return textline[index];
	}


}