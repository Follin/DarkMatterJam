using UnityEngine;

public class GameManager : MonoBehaviour
{
    HealthComponent _playerHealth;

    private int _amoutOfKills = 0;

    [SerializeField]
    int _killLimit = 10;

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
        if (_amoutOfKills >= _killLimit)
            _inDarkSpace = false;
    }

    public void AddKillToPlayer() => ++_amoutOfKills;

    public bool InDarkWorld() => _inDarkSpace;
    


   /* transform.LookAt(Player);
     
     if(Vector3.Distance(transform.position, Player.position) >= MinDist){
     
          transform.position += transform.forward* MoveSpeed*Time.deltaTime;
 
           
           
          if(Vector3.Distance(transform.position, Player.position) <= MaxDist)
              {
                 //Here Call any function U want Like Shoot at here or something*/

}
