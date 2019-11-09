using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] float _scrollScpeed = -5f;


    Vector2 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * _scrollScpeed, 10);
        transform.position = _startPosition + Vector2.up * newPosition;


    }
}
