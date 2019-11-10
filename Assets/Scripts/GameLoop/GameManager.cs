using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    HealthComponent _playerHealth;

    private int _amoutOfKillsDark = 0;
    private int _amoutOfKillsWhite = 0;
    private int _totalEnemiesToKill;

    private int _score;
    public float _sanityMeter = 0;

    private float _distance = 0;

    [SerializeField] bool _inDarkSpace = true; //Black space is normal space. White space is danger-zone 
    bool _isDead = false;
    bool _canSpawnWhite = false;

    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _highscoreText;
    [SerializeField] Image _deathScreen;

    [Header("Creating Enemies")]
    public GameObject[] EnemyPrefab;
    private float _enemuSpawnTimer;

    float _timeOfLastKill;
    [SerializeField] float _killTime = 2;
    [SerializeField] float _madness = 10;
    [SerializeField] float _sanityRegain = 3;

    private void Awake()
    {
        _playerHealth = FindObjectOfType<HealthComponent>();
    }

    private void Start()
    {
        _enemuSpawnTimer = 2f;
        _playerHealth.ResetHealth();
        _sanityMeter = 0;
        _distance = 0;
        _deathScreen.gameObject.SetActive(false);
        _isDead = false;
    }

    private void Update()
    {
        SanityUpdate();

       /* if (_canSpawnWhite)
        {
            _canSpawnWhite = false;
            SpawnInWhitePlace();
        }*/

        //if (InDarkWorld())
            SpawnEnemies();
     
        FromWhiteToDarkPlace();


        if (!_isDead)
            IncreaseScore();

        if(Time.time - _timeOfLastKill >= _killTime && _sanityMeter > 0)
        {
            _sanityMeter -= _sanityRegain * Time.deltaTime;
            _sanityMeter = Mathf.Clamp(_sanityMeter , 0, 100);
        }
    }

    public void AddKillToPlayer()
    {
        _timeOfLastKill = Time.time;
        _sanityMeter += _madness;

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

    void SpawnEnemies()
    {
        if (_enemuSpawnTimer > 0)
        {
            _enemuSpawnTimer -= Time.deltaTime;
        }

        if (_enemuSpawnTimer <= 0)
        {
            Instantiate(EnemyPrefab[Random.Range(0, EnemyPrefab.Length)], new Vector3(0, 15, 0), Quaternion.identity);
            _enemuSpawnTimer = 2;
        }
    }

    void SpawnInWhitePlace()
    {
        for (int i = 0; i < _totalEnemiesToKill; i++)
        {
            Instantiate(EnemyPrefab[Random.Range(0, EnemyPrefab.Length)], new Vector3(Random.Range(-7, 7), 15, 0), Quaternion.identity);
        }
    }

    void FromWhiteToDarkPlace()
    {
        if (_amoutOfKillsWhite >= _totalEnemiesToKill && !InDarkWorld())
        {
            _amoutOfKillsDark = 0;
            _sanityMeter = 0;
            _inDarkSpace = true;
        }
    }

    void SanityUpdate()
    {
        if (_sanityMeter >= 100)
        {
            _totalEnemiesToKill = _amoutOfKillsDark;
            _inDarkSpace = false;
        }
        else
            _amoutOfKillsWhite = 0;
    }


}
