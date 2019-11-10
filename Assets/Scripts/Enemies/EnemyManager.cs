using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameManager _gameManager;
    Rigidbody _rb;
       
    [SerializeField] private float _speedDarkWorld = 5f;
    [SerializeField] private float _speedWhiteWorld = 12f;

    [SerializeField] int _damage = 1;

    [SerializeField] Sprite _enemyDarkWorld;
    [SerializeField] Sprite _enemyWhiteWorld;
    HealthComponent _player;

    [SerializeField]
    private GameObject FMOD;
    private FMODManager fmodManager;

    private float counter;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _player = FindObjectOfType<HealthComponent>();
        _rb = GetComponent<Rigidbody>();
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _enemyDarkWorld;
        FMOD = GameObject.FindGameObjectWithTag("FMOD");
        fmodManager = FMOD.GetComponent<FMODManager>();

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
            transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().sprite = _enemyDarkWorld;

        }
        else 
        {
            transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().sprite = _enemyWhiteWorld; // SPRITE SYNS INTE

            transform.LookAt(_player.gameObject.transform);
            transform.position += transform.forward * _speedDarkWorld / 3 * Time.deltaTime;
        }

        counter += Random.Range(0, 10);
        if(counter > 1000)
        {
            counter = 0;
            fmodManager.EnemyAmbient();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tongue"))
        {
            _gameManager.AddKillToPlayer();
            fmodManager.EnemyDeath();
            // TODO: enemy death
            Destroy(gameObject);
        } 

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthComponent>().TakeDamage(_damage);
            fmodManager.EnemyDeath();
            // TODO: enemy death
            Destroy(gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DeathZone"))   
            Destroy(gameObject);        
    }
}
