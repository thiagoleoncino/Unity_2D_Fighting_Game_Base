using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GroundState
{
    Grounded,
    Airborn
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

