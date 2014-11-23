using UnityEngine;
using System.Collections;

public class AbysmalPit : GameBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(" hi " + coll.gameObject.name);
        var player = coll.gameObject.GetComponent<CharacterController2D>();
        if (player != null)
        {
            player.Interact(this);
        }
    }
}
