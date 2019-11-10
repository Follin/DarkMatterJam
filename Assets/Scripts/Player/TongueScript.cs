using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueScript : MonoBehaviour
{
    [SerializeField]
    private Transform StartPosition;
    [SerializeField]
    private Transform EndPosition;

    private bool _tongueFired = false;
    private bool _retracting = false;


    private float _startTime;
    private float _journeyLength;
    private float _timer = 0;

    [SerializeField] float _toungTime = 0.3f;

    private void Start()
     {
        _startTime = Time.time;
        _journeyLength = Vector3.Distance(StartPosition.position, EndPosition.position);
        transform.position = StartPosition.position;
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space) && !_tongueFired)
        {
            _tongueFired = true;
            _timer = 0;
        }

        if(_tongueFired || _retracting)
        {
            _timer += Time.deltaTime * (_retracting ? -1f : 1f);
            Firetongue();
        }

        if(_timer >= _toungTime)
        {
            _retracting = true;
        }
               
        if (_retracting && _timer <= 0)
        {
            _tongueFired = false;
            _retracting = false;
        }
    }

    void Firetongue()
    {
        transform.position = Vector3.Lerp(StartPosition.position, EndPosition.position, _timer / _toungTime);
    }
}