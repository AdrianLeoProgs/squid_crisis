using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static readonly object padlock = new object();

    private static GameManager instance = null;

    [SerializeField] private int _playerHealth = 5;

    [SerializeField] private int _squidMainHealth = 100;

    [SerializeField] private SquidPhases _squidPhase = SquidPhases.PHASE_ONE;

    public GameObject squidModelOne;

    public GameObject squidModelTwo;

    public GameObject squidModelThree;

    // Private constructor
    private GameManager() {}

    // Ensure thread safety for Game Manager
    public static GameManager getInstance()
    {
        if (instance == null)
        {
            // synchronized zone
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
            }
        }

        return instance;
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

    public int squidMainHealth
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

    void Start()
    {
        // Disable 2nd and 3rd phase squid models
        squidModelOne.SetActive(true);
        squidModelTwo.SetActive(false);
        squidModelThree.SetActive(false);
    }

    void Update()
    {
        if (SquidPhases.PHASE_TWO.Equals(_squidPhase))
        {
            squidModelOne.SetActive(false);
            squidModelTwo.SetActive(true);
        } 
        else if (SquidPhases.PHASE_THREE.Equals(_squidPhase))
        {
            squidModelTwo.SetActive(false);
            squidModelThree.SetActive(true);
        }
    }
}
