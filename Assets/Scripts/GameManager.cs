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

    //public GameObject squidModelOne;

    //public GameObject squidModelTwo;

    //public GameObject squidModelThree;

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
                    instance = (GameManager) FindObjectOfType(typeof(GameManager));
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

    void Start()
    {
        // Disable 2nd and 3rd phase squid models
        // squidModelOne.SetActive(true);
        //squidModelTwo.SetActive(false);
        //squidModelThree.SetActive(false);
    }

    void Update()
    {
        //if (SquidPhases.PHASE_TWO.Equals(_squidPhase))
        //{
        //    squidModelOne.SetActive(false);
        //    squidModelTwo.SetActive(true);
        //} 
        //else if (SquidPhases.PHASE_THREE.Equals(_squidPhase))
        //{
        //    squidModelTwo.SetActive(false);
        //    squidModelThree.SetActive(true);
        //}
    }
}
