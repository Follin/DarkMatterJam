using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private Rigidbody _rb;
    [SerializeField] int _damage = 1;
    GameManager _gameManager;
    [SerializeField] Sprite _enemyDarkWorld;
    [SerializeField] Sprite _enemyWhiteWorld;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _rb = GetComponent<Rigidbody>();
        gameObject.GetComponent<SpriteRenderer>().sprite = _enemyDarkWorld;

        if (!_rb)
        {
            Debug.LogError(gameObject.name + " has no rigidbody");
            return;
        }

        
    }

    void EnemyMovement()
    {
        _rb.transform.position += -transform.up * _speed * Time.deltaTime;
    }
 
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tongue"))
        {
            _gameManager.AddKillToPlayer();
            Destroy(gameObject);
        }
            

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthComponent>().TakeDamage(_damage);
        }

    }


}
