using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaHead : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MarioScript marioScript = collision.gameObject.GetComponent<MarioScript>();
        if (marioScript)
        {
            marioScript.AddJumpForce();
            Destroy(gameObject.transform.parent.gameObject); 
        }
    }
}

// singleton
