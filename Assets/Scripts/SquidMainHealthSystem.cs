using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidMainBodyHealthSystem : MonoBehaviour
{
    private static readonly object padlock = new object();
    private static SquidMainBodyHealthSystem instance = null;
    [SerializeField] private int _health;

    // private constructor
    private SquidMainBodyHealthSystem() {}

    void Start()
    {
        _health = 100;
    }

    public static SquidMainBodyHealthSystem getInstance() 
    {
        if (instance == null)
        {
            // synchronized zone
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new SquidMainBodyHealthSystem();
                }
            }
        }

        return instance;
    }

    public int health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
        }
    }
}
