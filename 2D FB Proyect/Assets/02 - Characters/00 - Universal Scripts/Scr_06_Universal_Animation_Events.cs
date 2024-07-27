using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_06_Universal_Animation_Events : MonoBehaviour
{
    //Scripts
    private Scr_01_Control_Manager controlManager;
    private Scr_02_State_Manager stateManager;
    private Scr_04_Universal_Physics_Manager universalPysicsManager;



    void Awake()
    {
        controlManager = GetComponentInParent<Scr_01_Control_Manager>();
        stateManager = GetComponentInParent<Scr_02_State_Manager>();
        universalPysicsManager = GetComponentInParent<Scr_04_Universal_Physics_Manager>();
    }

    public void PassiveActionEvent()
    {
        stateManager.passiveAction = true;
    }

    public void EndDashEvent()
    {
        controlManager.buttonDashLeft = false;
        controlManager.buttonDashRight = false;
        stateManager.cancelableAction = true;
        universalPysicsManager.MoveCharacterFunction(0f);
    }
}
