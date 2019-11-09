using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {
    [SerializeField]
    private float _speed = 10f;

    void Update() {
        float movement_y = Input.GetAxis("Vertical") * _speed;
        float movement_x = Input.GetAxis("Horizontal") * _speed;
        movement_x *= Time.deltaTime;
        movement_y *= Time.deltaTime;
        transform.Translate(movement_x, movement_y, 0);
    }
}
