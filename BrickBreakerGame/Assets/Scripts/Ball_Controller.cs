using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller : MonoBehaviour
{
    public bool onGround, drawLine;
    public int speed;
    public float angle;
    Rigidbody2D rb;
    LineRenderer line;
    Vector3 mousePos, objectPos;
    RaycastHit2D hit;


    void Start() {
        speed = 6;
        onGround = true;
        transform.position = new Vector2 (0, -3.783198f); //On ground
        rb = GetComponent<Rigidbody2D>();
        line = gameObject.AddComponent<LineRenderer>();
        line.widthMultiplier = 0.03f;
        line.positionCount = 3;
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
            DrawRay();
        }
        if (!onGround)
            rb.velocity = transform.right * speed;
        if (Input.GetMouseButtonUp(0)) {
            onGround = false;
            drawLine = false;
            DrawRay();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            onGround = true;
            drawLine = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else {
            transform.right = Vector3.Reflect(transform.right, collision.contacts[0].normal);
        }
    }

    public void DrawRay() {
        hit = Physics2D.CircleCast(transform.position, 0.161f, transform.right);
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hit.point);
        Vector3 rayReflect = Vector3.Reflect((Vector3)hit.centroid - line.GetPosition(0), hit.normal);
        hit = Physics2D.Raycast(hit.centroid, rayReflect.normalized, 4f);
        if ((Vector3)hit.point == Vector3.zero)
            line.SetPosition(2, line.GetPosition(1) + rayReflect.normalized * 4f);
        else
            line.SetPosition(2, line.GetPosition(1) + rayReflect.normalized * hit.distance);
    }
}
