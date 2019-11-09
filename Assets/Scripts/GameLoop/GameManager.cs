using UnityEngine;

public class GameManager : MonoBehaviour
{
    HealthComponent _playerHealth;

    private int _amoutOfKillsDark = 0;
    private int _amoutOfKillsWhite = 0;

    [SerializeField]
    int _killLimit = 10;

    private int _score;

    [SerializeField] bool _inDarkSpace = true; //Black space is normal space. White space is danger-zone    

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
        if (_amoutOfKillsDark >= _killLimit)
            _inDarkSpace = false;
    }

    public void AddKillToPlayer() => ++_amoutOfKillsDark;

    public bool InDarkWorld() => _inDarkSpace;  
    
    public void IncreaseScore()
    {
        _score++;
        if(_score > GetHighScore)
        {
            PlayerPrefs.SetInt("Highscore", _score);
        }
    }

    private int GetHighScore => PlayerPrefs.GetInt("Highscore");
}
