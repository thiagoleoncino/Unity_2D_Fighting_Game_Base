using UnityEngine;

public class Scr_04_Universal_Physics_Manager : MonoBehaviour
{
    //Components
    private Rigidbody rigidBody;

    //Scripts
    private Scr_03_Character_Stats characterStats;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        characterStats = GetComponent<Scr_03_Character_Stats>();
    }

    public void MoveCharacterFunction(float speed)
    {
        rigidBody.velocity = new Vector3(speed, rigidBody.velocity.y, rigidBody.velocity.z);
    } //Horizontal movement function

    public void JumpCharacterFunction()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, characterStats.jumpHeight, rigidBody.velocity.z);
    } //Jump movement function

}
