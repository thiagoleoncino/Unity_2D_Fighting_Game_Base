using UnityEngine;

public class Scr_04_Universal_Physics_Manager : MonoBehaviour
{
    //Components
    public Rigidbody rigidBody;

    //Scripts
    private Scr_03_Character_Stats characterStats;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        characterStats = GetComponent<Scr_03_Character_Stats>();
    }

    public void MoveCharacterFunction(float speedX, float speedY)
    {
        rigidBody.velocity = new Vector3(speedX, speedY, rigidBody.velocity.z);
    } //Horizontal movement function

}
