using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_A02_DestroyObject : MonoBehaviour
{
    public bool byStage;
    public bool byPlayer;

    public AnimationClip animationClip;
    private Animator animator;
    private Rigidbody Rigidbody;


    private void Start()
    {
        animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayAnimationAndDestroy(other, byStage, "Layer_Stage");

        if (other.gameObject.tag == "Tag_Player1")
        {
            PlayAnimationAndDestroy(other, byPlayer, "Tag_Player2");
        }

        if (other.gameObject.tag == "Tag_Player2")
        {
            PlayAnimationAndDestroy(other, byPlayer, "Tag_Player1");
        }
    }

    private void PlayAnimationAndDestroy(Collider other, bool A, string B)
    {
        if(A && other.gameObject.layer == LayerMask.NameToLayer(B))
        {
            Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;

            // Reproduce la animación y luego destruye el objeto
            if (animator != null && animationClip != null)
            {
                animator.Play(animationClip.name);
                StartCoroutine(DestroyAfterAnimation());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animationClip.length);
        Destroy(gameObject);
    }
}
