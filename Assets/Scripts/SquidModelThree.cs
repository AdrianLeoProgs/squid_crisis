using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidModelThree : MonoBehaviour
{

    public Animator globalAnimator;

    void Start()
    {
        globalAnimator.SetBool("PhaseThree", true);
    }
    
}
