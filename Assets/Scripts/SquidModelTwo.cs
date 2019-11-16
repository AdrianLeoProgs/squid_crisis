using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidModelTwo : MonoBehaviour
{

    public Animator globalAnimator;

    void Start()
    {
        globalAnimator.SetBool("PhaseTwo", true);
    }
    
}
