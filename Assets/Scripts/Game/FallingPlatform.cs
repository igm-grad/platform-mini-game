using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;


public class FallingPlatform : GameBehaviour {

	public float timer=4.0f;
	public bool resetWhenActive;
	[EnumMask]
	public Dimensions ActiveDimensions;
	[EnumMask]
	public Dimensions FallingDimensions;

	bool isTimerTicking;
	bool isFalling;
	
	GameObject meshContainer;

	Vector3 platformCheckPoint;
	bool isFallingCheckPoint;
	bool isTimerTickingCheckPoint;
	bool isKinematicCheckPoint;
	float timerCheckPoint;
	
	protected override void Awake()
	{
		base.Awake();
		
		meshContainer = gameObject.FindChild("Mesh");
	}
	
	public override void ShiftTo(Dimensions dimension)
	{
		base.ShiftTo(dimension);
		var active = (dimension & ActiveDimensions) == dimension;
		meshContainer.SetActive(active);
		if(resetWhenActive && active)
		{
			LoadCheckpoint();
		}
	}


	void OnCollisionStay2D(Collision2D collision)
	{
		var player = collision.gameObject.GetComponent<CharacterController2D>();
		if(player != null)
		{
			var contact = collision.contacts[0];
			var normal = contact.normal;
			var willFall = (World.Dimension & FallingDimensions) == World.Dimension;
			if(willFall && normal == -Vector2.up)
			{
				isTimerTicking=true;
			} 
			else if(isFalling && normal == Vector2.up)
			{
				player.Interact(this);
			}
		}
	}

	protected override void Update()
	{
		if (isTimerTicking) 
		{
			timer -= Time.deltaTime / 2;
		}
		if(timer < 0.0)
		{
			rigidbody2D.isKinematic=false;
			isFalling=true;
		}
	}
	
	public override void SetCheckpoint ()
	{
		base.SetCheckpoint ();

		//save position, booleans, timer and rigidBody state
		platformCheckPoint = transform.position;
		isFallingCheckPoint = isFalling;
		isTimerTickingCheckPoint = isTimerTicking;
		isKinematicCheckPoint = rigidbody2D.isKinematic;
		timerCheckPoint = timer;

	}

	public override void LoadCheckpoint ()
	{
		base.LoadCheckpoint ();
		transform.position = platformCheckPoint;
		rigidbody2D.isKinematic = isKinematicCheckPoint;
		timer = timerCheckPoint;
		isFalling = isFallingCheckPoint;
		isTimerTicking = isTimerTickingCheckPoint;
	}

}