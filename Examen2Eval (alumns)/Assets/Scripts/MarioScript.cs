using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MarioScript : MonoBehaviour
{
    public KeyCode rightKey, leftKey, jumpKey;
    public float speed, rayDistance;
    public LayerMask groundMask;
    public AudioClip jumpClip;
    public GameObject fireworkPrefab;
    public int maxJumps = 2; //Máximo númeo de saltos permitidos.
    public float jumpForce = 10f;
    public Transform groundCheck; //Posición del objeto para verificar si el jugador está en el suelo.
    public LayerMask groundLayer; //Capa que representa el suelo.
    public float groundCheckRadius = 0.2f;

    private int jumpsRemaining;//Número de saltos restantes.
    private Rigidbody2D rb;
    private SpriteRenderer _rend;
    private Animator _animator;
    private Vector2 dir;
    private bool _intentionToJump;
    private bool isGrounded;//indica si el jugador está en el suelo.

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _rend = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        jumpsRemaining = maxJumps;//Establece los saltos restantes al máximo permitido.
    }

    // Update is called once per frame
    void Update()
    {
        // print(GameManager.instance.GetTime());
     isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);//Verifica si el jugador está en el suelo.
     if(isGrounded)
        {
            jumpsRemaining = maxJumps;//Reset de los saltos restantes si está en el suelo.
        }   
        dir = Vector2.zero;
        if (Input.GetKey(rightKey))
        {
            _rend.flipX = false;
            dir = Vector2.right;
        }
        else if (Input.GetKey(leftKey))
        {
            _rend.flipX = true;
            dir = new Vector2(-1, 0);
        }

        // _intentionToJump = false;
        if (Input.GetKeyDown(jumpKey) && jumpsRemaining > 0)//Si el jugador presiona la tecla de salto y le quedan saltos restantes, salta.
        {
            _intentionToJump = true;
        }

        if(Input.GetMouseButtonDown(0))
        {
           Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            Instantiate(fireworkPrefab, mousePos, Quaternion.identity);
        }

        #region ANIMACIONES
        // ANIMACIONES (PROXIMA DIA ORGANIZARLO EN OTRO SCRIPT)
        if (dir != Vector2.zero)
        {
            // estamos andando
            _animator.SetBool("isWalking", true);
        }
        else
        {
            // estamos parados
            _animator.SetBool("isWalking", false);
        }
        #endregion
    }

    private void FixedUpdate()
    {
        bool grnd = IsGrounded();
        float currentYVel = rb.velocity.y;
        Vector2 nVel = dir * speed;
        nVel.y = currentYVel;

        rb.velocity = nVel;


        if (_intentionToJump && grnd)
        {
            _animator.Play("jumpAnimation");
            AddJumpForce();
        }
        _intentionToJump = false;

        _animator.SetBool("isGrounded", grnd);
    }

    public void AddJumpForce()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);//Aplica la fuerza de salto.
        rb.AddForce(Vector2.up * jumpForce * rb.gravityScale, ForceMode2D.Impulse);
        AudioManager.instance.PlayAudio(jumpClip, "jumpSound");
        jumpsRemaining--;
    }

    private bool IsGrounded()
    {
        RaycastHit2D collision = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundMask);
        if (collision)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * rayDistance);
    }
}
