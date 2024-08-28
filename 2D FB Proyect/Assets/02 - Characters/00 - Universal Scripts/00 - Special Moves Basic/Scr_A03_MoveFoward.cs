using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_A03_MoveFoward : MonoBehaviour
{
    private Rigidbody Rigidbody;

    public float velX;
    public float velY;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        // Move the object by setting its velocity
        Rigidbody.velocity = new Vector3(velX, velY, Rigidbody.velocity.z);
    }
}

