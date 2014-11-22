using UnityEngine;
using System.Collections;

public class Spike : GameBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        var player = coll.gameObject.GetComponent<CharacterController2D>();
        if (player != null)
        {
            player.Interact(this);
        }
    }
}
