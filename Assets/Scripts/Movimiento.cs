using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [Header("Movimiento")]
    public float velocidad = 5f;
    private float horizontal;
    private bool mirandoDerecha = true;
    public bool attack = false;

    [Header("Salto")]
    public float speedSalto = 8f;
    public Transform checkPiso;
    public LayerMask layerPiso;
    public int maxJumps = 2;
    private int jumpCount = 0;

    [Header("Animacion")]
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isGrounded())
    {
        jumpCount = 0;
    }
    if (attack == false)
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal > 0 || horizontal < 0)
        {
            anim.SetFloat("Caminar", Mathf.Abs(horizontal));
        }
        else
        {
            anim.SetFloat("Caminar",0);
        }
    }

    if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, speedSalto);
        jumpCount++;
    }

    if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
    }

        voltear();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal*velocidad, rb.linearVelocity.y);
        //rb.AddForce(Vector2.right * horizontal * velocidad);
    }

    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(checkPiso.position, 0.1f, layerPiso);
    }

    private void voltear()
    {
        if (mirandoDerecha && horizontal < 0f || !mirandoDerecha && horizontal > 0f)
        {
            mirandoDerecha = !mirandoDerecha;
            
            /*Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;*/

            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Moneda")
        {
            GameManager.Instance.SetMonedas();

            Debug.Log(GameManager.Instance.cantMonedas);

            Destroy(collision.gameObject);
        }
    }
}
