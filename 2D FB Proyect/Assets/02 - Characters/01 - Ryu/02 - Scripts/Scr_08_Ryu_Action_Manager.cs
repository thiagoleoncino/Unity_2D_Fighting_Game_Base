using System.Collections;
using System;
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

    //Input Tracker
    public HashSet<string> currentInputs = new HashSet<string>(); 
    private HashSet<string> previousInputs = new HashSet<string>();

    private List<float> inputTimes = new List<float>();             
    public List<string> inputTracker = new List<string>();

    private const float commandTimer = 0.7f;        
    private int lastSuccessfulInputIndex = -1;      

    //Special Moves Bools
    public bool specialMoveActive;
    public bool Special1;
    public bool Special2;

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
        CaptureInputs();
        
        // Special Moves
        if (stateManager.stateGrounded && (stateManager.passiveAction || stateManager.cancelableAction))
        {
            SpecialAttack(new List<string> { "Down", "Right", "LightPunch" }, ref Special1, SpecialMove1Function);

            SpecialAttack(new List<string> { "Down", "Left", "LightKick" }, ref Special2, SpecialMove2Function);
        }

        if (specialMoveActive)
        {
            return;
        } //Prevents overlap

        // Normal Moves
        if (stateManager.stateGrounded)
        {
            if (stateManager.passiveAction || universalActionManager.actualAction == "Standing")
            {
                //Standing Attacks
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

            if (universalActionManager.crouching || universalActionManager.actualAction == "ToCrouch")
            {
                //Crouch Attacks
                Attack(controlManager.buttonLightPunch, "CrouchLightPunch", 0f, 0f, true); //LP
                Attack(controlManager.buttonMediumPunch, "CrouchMediumPunch", 0f, 0f, true); //MP
                Attack(controlManager.buttonHeavyPunch, "CrouchHeavyPunch", 0f, 0f, true); //HP
                Attack(controlManager.buttonLightKick, "CrouchLightKick", 0f, 0f, true); //LK
                Attack(controlManager.buttonMediumKick, "CrouchMediumKick", 0f, 0f, true); //MK
                Attack(controlManager.buttonHeavyKick, "CrouchHeavyKick", 0f, 0f, true); //HK
            }
        }

        if (stateManager.stateAirborn && stateManager.passiveAction)
        {
            //Jump Attacks
            Attack(controlManager.buttonLightPunch, "JumpingLightPunch", 0f, 0f, false); //LP
            Attack(controlManager.buttonMediumPunch, "JumpingMediumPunch", 0f, 0f, false); //MP
            Attack(controlManager.buttonHeavyPunch, "JumpingHeavyPunch", 0f, 0f, false); //HP
            Attack(controlManager.buttonLightKick, "JumpingLightKick", 0f, 0f, false); //LK
            Attack(controlManager.buttonMediumKick, "JumpingMediumKick", 0f, 0f, false); //MK
            Attack(controlManager.buttonHeavyKick, "JumpingHeavyKick", 0f, 0f, false); //HK
        }
    }

    private void CaptureInputs()
    {
        currentInputs.Clear();

        AddInput(controlManager.buttonRight, "Right");
        AddInput(controlManager.buttonRightUpDiagonal, "RightUp");
        AddInput(controlManager.buttonRightDownDiagonal, "RightDown");

        AddInput(controlManager.buttonLeft, "Left");
        AddInput(controlManager.buttonLeftUpDiagonal, "LeftUp");
        AddInput(controlManager.buttonLeftDownDiagonal, "LeftDownDiagonal");

        AddInput(controlManager.buttonUp, "Up");
        AddInput(controlManager.buttonDown, "Down");

        AddInput(controlManager.buttonLightPunch, "LightPunch");
        AddInput(controlManager.buttonMediumPunch, "MediumPunch");
        AddInput(controlManager.buttonHeavyPunch, "HeavyPunch");

        AddInput(controlManager.buttonLightKick, "LightKick");
        AddInput(controlManager.buttonMediumKick, "MediumKick");
        AddInput(controlManager.buttonHeavyKick, "HeavyKick");

        // Ensure the buffer size is limited to 10
        if (inputTracker.Count > 10)
        {
            inputTracker.RemoveAt(0);       // Remove the oldest input
            inputTimes.RemoveAt(0);         // Remove the corresponding time
        }

        previousInputs.Clear();
        previousInputs.UnionWith(currentInputs);
    }

    private void AddInput(bool actionBool, string inputOutput)
    {
        if (actionBool)
        {
            currentInputs.Add(inputOutput);
            if (!previousInputs.Contains(inputOutput))
            {
                inputTracker.Add(inputOutput);
                inputTimes.Add(Time.time);
                lastSuccessfulInputIndex = -1;
            }
        }
    }

    private bool MatchesSequence(List<string> sequence)
    {
        if (inputTracker.Count < sequence.Count)
            return false;

        float firstInputTime = inputTimes[inputTracker.Count - sequence.Count];
        float lastInputTime = inputTimes[inputTracker.Count - 1];

        if (lastInputTime - firstInputTime > commandTimer)
            return false;

        for (int i = 0; i < sequence.Count; i++)
        {
            if (inputTracker[inputTracker.Count - sequence.Count + i] != sequence[i])
                return false;
        }

        if (inputTracker.Count - sequence.Count <= lastSuccessfulInputIndex)
            return false;

        return true;
    }

    public void Attack(bool button, string Action, float MoveX, float MoveY, bool isStanding)
    {
        if (button && !specialMoveActive)
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

    private void SpecialAttack(List<string> specialCommand, ref bool specialMoveBool, Action specialMoveAction)
    {
        if (!specialMoveBool && MatchesSequence(specialCommand))
        {
            specialMoveBool = true;
            lastSuccessfulInputIndex = inputTracker.Count - 1;
            specialMoveActive = true;
        }

        if (specialMoveBool)
        {
            specialMoveAction();
        }
    }

    void SpecialMove1Function()
    {
        universalActionManager.actualAction = "Special1";
        stateManager.noCancelableAction = true;
    }

    void SpecialMove2Function()
    {
        universalActionManager.actualAction = "Special2";
        stateManager.noCancelableAction = true;
    }
}
