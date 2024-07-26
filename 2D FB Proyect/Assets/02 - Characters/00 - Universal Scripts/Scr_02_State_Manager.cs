using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GroundState
{
    Grounded,
    Airborn
}

public enum ActionType
{
    Passive,
    Cancelable,
    SemiCancelable,
    NonCancelable
}

public class Scr_02_State_Manager : MonoBehaviour
{
    // Ground Check Components & Variables
    private BoxCollider boxCollider;
    private LayerMask groundLayer;

    [Header("Character Ground States")]

    public GroundState currentGroundState;

    public bool stateGrounded
    {
        get => currentGroundState == GroundState.Grounded;
        set { if (value) currentGroundState = GroundState.Grounded; }
    }
    public bool stateAirborn
    {
        get => currentGroundState == GroundState.Airborn;
        set { if (value) currentGroundState = GroundState.Airborn; }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(boxCollider.bounds.center, Vector3.down,
            boxCollider.bounds.extents.y + 0.1f, groundLayer);
    }

    // Character State Components & Variables
    [Header("Character Action States")]

    public ActionType currentState;

    public bool passiveAction
    {
        get => currentState == ActionType.Passive;
        set { if (value) currentState = ActionType.Passive; }
    }

    public bool cancelableAction
    {
        get => currentState == ActionType.Cancelable;
        set { if (value) currentState = ActionType.Cancelable; }
    }

    public bool semiCancelableAction
    {
        get => currentState == ActionType.SemiCancelable;
        set { if (value) currentState = ActionType.SemiCancelable; }
    }

    public bool noCancelableAction
    {
        get => currentState == ActionType.NonCancelable;
        set { if (value) currentState = ActionType.NonCancelable; }
    }


    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        groundLayer = LayerMask.GetMask("Layer_Stage");
    }

    void Update()
    {
        stateGrounded = IsGrounded(); // Grounded
        stateAirborn = !IsGrounded(); // Airborn

    }
}
