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

    void Awake()
    {
        controlManager = GetComponent<Scr_01_Control_Manager>();
        stateManager = GetComponent<Scr_02_State_Manager>();
        characterStats = GetComponent<Scr_03_Character_Stats>();
        universalPysicsManager = GetComponent<Scr_04_Universal_Physics_Manager>();
    }

    private void FixedUpdate()
    {

        //Passive Actions in the Ground
        if (stateManager.stateGrounded)
        {
            //Idle
            if (controlManager.idle || controlManager.buttonRight && controlManager.buttonLeft)
            {
                universalPysicsManager.MoveCharacterFunction(0f);
                actualAction = "Idle";
            }

            //Horizontal Movement Code
            if (controlManager.buttonRight)
            {
                universalPysicsManager.MoveCharacterFunction(characterStats.groundFowardSpeed);
            }
            else if (controlManager.buttonLeft)
            {
                universalPysicsManager.MoveCharacterFunction(-characterStats.groundBackwardSpeed);
            }

            //Jump
            if (controlManager.buttonUp)
            {
                universalPysicsManager.JumpCharacterFunction();
                controlManager.buttonUp = false;
                actualAction = "Jump";
            }
        }

    }
}
