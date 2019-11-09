using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float _speed = 2f;

    void Update() {
        float movement_y = Input.GetAxis("Vertical") * _speed;
        float movement_x = Input.GetAxis("Horizontal") * _speed;

        transform.position = transform.position + new Vector3(movement_x * _speed * Time.deltaTime, movement_y * _speed * Time.deltaTime, 0);
    }
}
