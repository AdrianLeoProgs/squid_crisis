using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TurretScope : MonoBehaviour
{

    public SteamVR_Action_Boolean triggerPulled;

    public SteamVR_Input_Sources hand;

    void Start()
    {
        triggerPulled.AddOnStateDownListener(TriggerDown, hand);
        triggerPulled.AddOnStateUpListener(TriggerUp, hand);
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is up");
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");
    }
}
