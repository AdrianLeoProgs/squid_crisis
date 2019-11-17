using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidAction : MonoBehaviour
{
    public List<GameObject> targets;

    void Start()
    {
        
    }

    void Update()
    {
        if (targets.Count < 1)
        {
            GameManager.getInstance.squidMainHealth -= 40;
            
            switch (GameManager.getInstance.squidPhase)
            {
                case SquidPhases.PHASE_ONE:
                    GameManager.getInstance.squidPhase = SquidPhases.PHASE_TWO;
                    break;

                case SquidPhases.PHASE_TWO:
                    GameManager.getInstance.squidPhase = SquidPhases.PHASE_THREE;
                    break;
            }
        }
    }
}
