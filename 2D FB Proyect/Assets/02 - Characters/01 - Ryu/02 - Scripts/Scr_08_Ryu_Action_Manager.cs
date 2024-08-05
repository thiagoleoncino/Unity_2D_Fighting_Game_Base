using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_08_Ryu_Action_Manager : MonoBehaviour
{
    //Extraction Scripts
    private Scr_01_Control_Manager controlManager;
    private Scr_02_State_Manager stateManager;
    private Scr_03_Character_Stats characterStats;
    private Scr_04_Universal_Physics_Manager universalPysicsManager;
    private Scr_05_Universal_Action_Manager universalActionManager;

    void Awake()
    {
        controlManager = GetComponent<Scr_01_Control_Manager>();
        stateManager = GetComponent<Scr_02_State_Manager>();
        characterStats = GetComponent<Scr_03_Character_Stats>();
        universalPysicsManager = GetComponent<Scr_04_Universal_Physics_Manager>();
        universalActionManager =GetComponent<Scr_05_Universal_Action_Manager>();
    }

    private void FixedUpdate()
    {
        //Standing Attacks
        if (stateManager.stateGrounded && stateManager.passiveAction || universalActionManager.actualAction == "Standing")
        {

            Attack(controlManager.buttonLightPunch, "LightPunch", 0f, 0f, true); //LP

            Attack(controlManager.buttonMediumPunch, "MediumPunch", 0f, 0f, true); //MP

            Attack(controlManager.buttonHeavyPunch, "HeavyPunch", 0f, 0f, true); //HP

            Attack(controlManager.buttonLightKick, "LightKick", 0f, 0f, true); //LK

            Attack(controlManager.buttonMediumKick, "MediumKick", 0f, 0f, true); //MK

            Attack(controlManager.buttonHeavyKick, "HeavyKick", 0f, 0f, true); //HK

            //Command Normals
            if (universalActionManager.rightSide)
            {
                Attack(controlManager.buttonRight && controlManager.buttonHeavyPunch, "FowardHeavyPunch", 0f, 0f, true);
            }
            if (!universalActionManager.rightSide)
            {
                Attack(controlManager.buttonLeft && controlManager.buttonHeavyPunch, "FowardHeavyPunch", 0f, 0f, true);
            }
            
        }

        //Jumping Attacks
        if (stateManager.stateAirborn && stateManager.passiveAction)
        {

            Attack(controlManager.buttonLightPunch, "JumpingLightPunch", 0f, 0f, false); //LP

            Attack(controlManager.buttonMediumPunch, "JumpingMediumPunch", 0f, 0f, false); //MP

            Attack(controlManager.buttonHeavyPunch, "JumpingHeavyPunch", 0f, 0f, false); //HP

            Attack(controlManager.buttonLightKick, "JumpingLightKick", 0f, 0f, false); //LK

            Attack(controlManager.buttonMediumKick, "JumpingMediumKick", 0f, 0f, false); //MK

            Attack(controlManager.buttonHeavyKick, "JumpingHeavyKick", 0f, 0f, false); //HK

        }

        //Crouch Attacks
        if (universalActionManager.crouching || universalActionManager.actualAction == "ToCrouch")
        {
            Attack(controlManager.buttonLightPunch, "CrouchLightPunch", 0f, 0f, true); //LP

            Attack(controlManager.buttonMediumPunch, "CrouchMediumPunch", 0f, 0f, true); //MP

            Attack(controlManager.buttonHeavyPunch, "CrouchHeavyPunch", 0f, 0f, true); //HP

            Attack(controlManager.buttonLightKick, "CrouchLightKick", 0f, 0f, true); //LK

            Attack(controlManager.buttonMediumKick, "CrouchMediumKick", 0f, 0f, true); //MK

            Attack(controlManager.buttonHeavyKick, "CrouchHeavyKick", 0f, 0f, true); //HK
        }
    }

    public void Attack(bool button, string Action, float MoveX, float MoveY, bool isStanding)
    {
        if (button)
        {
            universalActionManager.actualAction = Action;
            stateManager.semiCancelableAction = true;

            if(isStanding)
            {
                if (universalActionManager.rightSide)
                {
                    universalPysicsManager.MoveCharacterFunction(MoveX, MoveY);
                }
                if (!universalActionManager.rightSide)
                {
                    universalPysicsManager.MoveCharacterFunction(-MoveX, MoveY);
                }
            }
        }
    }
}
