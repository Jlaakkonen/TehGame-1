using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchActivate : MonoBehaviour
{
    public List<SwitchTarget> targets;

    //Activates all targets
    protected virtual void Activate ()
    {
        if (targets != null)
        {
            foreach (SwitchTarget item in targets)
            {
                item.Action();
            }
        }
    }
}
