using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class GameSelectButton : MonoBehaviour {

    public int buttonLevel;
    Button button;
    bool initialized;

    //Checks levels if OnLevelWasLoaded was not called
    void Start ()
    {
        if (initialized == false)
        {
            CheckLevels();
        }
    }
    void OnLevelWasLoaded(int level)
    {
        CheckLevels();
    }
    void loadFrigginLevel ()
    {
        GlobalGameManager.GGM.StartGame(buttonLevel);
        //Debug.Log("kek");
    }

    //Creates buttons and checks if this level is playable
    void CheckLevels ()
    {
        /* If OnClick is assigned through the inspector,
         * the reference is lost upon scene load
         * So it's done through code */
        button = GetComponent<Button>();        
        button.onClick.AddListener(loadFrigginLevel); //Adds OnClick to button
        gameObject.SetActive(false);
        //Debug.Log(GameManager.manager.completedLevels.Any());
        //The first level is always shown regardless of its completetion
        if (GlobalGameManager.GGM.completedGames.Any() == false && buttonLevel == 0)
        {
            gameObject.SetActive(true);
        }
        //Checks if this or the previous level is completed, if previous is completed check if next level exists
        if (GlobalGameManager.GGM.completedGames.Contains(buttonLevel) ||
            GlobalGameManager.GGM.completedGames.Contains(buttonLevel - 1) && buttonLevel < GlobalGameManager.GGM.gameScenes.Length)
        {
            gameObject.SetActive(true);
        }
        initialized = true;
    }
}
