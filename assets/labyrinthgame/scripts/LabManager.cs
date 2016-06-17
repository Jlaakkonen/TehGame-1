using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LabManager : GameManager
{
    bool levelComplete;

    //Singleton check
    protected override void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
        //Debug.Log("managero " + manager);
    }
    public override void PlayerWin()
    {
		if (manager.completedLevels.Contains(currentLevel) == false)
		{
			completedLevels.Add(currentLevel);
		}

        base.PlayerWin();
    }
    public override void PlayerLose()
    {
        base.PlayerLose();
    }
    public override void CheckForWin(bool hasKisse)
    {
        if (hasKisse)
        {
            PlayerWin();
        }
    }
}