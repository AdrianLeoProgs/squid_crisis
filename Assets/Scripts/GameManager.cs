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

    public List<List<GameObject>> phaseOneTentacleSet;

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
        for (List<GameObject> tentacles : phaseOneTentacleSet)
        {
            for (tentacle : tentacles)
            {
                tentacle.SetEnabled(false);
            }
        }
    }

    void Start()
    {
        // Enable first phase
        animator.SetBool("PhaseOne", true);
    }

    void Update()
    {
        // Remove tentacle list from tentacleSet if said list is empty (shift the "first" index)
        if (phaseOneTentacleSet.First().Count < 1)
        {
            phaseOneTentacleSet.Remove(phaseOneTentacleSet.First());
        }

        // Always enable the first List of tentacles in tentacleSet (since list will change dynamically)
        for (GameObject tentacle : phaseOneTentacleSet.First())
        {
            tentacle.SetEnabled(true);
        }

        // Check for tentacle set count of current phase (more can be added later)
        if (animator.GetBool("PhaseOne") && phaseOneTentacleSet.Count < 1)
        {
            // For now since we are only doing one phase, killing all tentacles in this will beat the boss
            GameManager.getInstance.squidMainHealth -= 100;
            // "PhaseTwo" in this case can just be the boss end phase until we feel more confident we can add additional phases
            squidPhase = SquidPhases.PHASE_TWO;
        }

        // Setting boss phases
        if (SquidPhases.PHASE_TWO.Equals(_squidPhase))
        {
            animator.SetBool("PhaseOne", false);
            animator.SetBool("PhaseTwo", true);
        }
        // May never get to this point
        else if (SquidPhases.PHASE_THREE.Equals(_squidPhase))
        {
            animator.SetBool("PhaseTwo", false);
            animator.SetBool("PhaseThree", true);
        }
    }
}
