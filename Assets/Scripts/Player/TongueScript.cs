using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueScript : MonoBehaviour
{
    [SerializeField]
    private Transform StartPosition;
    [SerializeField]
    private Transform EndPosition;
    [SerializeField]
    private float _speed = 2f;
    private bool _tongueFired = false;
    private float _startTime;
    private float _journeyLength;
    private float _timer = 1;

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
            _timer = 1;
        }

        if(_tongueFired)
        {
            Firetongue();
        }

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }

        if(_timer <= 0)
        {
            transform.position = Vector3.Lerp(EndPosition.position, StartPosition.position, 2);
        }

        if(transform.position == StartPosition.position)
        {
            _tongueFired = false;
        }
    }

    void Firetongue()
    {
        transform.position = Vector3.Lerp(StartPosition.position, EndPosition.position, 2);
    }
}