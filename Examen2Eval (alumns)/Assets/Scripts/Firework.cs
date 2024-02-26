using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    public float minTimeToExplode = 1f;
    public float maxTimeToExplode = 3f;
    public float minForce = 5f;
    public float maxForce = 10f;
    public int minFireworks = 3;//Numero minimo de fuegos artificiales.
    public int maxFireworks = 6;//Numero maximo de fuegos artificiales.
    public Color[] colors;
    public GameObject fireworkPrefab;
    public int maxExplosions = 3;

    private Rigidbody2D _rb;
    private SpriteRenderer _rend;
    private int _count = 0;
    private Vector2 _dir = Vector2.up;
    private float currentTime, timeToExplode;
    private bool exploded = false;
    private float explodeTime;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rend = GetComponent<SpriteRenderer>();
        GameManager.instance.IncrementFireworksCount();
        Vector2 dir = Random.insideUnitCircle.normalized;//Aplica una fuerza en una dirección aleatoria.
        float force = Random.Range(minForce, maxForce);
        _rb.AddForce(dir * force);

        _rend.color = colors[Random.Range(0, colors.Length)];//Elige un color aleatorio para el SpriteRenderer.
        float explodeTime = Random.Range(minTimeToExplode, maxTimeToExplode);//Establece un tiempo aleatorio para explotar.
        Invoke("Explode", explodeTime);
    }

    private void Update()
    {
        if (transform.position.y < -10f)//Si el objeto sale de la pantalla, se deestruye.
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(!exploded && Time.time >= explodeTime)
        {
            Explode();
        }
    }
    void Explode()
    {
        exploded = true;//Marcar como explotado para evitar quecse ejecute varias veces.
        Destroy(gameObject);//Destruye ese fuego artificial.

        int numFireworks = Random.Range(minFireworks, maxFireworks + 1);//Genera un numero aletaorio de fuegos artificiales.
        for (int i = 0; i< numFireworks; i++)
        {
            // Instanciar un nuevo fuego artificial con dirección aleatoria
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            GameObject newFirework = Instantiate(gameObject, transform.position, Quaternion.identity);
            newFirework.GetComponent<Rigidbody2D>().AddForce(randomDir * Random.Range(minForce, maxForce));
        }
    }
}
