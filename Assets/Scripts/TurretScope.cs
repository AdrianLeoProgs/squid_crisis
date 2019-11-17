using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TurretScope : MonoBehaviour
{
    public SteamVR_Action_Boolean triggerPulled;
    
    public SteamVR_Action_Vibration vibration;

    public SteamVR_Input_Sources hand;

    private bool isShooting;

    public float fireRate = 15f;

    public float nextTimeToFire = 0f;

    void Start()
    {
        triggerPulled.AddOnStateDownListener(TriggerDown, hand);
        triggerPulled.AddOnStateUpListener(TriggerUp, hand);       
    }

    void FixedUpdate() 
    {
        print(GameManager.getInstance.squidMainHealth);
        if (isShooting && Time.time >= nextTimeToFire)
        {
            vibration.Execute(0, 1, 320, 1, hand);
            nextTimeToFire = Time.time + 1f / fireRate;
            Vector3 forward = transform.TransformDirection(Vector3.back) * 100;
            Debug.DrawRay(transform.position, forward, Color.green);
            RaycastHit hit;
            bool raycasthit = Physics.Raycast(transform.position, forward, out hit, 100);
            if (raycasthit)
            {
                bool colliderhit = hit.collider.gameObject.CompareTag("Target");
                if (colliderhit)
                {
                    // destroy target instance
                    Destroy(hit.collider.gameObject);
                }
            }

        }
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isShooting = false;
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isShooting = true;
    }
}
