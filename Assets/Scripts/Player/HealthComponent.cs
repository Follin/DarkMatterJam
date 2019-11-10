using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] int _maxHealth = 10;

    private int _playerHealth;
    private GameManager _gameManager;
    private Renderer renderer;

    private bool _tookDamage;
    private float _timer;

    void Awake()
    {
        _playerHealth = _maxHealth;
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start() {

        _tookDamage = false;
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Debug.Log(renderer.material.color);
        Debug.Log("Player Health: "+_playerHealth);
        if (_playerHealth <= 0) _gameManager.Death();

        Debug.Log("Timer: " + _timer);

        if(_tookDamage) {
            ChangeColor();
            _timer -= Time.deltaTime;
        }

        if (_timer <= 0) {
            _tookDamage = false;
            renderer.material.color = Color.white;
        }
    }

    public void TakeDamage(int damage) => _playerHealth -= damage;  

    public void ChangeColor() {
        renderer.material.color = Color.Lerp(Color.white, new Color(0.2f, 0.2f, 0.2f, 1), Mathf.PingPong(Time.time, 1));
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Enemy")) {
            _tookDamage = true;
            _timer = 1;
        }
    }

    public void ResetHealth() => _playerHealth = _maxHealth;
}
