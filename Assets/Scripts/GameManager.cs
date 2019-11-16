using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static readonly object padlock = new object();

    private static GameManager instance = null;

    [SerializeField] private int _playerHealth = 5;

    [SerializeField] private int _squidMainHealth = 100;

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

    // Getters/Setters for health
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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
