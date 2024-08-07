using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerControlNumber
{
    player1,
    player2
}

public class Scr_01_Control_Manager : MonoBehaviour
{
    private Player_Input_Action controls;

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
    public bool buttonRightUpDiagonal;
    public bool buttonRight;
    public bool buttonRightDownDiagonal;
    [Space]
    public bool buttonDown;
    [Space]
    public bool buttonLeftDownDiagonal;
    public bool buttonLeft;
    public bool buttonLeftUpDiagonal;
    [Space]
    public bool buttonUp;
    [Space] // Double Tap Detection
    public bool buttonDashLeft;
    public bool buttonDashRight;
    [Space] // Attack Buttons Detection
    public bool buttonLightPunch;
    public bool buttonMediumPunch;
    public bool buttonHeavyPunch;
    [Space]
    public bool buttonLightKick;
    public bool buttonMediumKick;
    public bool buttonHeavyKick;

    private void Awake()
    {
        controls = new Player_Input_Action();
    }

    private void OnEnable()
    {
        controls.Enable();

        if (player1)
        {
            ControlPlayer1();
        }
        if (player2)
        {
            ControlPlayer2();
        }
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        idle = (!buttonLeft && !buttonRight && !buttonUp && !buttonDown); // Variable that detects if an Action is being performed
    }

    void ControlPlayer1()
    {
        // Movement Buttons
        InputActionBool(controls.Player1.Left, value => buttonLeft = value);
        InputActionBool(controls.Player1.Right, value => buttonRight = value);
        InputActionBool(controls.Player1.Up, value => buttonUp = value);
        InputActionBool(controls.Player1.Down, value => buttonDown = value);

        // Diagonal Down
        InputActionBool(controls.Player1.LeftDownDiagonal, value => buttonLeftDownDiagonal = value);
        InputActionBool(controls.Player1.RightDownDiagonal, value => buttonRightDownDiagonal = value);

        // Diagonal Up
        InputActionBool(controls.Player1.LeftUpDiagonal, value => buttonLeftUpDiagonal = value);
        InputActionBool(controls.Player1.RightUpDiagonal, value => buttonRightUpDiagonal = value);
;
        // Dash
        InputActionBool(controls.Player1.DashLeft, value => buttonDashLeft = value);
        InputActionBool(controls.Player1.DashRight, value => buttonDashRight = value);

        // Attack Punch Buttons
        InputActionBool(controls.Player1.LP, value => buttonLightPunch = value);
        InputActionBool(controls.Player1.MP, value => buttonMediumPunch = value);
        InputActionBool(controls.Player1.HP, value => buttonHeavyPunch = value);

        // Attack Kick Buttons
        InputActionBool(controls.Player1.LK, value => buttonLightKick = value);
        InputActionBool(controls.Player1.MK, value => buttonMediumKick = value);
        InputActionBool(controls.Player1.HK, value => buttonHeavyKick = value);

    } // Player 1 Inputs

    void ControlPlayer2()
    {

    } // Player 2 Inputs

    private void InputActionBool(InputAction inputAction, System.Action<bool> boolAction)
    {
        inputAction.performed += ctx => boolAction(true);
        inputAction.canceled += ctx => boolAction(false);
    }
}
