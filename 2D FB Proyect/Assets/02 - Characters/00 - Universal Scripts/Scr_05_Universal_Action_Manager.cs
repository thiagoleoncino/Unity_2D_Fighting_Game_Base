 using UnityEngine;

public class Scr_05_Universal_Action_Manager : MonoBehaviour
{
    //Extraction Scripts
    private Scr_01_Control_Manager controlManager;
    private Scr_02_State_Manager stateManager;
    private Scr_03_Character_Stats characterStats;
    private Scr_04_Universal_Physics_Manager universalPysicsManager;

    //Basic Variables
    public string actualAction;

    public int totalJumps;

    void Awake()
    {
        controlManager = GetComponent<Scr_01_Control_Manager>();
        stateManager = GetComponent<Scr_02_State_Manager>();
        characterStats = GetComponent<Scr_03_Character_Stats>();
        universalPysicsManager = GetComponent<Scr_04_Universal_Physics_Manager>();
    }

    private void FixedUpdate()
    {
        //Actions in the Ground
        if (stateManager.stateGrounded)
        {
            totalJumps = characterStats.jumpAmount;

            if(stateManager.passiveAction)
            {
                //Idle and Horizontal Movement Code
                if (controlManager.idle || controlManager.buttonRight && controlManager.buttonLeft)
                {
                    universalPysicsManager.MoveCharacterFunction(0f);
                    actualAction = "Idle";
                }
                else if (controlManager.buttonRight)
                {
                    universalPysicsManager.MoveCharacterFunction(characterStats.groundFowardSpeed);
                    actualAction = "MoveFoward";
                }
                else if (controlManager.buttonLeft)
                {
                    universalPysicsManager.MoveCharacterFunction(-characterStats.groundBackwardSpeed);
                    actualAction = "MoveBackward";
                }

                //Crouch Code
                if (controlManager.buttonDown)
                {
                    actualAction = "Crouch";
                    stateManager.cancelableAction = true;
                }

                //Dash Code
                if (controlManager.buttonDashRight)
                {
                    universalPysicsManager.MoveCharacterFunction(characterStats.dashFowardSpeed);
                    actualAction = "DashFoward";
                }

                if (controlManager.buttonDashLeft)
                {
                    universalPysicsManager.MoveCharacterFunction(-characterStats.dashBackwardSpeed);
                    actualAction = "DashBackward";
                }
            }

            if (stateManager.cancelableAction)
            {
                //Character Standing from crouch
                if (actualAction == "Crouch")
                {
                    if (!controlManager.buttonDown)
                    {
                        actualAction = "Standing";
                    }
                }
            }
        }

        //Actions in the Air
        if (stateManager.stateAirborn)
        {
            if (stateManager.passiveAction)
            {
                actualAction = "Fall";
            }
        }

        //Actions outside the Ground Check

        //Passive Actions
        if (stateManager.passiveAction)
        {
            //Jump
            if (controlManager.buttonUp && totalJumps > 0)
            {
                universalPysicsManager.JumpCharacterFunction();
                controlManager.buttonUp = false;
                actualAction = "Jump";
                --totalJumps;
                stateManager.cancelableAction = true;
            }
        }

    }
}
