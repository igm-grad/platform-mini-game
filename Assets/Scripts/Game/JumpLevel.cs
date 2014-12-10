using UnityEngine;
using System.Collections;

public class JumpLevel : MonoBehaviour {

	public string NextLevelName=  "Level2" ;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Player") 
		{
			Application.LoadLevel(NextLevelName);
			World.ResetWorld();
		}
		/*var player = coll.gameObject.GetComponent<CharacterController2D>();
		if (player != null)
		{
			player.Interact(this);
		}*/
	}
}
