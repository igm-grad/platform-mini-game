using UnityEngine;
using System.Collections;


/// <summary>
/// References:
/// https://unity3d.com/pt/learn/tutorials/modules/beginner/2d/2d-controllers
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour {

    public float speed = 5f;
    public float gripTime = .5f;
    public LayerMask groundLayers;
    public LayerMask wallLayers;
    
    readonly Vector2 feetA = new Vector2(-.25f,  .05f);
    readonly Vector2 feetB = new Vector2( .25f, -.01f);

    bool isGrounded;
    bool isGripping;
    bool hasDoubleJump;

    Collider2D[] dumbColliders = new Collider2D[1];


	void FixedUpdate () 
    {

        var pos = (Vector2)transform.position;

        isGrounded = Physics2D.OverlapAreaNonAlloc(pos + feetA, pos + feetB, dumbColliders, groundLayers) > 0;
        isGripping = !isGrounded && Physics2D.OverlapAreaNonAlloc(pos + feetA, pos + feetB, dumbColliders, wallLayers) > 0;

        if (isGrounded || isGripping)
        {
            hasDoubleJump = true;
        }

        var horizontal = Input.GetAxis("Horizontal");

        rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            World.ShiftTo(Dimensions.Red);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            World.ShiftTo(Dimensions.Green);
        }


        if ((isGrounded || hasDoubleJump) && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isGrounded)
            {
                hasDoubleJump = false;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            }

            rigidbody2D.AddForce(new Vector2(0, 700));
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position + (Vector3)feetA, transform.position + (Vector3)feetB);
    }
}
