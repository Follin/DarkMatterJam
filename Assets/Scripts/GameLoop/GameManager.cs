using UnityEngine;

public class GameManager : MonoBehaviour
{
    HealthComponent _playerHealth;
       
    private int _amoutOfKills = 0;

    [SerializeField]
    int _killLimit = 10;

    bool _inBlackSpace = true; //Black space is normal space. White space is danger-zone    

    private void Awake()
    {
        _playerHealth = FindObjectOfType<HealthComponent>();
    }

    private void Start()
    {
        _playerHealth.ResetHealth();
    }

    private void Update()
    {
        if (_amoutOfKills >= _killLimit)
            _inBlackSpace = false;
    }

    public void AddKillToPlayer() => ++_amoutOfKills;

}
