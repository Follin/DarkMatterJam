using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] int _maxHealth = 10;

    private int _playerHealth;
    private GameManager _gameManager;

    void Awake()
    {
        _playerHealth = _maxHealth;
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (_playerHealth <= 0) _gameManager.Death();
    }

    public void TakeDamage(int damage) => _playerHealth -= damage;  

    public void ResetHealth() => _playerHealth = _maxHealth;

}
