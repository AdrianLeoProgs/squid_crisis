using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public List<GameObject> targets;

    public Animator animator;

    public string attackBool;

    void Start() 
    {
        
    }

    void Update() 
    {
        // destroy self and transistion animation if all targets are destroyed
        if (targets.Count < 1)
        {
            StartCoroutine("AnimationTransition");   
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.getInstance.playerHealth -= 1;
        }
    }

    IEnumerator AnimationTransition()
    {
        if (animator.GetBool(attackBool))
        {
            animator.SetBool("RetreatFromAttackTransition", true);
        }
        else
        {
            animator.SetBool("RetreatTransition", true);
        }
        // yield wait can be altered to diff time when we figure out about how long the end animation usually takes
        yield return new WaitForSeconds(2f);
        Destroy(gameObject, 2);
    }

}
