using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_06_Universal_Animation_Events : MonoBehaviour
{
    //Scripts
    private Scr_02_State_Manager stateManager;

    void Awake()
    {
        stateManager = GetComponentInParent<Scr_02_State_Manager>();
    }

    public void PassiveActionEvent()
    {
        stateManager.passiveAction = true;
    }
}
