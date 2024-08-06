using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerControlNumber
{
    player1,
    player2
}

public class Scr_01_Control_Manager : MonoBehaviour
{
    [Header("Player Bool")]
    // Determines which player is in control
    public PlayerControlNumber ActualPlayer;
    public bool player1
    {
        get => ActualPlayer == PlayerControlNumber.player1;
        set { if (value) ActualPlayer = PlayerControlNumber.player1; }
    }
    public bool player2
    {
        get => ActualPlayer == PlayerControlNumber.player2;
        set { if (value) ActualPlayer = PlayerControlNumber.player2; }
    }

    [Header("Action Bool")]
    public bool idle;

    [Space] // Movement Buttons Detection
    public bool buttonLeft;
    public bool buttonRight;
    public bool buttonUp;
    public bool buttonDown;
    public bool buttonLeftDownDiagonal;
    public bool buttonRightDownDiagonal;
    public bool buttonLeftUpDiagonal;
    public bool buttonRightUpDiagonal;

    [Space] // Double Tap Detection
    private bool tapping;
    private float lastTap;
    private KeyCode lastKey = KeyCode.None;
    private float tapTime = 0.3f; // The time to detect the double tap

    public bool buttonDashLeft;
    public bool buttonDashRight;

    [Space] // Attack Buttons Detection
    public bool buttonLightPunch;
    public bool buttonMediumPunch;
    public bool buttonHeavyPunch;

    public bool buttonLightKick;
    public bool buttonMediumKick;
    public bool buttonHeavyKick;

    void Update()
    {
        if (player1)
        {
            ControlPlayer1();
        }

        if (player2)
        {
            ControlPlayer2();
        }

        idle = (!buttonLeft && !buttonRight && !buttonUp && !buttonDown);
        // Variable that detects if an Action is being performed
    }

    void UpdateButtonState(KeyCode key, ref bool buttonState)
    {
        if (Input.GetKeyDown(key))
        {
            buttonState = true;
        }
        if (Input.GetKeyUp(key))
        {
            buttonState = false;
        }
    }

    void DoubleTap(KeyCode key, ref bool buttonState)
    {
        if (Input.GetKeyDown(key))
        {
            if (!tapping)
            {
                tapping = true;
                lastKey = key;
                StartCoroutine(SingleTap());
            }
            else if (lastKey == key && (Time.time - lastTap) < tapTime)
            {
                tapping = false;
                buttonState = true;
            }
            lastTap = Time.time;
        }
    }

    IEnumerator SingleTap()
    {
        yield return new WaitForSeconds(tapTime);
        if (tapping)
        {
            tapping = false;
        }
    }

    void ControlPlayer1()
    {
        // Movement Buttons
        UpdateButtonState(KeyCode.S, ref buttonDown); // Button Down
        UpdateButtonState(KeyCode.A, ref buttonLeft); // Button Left
        UpdateButtonState(KeyCode.W, ref buttonUp); // Button Up
        UpdateButtonState(KeyCode.D, ref buttonRight); // Button Right

        // Diagonal Movements
        buttonLeftDownDiagonal = Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A);
        buttonRightDownDiagonal = Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D);
        buttonLeftUpDiagonal = Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A);
        buttonRightUpDiagonal = Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D);

        if (buttonLeftDownDiagonal)
        {
            buttonDown = false;
            buttonLeft = false;
            buttonRightDownDiagonal = false;
        }
        if (buttonRightDownDiagonal)
        {
            buttonDown = false;
            buttonRight = false;
            buttonLeftDownDiagonal = false;
        }
        if (buttonLeftUpDiagonal)
        {
            buttonUp = false;
            buttonLeft = false;
        }
        if (buttonRightUpDiagonal)
        {
            buttonUp = false;
            buttonRight = false;
        }

        DoubleTap(KeyCode.A, ref buttonDashLeft);
        DoubleTap(KeyCode.D, ref buttonDashRight);

        // Attack Buttons
        UpdateButtonState(KeyCode.U, ref buttonLightPunch); // LP
        UpdateButtonState(KeyCode.I, ref buttonMediumPunch); // MP
        UpdateButtonState(KeyCode.O, ref buttonHeavyPunch); // HP

        UpdateButtonState(KeyCode.J, ref buttonLightKick); // LK
        UpdateButtonState(KeyCode.K, ref buttonMediumKick); // MK
        UpdateButtonState(KeyCode.L, ref buttonHeavyKick); // HK
    } // Player 1 Inputs

    void ControlPlayer2()
    {
        // Movement Buttons
        UpdateButtonState(KeyCode.DownArrow, ref buttonDown); // Button Down
        UpdateButtonState(KeyCode.LeftArrow, ref buttonLeft); // Button Left
        UpdateButtonState(KeyCode.UpArrow, ref buttonUp); // Button Up
        UpdateButtonState(KeyCode.RightArrow, ref buttonRight); // Button Right

        // Diagonal Movements
        buttonLeftDownDiagonal = Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow);
        buttonRightDownDiagonal = Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow);
        buttonLeftUpDiagonal = Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow);
        buttonRightUpDiagonal = Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow);

        if (buttonLeftDownDiagonal)
        {
            buttonDown = false;
            buttonLeft = false;
            buttonRightDownDiagonal = false;
        }
        if (buttonRightDownDiagonal)
        {
            buttonDown = false;
            buttonRight = false;
            buttonLeftDownDiagonal = false;
        }
        if (buttonLeftUpDiagonal)
        {
            buttonUp = false;
            buttonLeft = false;
        }
        if (buttonRightUpDiagonal)
        {
            buttonUp = false;
            buttonRight = false;
        }

        DoubleTap(KeyCode.LeftArrow, ref buttonDashLeft);
        DoubleTap(KeyCode.RightArrow, ref buttonDashRight);

        // Add attack button inputs for player 2 if needed
    } // Player 2 Inputs
}
