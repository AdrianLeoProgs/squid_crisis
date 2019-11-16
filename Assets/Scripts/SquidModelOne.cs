using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidModelOne : MonoBehaviour
{

    public Animator globalAnimator;

    void Start()
    {
        globalAnimator.SetBool("PhaseOne", true);
    }

}
