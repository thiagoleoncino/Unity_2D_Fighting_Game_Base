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

    //Turn Variables
    public GameObject playerObjetive;
    public bool rightSide;

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

            if (stateManager.passiveAction)
            {

                TurnAround();

                //Idle and Horizontal Movement Code
                if (controlManager.idle || controlManager.buttonRight && controlManager.buttonLeft)
                {
                    universalPysicsManager.MoveCharacterFunction(0f);
                    actualAction = "Idle";
                }
                else if (controlManager.buttonRight)
                {
                    if (rightSide) 
                    {
                        universalPysicsManager.MoveCharacterFunction(characterStats.groundFowardSpeed);
                        actualAction = "MoveFoward";
                    }
                    else {
                        universalPysicsManager.MoveCharacterFunction(characterStats.groundBackwardSpeed);
                        actualAction = "MoveBackward";
                        }
                }
                else if (controlManager.buttonLeft)
                {
                    if (rightSide) 
                    {
                        universalPysicsManager.MoveCharacterFunction(-characterStats.groundBackwardSpeed);
                        actualAction = "MoveBackward"; 
                    }
                    else 
                    {
                        universalPysicsManager.MoveCharacterFunction(-characterStats.groundFowardSpeed);
                        actualAction = "MoveFoward"; 
                    }
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
                    if (rightSide)
                    {
                        universalPysicsManager.MoveCharacterFunction(characterStats.dashFowardSpeed);
                        actualAction = "DashFoward";
                    }
                    else
                    {
                        universalPysicsManager.MoveCharacterFunction(characterStats.dashBackwardSpeed);
                        actualAction = "DashBackward";
                    }
                }

                if (controlManager.buttonDashLeft)
                {
                    if (rightSide)
                    {
                        universalPysicsManager.MoveCharacterFunction(-characterStats.dashBackwardSpeed);
                        actualAction = "DashBackward";
                    }
                    else
                    {
                        universalPysicsManager.MoveCharacterFunction(-characterStats.dashFowardSpeed);
                        actualAction = "DashFoward";
                    }
                }

                //Normal Attacks Code
                if(controlManager.buttonLightPunch)
                {
                    actualAction = "LightPunch";
                    stateManager.semiCancelableAction = true;
                    universalPysicsManager.MoveCharacterFunction(0f);
                } //LP

                if (controlManager.buttonMediumPunch)
                {
                    actualAction = "MediumPunch";
                    stateManager.semiCancelableAction = true;
                    universalPysicsManager.MoveCharacterFunction(0f);
                } //MP

                if (controlManager.buttonHeavyPunch)
                {
                    actualAction = "HeavyPunch";
                    stateManager.semiCancelableAction = true;
                    universalPysicsManager.MoveCharacterFunction(0f);
                } //HP

                if (controlManager.buttonLightKick)
                {
                    actualAction = "LightKick";
                    stateManager.semiCancelableAction = true;
                    universalPysicsManager.MoveCharacterFunction(0f);
                } //LK

                if (controlManager.buttonMediumKick)
                {
                    actualAction = "MediumKick";
                    stateManager.semiCancelableAction = true;
                    universalPysicsManager.MoveCharacterFunction(0f);
                } //MK

                if (controlManager.buttonHeavyKick)
                {
                    actualAction = "HeavyKick";
                    stateManager.semiCancelableAction = true;
                    universalPysicsManager.MoveCharacterFunction(0f);
                } //HK
            }

            if (stateManager.cancelableAction)
            {
                //Actions from crouch
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

    public void TurnAround()
    {
        if (playerObjetive.transform.position.x > transform.position.x)
        {
            rightSide = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (playerObjetive.transform.position.x < transform.position.x)
        {
            rightSide = false;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    } //Turn around
}
