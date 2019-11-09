using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] int _maxHealth;

    private int _playerHealth;
    

    void Awake()
    {
        _playerHealth = _maxHealth;
    }
    
    void Update()
    {
        
    }
}
