using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller : MonoBehaviour
{
    public bool onGround, lineDrawn;
    public int speed;
    public float angle;
    Rigidbody2D rb;
    Vector3 mousePos, objectPos;


    void Start() {
        speed = 6;
        onGround = true;
        transform.position = new Vector2 (0, -3.783198f); //On ground
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (onGround) {
            mousePos = Input.mousePosition;
            objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x -= objectPos.x;
            mousePos.y -= objectPos.y;

            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if (angle < 10 && angle > -90)
                angle = 10;
            if (angle > 170 || angle < -90)
                angle = 170;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        if (!onGround)
            rb.velocity = transform.right * speed;
        if (Input.GetMouseButtonUp(0))
            onGround = false;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            onGround = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else {
            transform.right = Vector3.Reflect(transform.right, collision.contacts[0].normal);
        }
    }

    public void OnDrawGizmos() {
        
    }
}
