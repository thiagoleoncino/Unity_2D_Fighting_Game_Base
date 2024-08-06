using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_09_Ryu_SpecialMoves_Manager : MonoBehaviour
{
    public Scr_01_Control_Manager controlManager;

    [Header("Special Move Bools")]
    public bool specialMove1;
    public bool specialMove2;

    private int moveStep;
    private float moveTimer;
    public float moveTimeLimit = 0.5f; // Time limit to complete the sequence

    void Update()
    {
        // Example usage for specialMove1
        DetectSpecialMoves(3, new bool[] { controlManager.buttonDown, controlManager.buttonRight, controlManager.buttonLightPunch }, ref specialMove1);
        // Example usage for specialMove2
        DetectSpecialMoves(3, new bool[] { controlManager.buttonDown, controlManager.buttonLeft, controlManager.buttonHeavyKick }, ref specialMove2);
    }

    void DetectSpecialMoves(int inputNumber, bool[] inputBools, ref bool specialMove)
    {
        // Reset the move sequence if the time limit is exceeded
        if (Time.time - moveTimer > moveTimeLimit)
        {
            moveStep = 0;
        }

        // Detecting the sequence of inputs
        if (moveStep < inputNumber)
        {
            if (inputBools[moveStep])
            {
                moveStep++;
                moveTimer = Time.time;
            }
        }

        // If the sequence is correctly completed
        if (moveStep == inputNumber)
        {
            specialMove = true;
            moveStep = 0; // Reset after detection
        }
        else
        {
            specialMove = false;
        }
    }
}

