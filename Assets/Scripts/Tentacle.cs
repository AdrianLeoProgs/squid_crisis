using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public List<GameObject> targets;

    public GameObject targetOrb;

    public Animator animator;

    public string attackTrigger;

    [SerializeField]
    private float startAttackTimer;

    void Start() 
    {
        if (startAttackTimer == 0f)
        {
            startAttackTimer = 5f;
        }

        foreach (GameObject target in targets)
        {
            Instantiate(targetOrb, target.transform.position, Quaternion.identity, target.transform);
        }
    }

    void Update() 
    {
        startAttackTimer -= Time.deltaTime;

        // destroy self and transistion animation if all targets are destroyed
        if (targets.Count < 1)
        {
            StartCoroutine("AnimationTransition");   
        }

        if (startAttackTimer < 0)
        {
            animator.SetBool(attackTrigger, true);
        }

    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.getInstance.playerHealth -= 1;
        }
    }
    void HitHealth() {
        GameManager.getInstance.playerHealth -= 1;
    }

    IEnumerator AnimationTransition()
    {
        if (animator.GetBool(attackTrigger))
        {
            animator.SetBool(attackTrigger, false);
            animator.SetBool("RetreatFromAttackTransition", true);
        }
        else
        {
            animator.SetBool(attackTrigger, false);
            animator.SetBool("RetreatTransition", true);
        }
        // yield wait can be altered to diff time when we figure out about how long the end animation usually takes
        yield return new WaitForSeconds(2f);
        Destroy(gameObject, 2);
    }

}
