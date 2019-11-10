using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Collision detection!
public class PlayerMovement : MonoBehaviour {
    Rigidbody _rb;
    [SerializeField]
    private float _speed = 2f;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    void Update() {
        float movement_y = Input.GetAxis("Vertical") * _speed;
        float movement_x = Input.GetAxis("Horizontal") * _speed;

        _rb.transform.position += new Vector3(movement_x * _speed * Time.deltaTime, movement_y * _speed * Time.deltaTime, 0);
    }
}
