using UnityEngine;
using System.Collections;

public class ShurikenStartScript : GameBehaviour {

	//Script to check if the player has entered the box collider

	void OnTriggerEnter2D(Collider2D coll)
	{
		//GameObject shuriken = transform.parent;

		Debug.Log (coll.gameObject.name);
		if (coll.gameObject.name == "Mesh") 
		{
			this.transform.parent.GetComponent<FollowingShuriken>().isActive = true;
		}
	}

    void OnTriggerExit2D(Collider2D coll)
    {
        //GameObject shuriken = transform.parent;

        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == "Mesh")
        {
            this.transform.parent.GetComponent<FollowingShuriken>().isActive = false;
        }
    }
}
