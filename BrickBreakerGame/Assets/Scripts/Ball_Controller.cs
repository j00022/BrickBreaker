using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller : MonoBehaviour
{
    public bool onGround, lineDrawn;
    public int speed;


    void Start() {
        speed = 3;
        onGround = true;
        transform.position = new Vector2 (0, -3.783198f);
    }

    void Update() {
        if (onGround)
            transform.LookAt(Input.mousePosition);
        if (!onGround)
            transform.position += transform.forward * Time.deltaTime * speed;
        if (Input.GetMouseButtonUp(0))
            onGround = false;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            onGround = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else {
            //bounce off
        }
    }
}
