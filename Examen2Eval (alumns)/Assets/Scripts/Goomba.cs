using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goomba : MonoBehaviour
{
    private GameObject target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<MarioScript>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<MarioScript>())
        {
            // Destroy(collision.gameObject);
            // collision.gameObject.SetActive(false);
            // Time.timeScale = 0;
            GameManager.instance.ResetTime();
            GameManager.instance.LoadScene("Game");
        }
    }
}
