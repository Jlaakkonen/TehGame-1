using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GlobalGameManager : MonoBehaviour
{
    //Do global functions
    public static GlobalGameManager GGM;
    public int gameSelectScene;
    public int[] gameScenes;
    int currentGame;
    public List<int> completedGames;
	//public List<int> completetlevels;

    //Singleton check
    public void Awake()
    {
        if (GGM == null)
        {
            DontDestroyOnLoad(gameObject);
            GGM = this;
        }
        else if (GGM != this)
        {
            Destroy(gameObject);
        }
    }

    public void GoToGameSelect()
    {
        SceneManager.LoadScene(gameSelectScene);
    }

    public void StartGame(int gameIndex)
    {
        currentGame = gameIndex;
        SceneManager.LoadScene(gameScenes[gameIndex]);
    }

    public void CompleteCurrentGame()
    {
        completedGames.Add(currentGame);
    }

	public void StartMapScene()
	{
		SceneManager.LoadScene ("map_scene");
	}
}
