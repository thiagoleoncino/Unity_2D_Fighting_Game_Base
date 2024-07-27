using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scr_07_Ryu_Animation_Manager : MonoBehaviour
{
    //Scripts
    private Scr_05_Universal_Action_Manager characterAction;

    //Component
    private Animator animator;

    private string actualAnimation;

    private Dictionary<string, string> animationDictionary = new Dictionary<string, string>
    {
        {"Idle",  "Anim_01_Ryu_Idle" },
        {"MoveFoward",  "Anim_02_Ryu_WalkFoward" },
        {"MoveBackward",  "Anim_03_Ryu_WalkBackwards" },
        {"Jump",  "Anim_04_Ryu_Jump" },
        {"Fall",  "Anim_05_Ryu_Fall" },
        {"Crouch",  "Anim_06_Ryu_Crouch" },
        {"Standing",  "Anim_07_Ryu_Standing" },
        {"DashFoward",  "Anim_08_Ryu_DashFoward" },
        {"DashBackward",  "Anim_09_Ryu_DashBackwards" },
    };

    void Awake()
    {
        characterAction = GetComponentInParent<Scr_05_Universal_Action_Manager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animationDictionary.TryGetValue(characterAction.actualAction, out string newAnimation))
        {
            ChangeAnimation(newAnimation);
        }
    }

    public void ChangeAnimation(string newAnimation) //Animation Change
    {
        if (actualAnimation == newAnimation) return;

        animator.Play(newAnimation);
        actualAnimation = newAnimation;
    }
}
