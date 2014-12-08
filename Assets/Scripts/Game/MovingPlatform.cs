using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class MovingPlatform : GameBehaviour {

	[EnumMask]
	public Dimensions ActiveDimensions;
    public bool resetWhenActive;
    public bool hideWhenInactive;
    public GameObject[] hidingMeshes;
	
	private GameObject meshContainer;
	private Transform pointA;
	private Transform pointB;
    private Vector3 direction;
    private bool isActive;

	protected override void Awake()
	{
		base.Awake();

		meshContainer = gameObject.FindChild("Mesh");

		pointA = transform.FindChild ("pointA");
		pointB = transform.FindChild ("pointB");
		meshContainer.transform.position = pointA.position;
		direction = (pointB.position - pointA.position).normalized;
	}

    protected override void Update()
    {
        base.Update();

        if (isActive)
        {
            var dir = (meshContainer.transform.position - pointA.position);
            float currentDistance = (dir.x * dir.x) + (dir.y * dir.y) + (dir.z * dir.z);

            dir = (pointB.position - pointA.position);
            float distance = (dir.x * dir.x) + (dir.y * dir.y) + (dir.z * dir.z);
            dir = dir.normalized;

            if (dir != direction)
            {
                dir = (meshContainer.transform.position - pointB.position);
                currentDistance = (dir.x * dir.x) + (dir.y * dir.y) + (dir.z * dir.z);
            }

            if (currentDistance >= distance)
            {
                direction = -direction;
            }
            meshContainer.transform.Translate(direction * 4 * Time.deltaTime); 
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
            meshContainer.transform.position = pointA.transform.position;
        }
    }
	
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{	
		meshContainer = gameObject.FindChild("Mesh");
		pointA = transform.FindChild ("pointA");
		pointB = transform.FindChild ("pointB");
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine(pointA.position, pointB.position);
		Gizmos.DrawSphere(meshContainer.transform.position, .1f);
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere(pointA.position, .1f);
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(pointB.position, .1f);
	}
#endif
}
