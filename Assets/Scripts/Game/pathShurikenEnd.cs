using UnityEngine;
using System.Collections;

public class pathShurikenEnd : GameBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        //GameObject shuriken = transform.parent;

        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == "Mesh")
        {
            this.transform.parent.GetComponent<pathShuriken>().isActive2 = false;
        }
    }
}
