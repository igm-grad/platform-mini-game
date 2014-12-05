using UnityEngine;
using System.Collections;


/// <summary>
/// References:
/// https://unity3d.com/pt/learn/tutorials/modules/beginner/2d/2d-controllers
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : GameBehaviour {

    public float speed = 5f;
    public float gripTime = .5f;
    public LayerMask groundLayers;
    public LayerMask wallLayers;
    
    readonly Vector2 feetA = new Vector2(-.23f,  .05f);
    readonly Vector2 feetB = new Vector2( .23f, -.01f);

    bool isGrounded;
    bool isGripping;
    bool hasDoubleJump;
    public bool DoubleJumpEnabled;

    Vector3 checkpointPosition;

    Collider2D[] dumbColliders = new Collider2D[1];


	protected override void FixedUpdate () 
    {
        base.FixedUpdate();

        var pos = (Vector2)transform.position;

        isGrounded = Physics2D.OverlapAreaNonAlloc(pos + feetA, pos + feetB, dumbColliders, groundLayers) > 0;
        if(isGrounded)
        {
            transform.parent = dumbColliders[0].transform;
        }
        else
        {
            transform.parent = null;
        }

        isGripping = !isGrounded && Physics2D.OverlapAreaNonAlloc(pos + feetA, pos + feetB, dumbColliders, wallLayers) > 0;

        if ((isGrounded || isGripping) && DoubleJumpEnabled)
        {
            hasDoubleJump = true;
        }

        var horizontal = Input.GetAxis("Horizontal");

        rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);
	}

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.parent = null;
			rigidbody2D.WakeUp();
			World.ShiftTo(Dimensions.Red);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
			transform.parent = null;
			rigidbody2D.WakeUp();
			World.ShiftTo(Dimensions.Green);
        }


        if ((isGrounded || hasDoubleJump && DoubleJumpEnabled) && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isGrounded)
            {
                hasDoubleJump = false;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            }

            rigidbody2D.AddForce(new Vector2(0, 700));
        }

    }

    public override void SetCheckpoint()
    {
        base.SetCheckpoint();

        checkpointPosition = transform.position;
    }

    public override void LoadCheckpoint()
    {
        base.LoadCheckpoint();

        transform.position = checkpointPosition;
        rigidbody2D.velocity = Vector2.zero;
    }

    public void Interact(GameBehaviour behaviour)
    {
        if (behaviour.GetType() == typeof(AbysmalPit))
        {
            World.LoadCheckpoint();
        }

		if (behaviour.GetType() == typeof(Spike))
		{
			World.LoadCheckpoint();
		}

		if (behaviour.GetType() == typeof(FallingPlatform))
		{
			World.LoadCheckpoint();
		}
    }

//    void OnDrawGizmos()
//    {
//        Gizmos.color = Color.magenta;
//        Gizmos.DrawLine(transform.position + (Vector3)feetA, transform.position + (Vector3)feetB);
//    }
}
