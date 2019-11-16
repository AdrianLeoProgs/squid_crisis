using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TurretScope : MonoBehaviour
{
    public SteamVR_Action_Boolean triggerPulled;

    public SteamVR_Input_Sources hand;

    private bool isSquidHit;

    void Start()
    {
        isSquidHit = false;
        triggerPulled.AddOnStateDownListener(TriggerDown, hand);
        triggerPulled.AddOnStateUpListener(TriggerUp, hand);
    }

    void FixedUpdate() 
    {
        if (isSquidHit)
        {
            GameManager.getInstance().squidMainHealth -= 1;
        }
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isSquidHit = false;
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100) && hit.collider.gameObject.CompareTag("Squid"))
        {
            isSquidHit = true;
        }
        else
        {
            isSquidHit = false;
        }
    }
}
