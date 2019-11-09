using UnityEngine;

public class GameManager : MonoBehaviour
{
    HealthComponent _playerHealth;
       
    private int _amoutOfKills = 0;

    public int GetAmountOfKills => _amoutOfKills;

    private void Awake()
    {
        _playerHealth = FindObjectOfType<HealthComponent>();
    }

    private void Start()
    {
        _playerHealth.ResetHealth();
    }

}
