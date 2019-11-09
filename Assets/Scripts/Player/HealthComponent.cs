﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] int _maxHealth = 10;

    private int _playerHealth;    

    void Awake()
    {
        _playerHealth = _maxHealth;
    }

    void Update()
    {
        if (_playerHealth <= 0) Death();
    }

    void TakeDamage(int damage) => _playerHealth -= damage;

    void Death()
    {
        Debug.Log("Death");
    }
    
}