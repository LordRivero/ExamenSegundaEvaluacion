using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinSound;
    public int points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<MarioScript>())
        {
            AudioManager.instance.PlayAudio(coinSound, "coindSound", false, 0.3f);
            GameManager.instance.SetPoints(GameManager.instance.GetPoints() + points);
            Destroy(gameObject);
        }
    }
}
