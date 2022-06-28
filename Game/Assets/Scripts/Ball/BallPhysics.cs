using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{

    Vector3 dir;
    public float speed = 5f;

    public float radius;

    public LayerMask notBall;

    // Start is called before the first frame update
    void Start()
    {
        dir = ballStartVelocity();
    }

    private void Update()
    {
        transform.Translate(dir.normalized * Time.deltaTime * speed);

        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, notBall);

        if(collider != null)
        {
            CheckCollider(collider);
        }
    }

    void CheckCollider(Collider2D col)
    {
        if (col.gameObject.CompareTag("Paddle"))
        {
            Vector2 newDir = transform.position - col.transform.position;
            if(dir.x > 0)
            {
                if(newDir.x > 0)
                {
                    return;
                }

                dir = newDir;
            } else
            {
                if (newDir.x < 0)
                {
                    return;
                }
                dir = newDir;
            }
        }
        else
        {
            dir = new Vector2(dir.x, -dir.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            dir = new Vector2(-dir.x, transform.position.y - collision.gameObject.transform.position.y);
        }
        else
        {
            dir = new Vector2(dir.x, -dir.y);
        }
    }

    Vector2 ballStartVelocity()
    {
        int xDir = Random.Range(0, 2) == 0 ? 1 : -1;
        Vector2 ballEndVelocity = new Vector2(xDir, Random.Range(-1f, 1f));
        return ballEndVelocity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
