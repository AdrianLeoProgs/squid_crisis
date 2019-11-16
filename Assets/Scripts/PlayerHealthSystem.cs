using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerHealthSystem : MonoBehaviour
{
    private static readonly object padlock = new object();
    private static PlayerHealthSystem instance = null;
    [SerializeField] private int _health;

    // private constructor
    private PlayerHealthSystem() {}

    void Start() 
    {
        _health = 5;
    }

    public static PlayerHealthSystem getInstance() 
    {
        if (instance == null) 
        {
            // synchronized zone
            lock (padlock)
            {
                if (instance == null) 
                {
                    instance = new PlayerHealthSystem();
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
