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

    public bool crouching;
    public bool blocking;

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
                    universalPysicsManager.MoveCharacterFunction(0f, 0f);
                    actualAction = "Idle";
                }
                else if (controlManager.buttonRight)
                {
                    if (rightSide) 
                    {
                        universalPysicsManager.MoveCharacterFunction(characterStats.groundFowardSpeed, universalPysicsManager.rigidBody.velocity.y);
                        actualAction = "MoveFoward";
                    }
                    else {
                        universalPysicsManager.MoveCharacterFunction(characterStats.groundBackwardSpeed, universalPysicsManager.rigidBody.velocity.y);
                        actualAction = "MoveBackward";
                        }
                }
                else if (controlManager.buttonLeft)
                {
                    if (rightSide) 
                    {
                        universalPysicsManager.MoveCharacterFunction(-characterStats.groundBackwardSpeed, universalPysicsManager.rigidBody.velocity.y);
                        actualAction = "MoveBackward"; 
                    }
                    else 
                    {
                        universalPysicsManager.MoveCharacterFunction(-characterStats.groundFowardSpeed, universalPysicsManager.rigidBody.velocity.y);
                        actualAction = "MoveFoward"; 
                    }
                }

                //Crouch Code
                if (controlManager.buttonDown || controlManager.buttonLeftDownDiagonal || controlManager.buttonRightDownDiagonal)
                {
                    actualAction = "ToCrouch";
                    universalPysicsManager.MoveCharacterFunction(0f,0f);
                    stateManager.cancelableAction = true;
                }

                //Dash Code
                if (controlManager.buttonDashRight)
                {
                    if (rightSide)
                    {
                        universalPysicsManager.MoveCharacterFunction(characterStats.dashFowardSpeed, universalPysicsManager.rigidBody.velocity.y);
                        actualAction = "DashFoward";
                    }
                    else
                    {
                        universalPysicsManager.MoveCharacterFunction(characterStats.dashBackwardSpeed, universalPysicsManager.rigidBody.velocity.y);
                        actualAction = "DashBackward";
                    }
                }

                if (controlManager.buttonDashLeft)
                {
                    if (rightSide)
                    {
                        universalPysicsManager.MoveCharacterFunction(-characterStats.dashBackwardSpeed, universalPysicsManager.rigidBody.velocity.y);
                        actualAction = "DashBackward";
                    }
                    else
                    {
                        universalPysicsManager.MoveCharacterFunction(-characterStats.dashFowardSpeed, universalPysicsManager.rigidBody.velocity.y);
                        actualAction = "DashFoward";
                    }
                }

                //Standing Block code
                if (controlManager.buttonBlock)
                {
                    universalPysicsManager.MoveCharacterFunction(0f, 0f);
                    stateManager.cancelableAction = true;
                    blocking = true;
                }
            }

            if (stateManager.cancelableAction)
            {
                //Actions from crouch
                if (crouching)
                {
                    actualAction = "Crouching";

                    if (!controlManager.buttonDown && !controlManager.buttonLeftDownDiagonal && !controlManager.buttonRightDownDiagonal)
                    {
                        actualAction = "Standing";
                        crouching = false;
                    }

                    if (controlManager.buttonBlock)
                    {
                        blocking = true;
                    }
                }

                //Actions from Block
                if (blocking)
                {
                    actualAction = "StandingBlock";

                    if (!controlManager.buttonBlock)
                    {
                        actualAction = "Idle";
                        blocking = false;
                        stateManager.passiveAction = true;
                    }

                    if (controlManager.buttonDown)
                    {
                        actualAction = "CrouchingBlock";
                        crouching = true;

                        if (!controlManager.buttonBlock)
                        {
                            actualAction = "Crouching";
                            blocking = false;
                        }
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
            // Jump
            if (totalJumps > 0)
            {
                if(controlManager.buttonUp || controlManager.buttonLeftUpDiagonal || controlManager.buttonRightUpDiagonal)
                {
                    Jump();
                }
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

    public void Jump()
    {
        if (controlManager.buttonRightUpDiagonal)
        {
            if (rightSide)
            {
                universalPysicsManager.MoveCharacterFunction(characterStats.jumpFowardSpeed, characterStats.jumpHeight);
            }
            else
            {
                universalPysicsManager.MoveCharacterFunction(characterStats.jumpBackwardSpeed, characterStats.jumpHeight);
            }
        }
        else if (controlManager.buttonLeftUpDiagonal)
        {
            if (rightSide)
            {
                universalPysicsManager.MoveCharacterFunction(-characterStats.jumpBackwardSpeed, characterStats.jumpHeight);
            }
            else
            {
                universalPysicsManager.MoveCharacterFunction(-characterStats.jumpFowardSpeed, characterStats.jumpHeight);
            }
        }
        else
        {
            universalPysicsManager.MoveCharacterFunction(0f, characterStats.jumpHeight);
        } // Neutral Jump

        controlManager.buttonUp = false;
        actualAction = "Jump";
        --totalJumps;
        stateManager.cancelableAction = true;
    }
}
