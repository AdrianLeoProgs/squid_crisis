using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class TurretScope : MonoBehaviour
{
    public SteamVR_Action_Boolean triggerPulled;

    public SteamVR_Action_Vibration vibration;

    public SteamVR_Input_Sources hand;

    private bool isShooting;

    public float fireRate = 15f;

    public float nextTimeToFire = 0f;

    public GameObject waterSplash;

    public GameObject targetHitSplash;

    public GameObject bodyHitSplash;

    public GameObject muzzleflashEffect;

    public GameObject smokeEffect;

    public Transform muzzlePivot;

    public Transform smokePivot;
    public AudioSource machineGunRapid;
    public AudioSource SquidHurtSound;


    private void Awake()
    {
        DontDestroyOnLoad(machineGunRapid);
    }
    void Start()
    {
        triggerPulled.AddOnStateDownListener(TriggerDown, hand);
        triggerPulled.AddOnStateUpListener(TriggerUp, hand);
    }

    void FixedUpdate()
    {

        if (isShooting && Time.time >= nextTimeToFire)
        {

            vibration.Execute(0, 1, 320, 1, hand);
            nextTimeToFire = Time.time + 1f / fireRate;
            Vector3 forward = transform.TransformDirection(Vector3.back) * 100;
            Debug.DrawRay(transform.position, forward, Color.green);
            Instantiate(muzzleflashEffect, muzzlePivot.position, muzzlePivot.rotation);
            Instantiate(smokeEffect, smokePivot.position, smokePivot.rotation);
            RaycastHit hit;
            bool raycasthit = Physics.Raycast(transform.position, forward, out hit,100000);
            if (raycasthit)
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.CompareTag("Target"))
                {
                    // destroy target instance
                    GameManager.getInstance.removeTarget(hitObject);
                    GameObject targetHitSplash = Instantiate(this.targetHitSplash, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(targetHitSplash, 2f);
                    SquidHurtSound.Play();

                }
                else if (hitObject.CompareTag("Water"))
                {
                    GameObject waterSplashes = Instantiate(waterSplash, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(waterSplashes, 2f);
                }
                else if (hitObject.CompareTag("Squid"))
                {
                    GameObject bodySplashes = Instantiate(bodyHitSplash, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(bodySplashes, 2f);
                    
                }
            }


        }
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isShooting = false;
        if (machineGunRapid != null)
        {
            machineGunRapid.Stop();
        }
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isShooting = true;
        if (machineGunRapid != null)
        {
            machineGunRapid.Play();

        }
    }
}
