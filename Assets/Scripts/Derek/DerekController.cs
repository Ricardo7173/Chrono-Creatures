using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerekController : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    public float fuerzaGolpe;
    public float satosMaximos;
    public LayerMask capaSuelo;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true;
    private float saltosRestantes;
    private Animator animator;
    private bool puedeMoverse = true;
    private bool puedeAtacar = true;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltosRestantes = satosMaximos;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
        ProcesarAtaque();
        VerificarLimites();
    }

    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }

    void ProcesarMovimiento()
    {
        if (!puedeMoverse)
        {
            return;
        }

        float inputMovimiento = Input.GetAxis("Horizontal");

        if (inputMovimiento != 0f)
        {
            animator.SetBool("isWalking", true);
            animator.ResetTrigger("isIdle");
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetTrigger("isIdle");
        }

        rigidBody.velocity = new Vector2(inputMovimiento * velocidad, rigidBody.velocity.y);
        GestionarOrientacion(inputMovimiento);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        if ((mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false && inputMovimiento > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    void ProcesarSalto()
    {
        if (EstaEnSuelo())
        {
            saltosRestantes = satosMaximos;
        }

        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            saltosRestantes--;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            rigidBody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            animator.SetTrigger("isJumping");
        }
        else if (EstaEnSuelo())
        {
            animator.ResetTrigger("isJumping");
        }
    }

    void ProcesarAtaque()
    {
        if (Input.GetKeyDown(KeyCode.Z) && EstaEnSuelo() && puedeAtacar)
        {
            Atacando();
        }
    }

    public void Atacando()
    {
        StartCoroutine(RealizarAtaque());
    }

    private IEnumerator RealizarAtaque()
    {
        puedeAtacar = false;
        animator.SetTrigger("isAttacking");
        yield return new WaitForSeconds(0.5f);
        puedeAtacar = true;
    }

    void VerificarLimites()
    {
        float limiteInferior = -10f;

        if (transform.position.y < limiteInferior)
        {
            Debug.Log("Derek ha caído");
            // Agregar lógica si quieres que pierda una vida o se reinicie
        }
    }
}
