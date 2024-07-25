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
    //Determines which player is in control
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

    [Space] //Movement Buttons Detection
    public bool buttonLeft;
    public bool buttonRight;
    public bool buttonUp;
    public bool buttonDown;

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
        //Variable that detects if an Action is being performed
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

    void ControlPlayer1()
    {
        //Movement Buttons
        UpdateButtonState(KeyCode.S, ref buttonDown); //Button Down
        UpdateButtonState(KeyCode.A, ref buttonLeft); //Button Left
        UpdateButtonState(KeyCode.W, ref buttonUp); //Button Up
        UpdateButtonState(KeyCode.D, ref buttonRight); //Button Right
    } //Player 1 Inputs

    void ControlPlayer2()
    {
        //Movement Buttons
        UpdateButtonState(KeyCode.DownArrow, ref buttonDown); //Button Down
        UpdateButtonState(KeyCode.LeftArrow, ref buttonLeft); //Button Left
        UpdateButtonState(KeyCode.UpArrow, ref buttonUp); //Button Up
        UpdateButtonState(KeyCode.RightArrow, ref buttonRight); //ButtonsRight
    } //Player 2 Inputs

}
