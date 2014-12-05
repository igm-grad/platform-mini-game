﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class followingShuriken : GameBehaviour {

	[EnumMask]
	public Dimensions ActiveDimensions;
	public bool isActive;
	public bool resetWhenActive;
	public bool hideWhenInactive;

	private GameObject Player;
	private GameObject meshContainer;

	protected override void Awake ()
	{
		base.Awake ();
		Player = null;
		Player = GameObject.Find ("Player");
		if (Player == null) 
		{
			Debug.Log("player object not found");
		}
		meshContainer = gameObject.FindChild("Mesh");
		isActive = false;
	}

	protected override void Update ()
	{
		base.Update ();
		if (isActive)
		{
			var dir = Player.transform.position - meshContainer.transform.position;
			meshContainer.transform.Translate(dir * 2 * Time.deltaTime); 
		}
	}
}