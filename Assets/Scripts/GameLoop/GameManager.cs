using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    HealthComponent _playerHealth;

    private int _amoutOfKillsDark = 0;
    private int _amoutOfKillsWhite = 0;

    private int _score;
    private float _sanityMeter = 0;

    private float _distance = 0;

    [SerializeField] bool _inDarkSpace = true; //Black space is normal space. White space is danger-zone 
    bool _isDead = false;

    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _highscoreText;
    [SerializeField] Image _deathScreen;

    [Header("Creating Enemies")]
    public GameObject[] EnemyPrefab;
    private float _timer;

    private void Awake()
    {
        _playerHealth = FindObjectOfType<HealthComponent>();
    }

    private void Start()
    {
        _timer = 2f;
        _playerHealth.ResetHealth();
        _sanityMeter = 0;
        _distance = 0;
        _deathScreen.gameObject.SetActive(false);
        _isDead = false;
    }

    private void Update()
    {
        if (_timer > 0) {
            _timer -= Time.deltaTime;
        }

        if (_timer <= 0) {
            Instantiate(EnemyPrefab[1], new Vector3(0, 15, 0), Quaternion.identity);
            _timer = 2;
        }

        if (_sanityMeter >= 100)
            _inDarkSpace = false;

        if (!_isDead)
            IncreaseScore();
    }

    public void AddKillToPlayer()
    {
        if (_inDarkSpace) ++_amoutOfKillsDark;
        else
            ++_amoutOfKillsWhite;
    }

    public bool InDarkWorld() => _inDarkSpace;  
    
    private void IncreaseScore()
    {
        _distance += Time.deltaTime;
        _score += (int)_distance;

        if (_score > GetHighScore)        
            PlayerPrefs.SetInt("Highscore", _score);        
    }

    private int GetHighScore => PlayerPrefs.GetInt("Highscore");

    public void Death()
    {
        _isDead = true;
        _deathScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
        _scoreText.text = "Your score: " + _score;
        _highscoreText.text = "Highscore: " + GetHighScore;        
    }

    void SanityUpdate()
    {
        //TODO: FIX
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        _isDead = false;
        Time.timeScale = 1;

        _playerHealth.ResetHealth();
        _sanityMeter = 0;
        _distance = 0;
        _deathScreen.gameObject.SetActive(false);

        SceneManager.LoadScene(1);
    }
}
