using UnityEngine;
using System.Collections;

public class PathShurikenStart : GameBehaviour {

	//Script to check if the player has entered the box collider

    void OnTriggerEnter2D(Collider2D coll)
    {
        //GameObject shuriken = transform.parent;

        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == "Mesh")
        {
			this.transform.parent.GetComponent<PathShuriken>().EnableMoving();
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        //GameObject shuriken = transform.parent;

        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == "Mesh")
        {
            this.transform.parent.GetComponent<PathShuriken>().DisableMoving();
        }
    }
}
