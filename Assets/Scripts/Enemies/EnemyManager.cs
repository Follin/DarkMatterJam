using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private float _speedDarkWorld = 5f;
    private float _speedWhiteWorld = 12f;
    private Rigidbody _rb;
    [SerializeField] int _damage = 1;
    GameManager _gameManager;
    [SerializeField] Sprite _enemyDarkWorld;
    [SerializeField] Sprite _enemyWhiteWorld;
    [SerializeField] Transform _player;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _rb = GetComponent<Rigidbody>();
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _enemyDarkWorld;

        if (!_rb)
        {
            Debug.LogError(gameObject.name + " has no rigidbody");
            return;
        }

    }
 
    void Update()
    { 

        if (_gameManager.InDarkWorld())
        {
            _rb.transform.position += -transform.up * _speedDarkWorld * Time.deltaTime;
        }
        else 
        {
            transform.LookAt(_player);
            transform.position += transform.forward * _speedWhiteWorld * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tongue"))
        {
            _gameManager.AddKillToPlayer();
            Debug.Log("kill enemy");

            Destroy(gameObject);
        }     

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthComponent>().TakeDamage(_damage);
            Debug.Log("take damage");

        }

        if (other.gameObject.CompareTag("DeathZone"))
        {
            Destroy(gameObject);
        }

    }

}
