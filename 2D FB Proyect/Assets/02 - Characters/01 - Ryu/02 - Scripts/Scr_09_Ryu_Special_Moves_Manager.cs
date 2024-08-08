using System.Collections.Generic;
using UnityEngine;

public class Scr_09_Ryu_Special_Moves_Manager : MonoBehaviour
{
    private Scr_01_Control_Manager controlManager;

    private HashSet<string> currentInputs = new HashSet<string>(); // Track active inputs
    private HashSet<string> previousInputs = new HashSet<string>(); // Track previous frame's inputs

    public List<string> inputTracker = new List<string>(); //Shows the current inputs

    private void Awake()
    {
        controlManager = GetComponent<Scr_01_Control_Manager>();
    }

    private void Update()
    {
        CaptureInputs();
    }

    private void CaptureInputs() // Keeps track of the inputs
    {
        // Clear current inputs at the start of each frame
        currentInputs.Clear();

        // Right Inputs
        if (controlManager.buttonRight) currentInputs.Add("Right");
        if (controlManager.buttonRightUpDiagonal) currentInputs.Add("RightUp");
        if (controlManager.buttonRightDownDiagonal) currentInputs.Add("RightDown");

        // Left Inputs
        if (controlManager.buttonLeft) currentInputs.Add("Left");
        if (controlManager.buttonLeftUpDiagonal) currentInputs.Add("LeftUp");
        if (controlManager.buttonLeftDownDiagonal) currentInputs.Add("LeftDownDiagonal");

        // Vertical Inputs
        if (controlManager.buttonUp) currentInputs.Add("Up");
        if (controlManager.buttonDown) currentInputs.Add("Down");

        // Punch Inputs
        if (controlManager.buttonLightPunch) currentInputs.Add("LightPunch");
        if (controlManager.buttonMediumPunch) currentInputs.Add("MediumPunch");
        if (controlManager.buttonHeavyPunch) currentInputs.Add("HeavyPunch");

        // Kick Inputs
        if (controlManager.buttonLightKick) currentInputs.Add("LightKick");
        if (controlManager.buttonMediumKick) currentInputs.Add("MediumKick");
        if (controlManager.buttonHeavyKick) currentInputs.Add("HeavyKick");

        // Add new inputs to the buffer only if they weren't in the previous frame's inputs
        foreach (string input in currentInputs)
        {
            if (!previousInputs.Contains(input))
            {
                inputTracker.Add(input);
            }
        }

        // Ensure the buffer size is limited to 10
        if (inputTracker.Count > 10)
        {
            inputTracker.RemoveAt(0);  // Remove the oldest input
        }

        // Update previousInputs to be the currentInputs for the next frame
        previousInputs.Clear();
        previousInputs.UnionWith(currentInputs);
    }
}
