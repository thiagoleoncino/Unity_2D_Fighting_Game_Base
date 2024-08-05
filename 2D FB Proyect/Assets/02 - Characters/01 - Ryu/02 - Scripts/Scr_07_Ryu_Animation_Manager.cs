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
        {"ToCrouch",  "Anim_06_Ryu_ToCrouch" },
        {"Standing",  "Anim_07_Ryu_Standing" },
        {"DashFoward",  "Anim_08_Ryu_DashFoward" },
        {"DashBackward",  "Anim_09_Ryu_DashBackwards" },
        {"LightPunch",  "Anim_11_Ryu_NeutralLightPunch" },
        {"MediumPunch",  "Anim_12_Ryu_NeutralMediumPunch" },
        {"HeavyPunch",  "Anim_13_Ryu_NeutralHeavyPunch" },
        {"LightKick",  "Anim_14_Ryu_NeutralLightKick" },
        {"MediumKick",  "Anim_15_Ryu_NeutralMediumKick" },
        {"HeavyKick",  "Anim_16_Ryu_NeutralHeavyKick" },
        {"JumpingLightPunch",  "Anim_17_Ryu_JumpLightPunch" },
        {"JumpingMediumPunch",  "Anim_18_Ryu_JumpMediumPunch" },
        {"JumpingHeavyPunch",  "Anim_19_Ryu_JumpHeavyPunch" },
        {"JumpingLightKick",  "Anim_20_Ryu_JumpLightKick" },
        {"JumpingMediumKick",  "Anim_21_Ryu_JumpMediumKick" },
        {"JumpingHeavyKick",  "Anim_22_Ryu_JumpHeavyKick" },
        {"CrouchLightPunch",  "Anim_23_Ryu_CrouchLightPunch" },
        {"CrouchMediumPunch",  "Anim_24_Ryu_CrouchMediumPunch" },
        {"CrouchHeavyPunch",  "Anim_25_Ryu_CrouchHeavyPunch" },
        {"CrouchLightKick",  "Anim_26_Ryu_CrouchLightKick" },
        {"CrouchMediumKick",  "Anim_27_Ryu_CrouchMediumKick" },
        {"CrouchHeavyKick",  "Anim_28_Ryu_CrouchHeavyKick" },
        {"Crouching",  "Anim_29_Ryu_Crouching" },
        {"FowardHeavyPunch",  "Anim_B01_Ryu_FowardHeavyPunch" },
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
