using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool m_shuttingDown = false;

    private static readonly object padlock = new object();

    private static GameManager instance;

    [SerializeField] private int _playerHealth = 5;

    [SerializeField] private float _squidMainHealth = 100f;

    [SerializeField] private SquidPhases _squidPhase = SquidPhases.PHASE_ONE;

    public Animator animator;

    public GameObject winCanvas;

    public GameObject loseCanvas;

    public List<GameObject> phaseOneTentacleSet;

    // Ensure thread safety for Game Manager
    public static GameManager getInstance
    {
        get
        {
            if (m_shuttingDown)
            {
                return null;
            }
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = (GameManager)FindObjectOfType(typeof(GameManager));
                    if (instance == null)
                    {
                        print("in here");
                        var singleObj = new GameObject();
                        instance = singleObj.AddComponent<GameManager>();
                        singleObj.name = "GameManager";
                        DontDestroyOnLoad(singleObj);
                    }
                }
            }
            return instance;
        }
    }

    // Getters/Setters
    public int playerHealth
    {
        get
        {
            return _playerHealth;
        }

        set
        {
            _playerHealth = value;
        }
    }

    public float squidMainHealth
    {
        get
        {
            return _squidMainHealth;
        }

        set
        {
            _squidMainHealth = value;
        }
    }

    public SquidPhases squidPhase
    {
        get
        {
            return _squidPhase;
        }

        set
        {
            _squidPhase = value;
        }
    }

    void Awake()
    {
        foreach (GameObject tentacle in phaseOneTentacleSet)
        {
            tentacle.SetActive(false);
        }

        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
    }

    void Start()
    {
        // Enable first phase
        animator.SetBool("PhaseOne", true);
    }

    void Update()
    {
        // Always enable the tentacle in tentacleSet (since list will change dynamically)
        if (phaseOneTentacleSet.Count > 0 && !phaseOneTentacleSet[0].activeSelf)
        {
            phaseOneTentacleSet[0].SetActive(true);
        }

        // Check for tentacle set count of current phase (more can be added later)
        if (phaseOneTentacleSet.Count < 1)
        {
            // For now since we are only doing one phase, killing all tentacles in this will beat the boss
           GameManager.getInstance.squidMainHealth -= 100;
            //"PhaseTwo" in this case can just be the boss end phase until we feel more confident we can add additional phases
           squidPhase = SquidPhases.PHASE_TWO;
           WinCondition();
       }
        //Debug.Log("Player Health: " + playerHealth);
        if (playerHealth < 1)
        {
            LoseCondition();
        }

        // Setting boss phases
        //if (SquidPhases.PHASE_TWO.Equals(_squidPhase))
        //{
        //    animator.SetBool("PhaseOne", false);
        //    animator.SetBool("PhaseTwo", true);
        //}
        //// May never get to this point
        //else if (SquidPhases.PHASE_THREE.Equals(_squidPhase))
        //{
        //    animator.SetBool("PhaseTwo", false);
        //    animator.SetBool("PhaseThree", true);
        //}
    }

    public void removeTarget(GameObject target)
    {
        Debug.Log("Removing target: " + target, target);
        // Remove target
        if (phaseOneTentacleSet != null && phaseOneTentacleSet[0] != null)
        {
            Debug.Log("Removing target...");
           Transform TargetToDestroy = target.transform.GetChild(1);
           ParticleSystem TargetParticle = TargetToDestroy.GetComponentInChildren<ParticleSystem>();
           TargetParticle.Stop();
            phaseOneTentacleSet[0].GetComponent<Tentacle>().targets.Remove(target);

            // If targets are less than 1, remove the tentacle from the tentacle set
            if (phaseOneTentacleSet[0].GetComponent<Tentacle>().targets.Count < 1)
            {
                phaseOneTentacleSet.Remove(phaseOneTentacleSet[0]);
            }
        }
    }

    public void WinCondition()
    {
        winCanvas.SetActive(true);
        loseCanvas.SetActive(false);
    }

    public void LoseCondition()
    {
        loseCanvas.SetActive(true);
        winCanvas.SetActive(false);
    }
}

// Win: X: -0.44406 Y: 8.12 Z: -1.1101

// Lose: 
