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

    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _highscoreText;
    [SerializeField] Image _deathScreen;

    private void Awake()
    {
        _playerHealth = FindObjectOfType<HealthComponent>();
    }

    private void Start()
    {
        _playerHealth.ResetHealth();
        _sanityMeter = 0;
        _distance = 0;
        _deathScreen.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_sanityMeter >= 100)
            _inDarkSpace = false;

        IncreaseScore();

        if (Input.GetKeyDown(KeyCode.P)) Death();
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
        Time.timeScale = 1;

        _playerHealth.ResetHealth();
        _sanityMeter = 0;
        _distance = 0;
        _deathScreen.gameObject.SetActive(false);

        SceneManager.LoadScene(1);
    }
}
