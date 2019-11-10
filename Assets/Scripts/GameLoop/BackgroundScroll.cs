using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float ScrollScpeed = -3f;

    Vector2 _startPosition;
    [SerializeField] GameObject _blackBackground;
    [SerializeField] GameObject _whiteBackground;
    [SerializeField] GameObject _sanityBackground;

    GameManager _manager;

    private void Awake()
    {
        _manager = FindObjectOfType<GameManager>();
        GetComponent<SpriteRenderer>().sprite = _blackBackground.GetComponent<SpriteRenderer>().sprite;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _blackBackground.GetComponent<SpriteRenderer>().sprite;
    }

    private void Start()
    {
        _startPosition = transform.position;

        Color temp = _sanityBackground.GetComponent<SpriteRenderer>().color;
        temp.a = 0f;
        _sanityBackground.GetComponent<SpriteRenderer>().color = temp;
    }

    private void Update()
    {
        if(_manager.InDarkWorld())
        {
            _sanityBackground.SetActive(true);
        }
        else
            _sanityBackground.SetActive(false);



        float newPosition = Mathf.Repeat(Time.time * ScrollScpeed, 10);
        transform.position = _startPosition + Vector2.up * newPosition;

        if(_manager.InDarkWorld())
        {
            GetComponent<SpriteRenderer>().sprite = _blackBackground.GetComponent<SpriteRenderer>().sprite;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _blackBackground.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = _whiteBackground.GetComponent<SpriteRenderer>().sprite;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _whiteBackground.GetComponent<SpriteRenderer>().sprite;
        }

        Color temp = _sanityBackground.GetComponent<SpriteRenderer>().color;
        temp.a = Mathf.Lerp(0, 1, _manager._sanityMeter / 100);
        _sanityBackground.GetComponent<SpriteRenderer>().color = temp;
    }
}
