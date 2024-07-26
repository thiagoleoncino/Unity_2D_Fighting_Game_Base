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
        }

        //Passive Actions in the Ground
        if (stateManager.stateGrounded && stateManager.passiveAction)
        {
            //Idle and Horizontal Movement Code
            if (controlManager.idle || controlManager.buttonRight && controlManager.buttonLeft)
            {
                universalPysicsManager.MoveCharacterFunction(0f);
                actualAction = "Idle";
            }
            else if (controlManager.buttonRight)
            {
                universalPysicsManager.MoveCharacterFunction(1f);
                actualAction = "MoveFoward";
            }
            else if (controlManager.buttonLeft)
            {
                universalPysicsManager.MoveCharacterFunction(-1f);
                actualAction = "MoveBackward";
            }
        }

        //Jump
        if (controlManager.buttonUp && totalJumps > 0)
        {
            universalPysicsManager.JumpCharacterFunction();
            controlManager.buttonUp = false;
            actualAction = "Jump";
            --totalJumps;
        }

    }
}
