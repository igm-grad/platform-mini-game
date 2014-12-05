﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class pathShuriken : GameBehaviour {

	public Dimensions ActiveDimensions;
	public bool resetWhenActive;
	public bool hideWhenInactive;
	public GameObject[] hidingMeshes;
	public Transform[] waypoints;
	private GameObject meshContainer;
	private int i;

	private bool isActive;
    public bool isActive2;
	protected override void Awake()
	{
        isActive2 = false;
		base.Awake();
		i = 0;
		meshContainer = gameObject.FindChild("Mesh");
		meshContainer.transform.position = waypoints [0].position;
	}


	protected override void Update()
	{
		base.Update();
        Debug.Log("hello");
		if (isActive && isActive2)
		{
            if (i < waypoints.Length -1)
            {
                var dir = (meshContainer.transform.position - waypoints[i].position);
                float distance = (dir.x * dir.x) + (dir.y * dir.y) + (dir.z * dir.z);

                dir = (waypoints[i + 1].position - waypoints[i].position);
                float currentDistance = (dir.x * dir.x) + (dir.y * dir.y) + (dir.z * dir.z);

                Debug.Log(currentDistance.ToString());


                dir = dir.normalized;

                Debug.Log(distance.ToString());

                if (distance < currentDistance)
                {
                    meshContainer.transform.Translate(dir * 4 * Time.deltaTime);
                }
                else
                {
                    i++;
                }
            }
            else
            {
                
                i = 0;
                meshContainer.transform.position = waypoints[0].position;
                
            }

		}
		
	}

	public override void ShiftTo(Dimensions dimension)
	{
		base.ShiftTo(dimension);
		
		isActive = (dimension & ActiveDimensions) == dimension;
		
		// show when active or when inactive and we dont need to hide
		foreach (var mesh in hidingMeshes)
		{
			mesh.SetActive(isActive || !hideWhenInactive);
		}
		
		if (isActive && resetWhenActive)
		{
			meshContainer.transform.position = waypoints[0].transform.position;
		}
	}
}