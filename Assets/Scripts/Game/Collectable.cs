using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class Collectable : GameBehaviour {

	[EnumMask]
	public Dimensions ActiveDimensions;
	[EnumMask]
	public Dimensions CollectableDimensions;

	public bool resetOnLoad;

	bool isActive;
	GameObject container;
	bool collected;

	bool cpCollected;

	protected override void Awake()
	{
		base.Awake();
		
		container = gameObject.FindChild("Container");
	}


	public override void ShiftTo(Dimensions dimension)
	{

		base.ShiftTo(dimension);

		isActive = (dimension & ActiveDimensions) == dimension;
		container.SetActive(isActive && !collected);

		if(isActive && !collected)
		{
			var mask = 1 << LayerMask.NameToLayer ("Player");
			
			var collider=Physics2D.OverlapArea ((Vector2)collider2D.bounds.max, (Vector2)collider2D.bounds.min, mask);
			if(collider !=null)
			{
				Collect (collider);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(!isActive || collected)
		{
			return;
		}

		Collect (collider);
	}


	private void Collect(Collider2D collider)
	{
		var player = Utils.GetPlayerFromCollider (collider);

		if(player != null)
		{
			var canCollect = (World.Dimension & CollectableDimensions) == World.Dimension;
			if(canCollect)
			{
				collected = true;
				container.SetActive(false);
			}
		}
	}

	public override void SetCheckpoint ()
	{
		base.SetCheckpoint ();

		cpCollected = collected;

	}
	
	public override void LoadCheckpoint ()
	{
		base.LoadCheckpoint ();
		if(resetOnLoad)
		{
			collected=cpCollected;
			container.SetActive(isActive && !collected);
		}
	}
}