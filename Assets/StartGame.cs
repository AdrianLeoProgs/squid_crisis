using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public SteamVR_Action_Boolean triggerPulled;

    public SteamVR_Input_Sources hand;

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        print("Menu Select Trigger called");
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }
}
