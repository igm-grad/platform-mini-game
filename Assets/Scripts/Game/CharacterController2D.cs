using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour {

    public float speed = 5f;
    public LayerMask groundLayers;
    public Collider2D[] groundColliders = new Collider2D[1];

    readonly Vector2 feetA = new Vector2(-.25f,  .05f);
    readonly Vector2 feetB = new Vector2( .25f, -.01f);

    bool isGrounded;
    bool hasDoubleJump;

	void FixedUpdate () 
    {

        var pos = (Vector2)transform.position;
        isGrounded = Physics2D.OverlapAreaNonAlloc(pos + feetA, pos + feetB, groundColliders, groundLayers) > 0;

        if (isGrounded)
        {
            hasDoubleJump = true;
        }

        var horizontal = Input.GetAxis("Horizontal");

        rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);
	}

    void Update()
    {
        if ((isGrounded || hasDoubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isGrounded)
            {
                hasDoubleJump = false;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            }

            rigidbody2D.AddRelativeForce(new Vector2(0, 700));
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position + (Vector3)feetA, transform.position + (Vector3)feetB);
    }
}
