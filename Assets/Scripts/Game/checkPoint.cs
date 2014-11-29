using UnityEngine;
using System.Collections;

public class checkPoint : GameBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
	{
		Debug.Log (coll.gameObject.name);
		if (coll.gameObject.name == "Mesh") 
		{
			World.SetCheckpoint();		
			Debug.Log("checkpoint set");
		}
	}

}
