using System.Collections.Generic;
using UnityEngine;

public class Scr_09_Ryu_Special_Moves_Manager : MonoBehaviour
{
    private Scr_01_Control_Manager controlManager;
    private Scr_02_State_Manager stateManager;
    private Scr_05_Universal_Action_Manager universalActionManager;

    public HashSet<string> currentInputs = new HashSet<string>();   // Track active inputs
    private HashSet<string> previousInputs = new HashSet<string>(); // Track previous frame's inputs

    private List<float> inputTimes = new List<float>();             // Tracks times for each input
    public List<string> inputTracker = new List<string>();          // Tracks inputs (visible in editor)

    private const float CommandTimer = 0.7f;        // Maximum time allowed between inputs
    private int lastSuccessfulInputIndex = -1;      // Track the index of the last successful input sequence

    // Special Bools
    public bool Special1; // Especial 1

    private void Awake()
    {
        controlManager = GetComponent<Scr_01_Control_Manager>();
        stateManager = GetComponent<Scr_02_State_Manager>();
        universalActionManager = GetComponent<Scr_05_Universal_Action_Manager>();
    }

    private void CaptureInputs() // Keeps track of the inputs
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
            inputTracker.RemoveAt(0);  // Remove the oldest input
            inputTimes.RemoveAt(0);    // Remove the corresponding time
        }

        previousInputs.Clear();
        previousInputs.UnionWith(currentInputs);
    }

    private void AddInput(bool condition, string input)
    {
        if (condition)
        {
            currentInputs.Add(input);
            if (!previousInputs.Contains(input))
            {
                inputTracker.Add(input);
                inputTimes.Add(Time.time);
                lastSuccessfulInputIndex = -1; // Reset the last successful index when new input is added
            }
        }
    }

    private void Update()
    {
        CaptureInputs();

        if (stateManager.stateGrounded && stateManager.passiveAction)
        {
            CheckSpecialMove(new List<string> { "Down", "Right", "LightPunch" }, ref Special1);
        }

        if (Special1)
        {
            ExecuteSpecialMove1();
        }
    }

    private void CheckSpecialMove(List<string> specialCommand, ref bool specialMoveBool)
    {
        if (!specialMoveBool && MatchesSequence(specialCommand))
        {
            specialMoveBool = true;
            lastSuccessfulInputIndex = inputTracker.Count - 1; // Store the index of the last input used for the sequence
        }
    }

    private bool MatchesSequence(List<string> sequence)
    {
        if (inputTracker.Count < sequence.Count)
            return false;

        float firstInputTime = inputTimes[inputTracker.Count - sequence.Count];
        float lastInputTime = inputTimes[inputTracker.Count - 1];

        // Check if inputs are within the allowed time frame
        if (lastInputTime - firstInputTime > CommandTimer)
            return false;

        // Check if the last entries in inputTracker match the sequence
        for (int i = 0; i < sequence.Count; i++)
        {
            if (inputTracker[inputTracker.Count - sequence.Count + i] != sequence[i])
                return false;
        }

        // Ensure the same sequence doesn't trigger the special move again unless new inputs were added
        if (inputTracker.Count - sequence.Count <= lastSuccessfulInputIndex)
            return false;

        return true;
    }

    private void ExecuteSpecialMove1()
    {
        universalActionManager.actualAction = "Special1";
        stateManager.noCancelableAction = true;
    }
}
