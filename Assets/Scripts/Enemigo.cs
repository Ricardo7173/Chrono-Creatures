using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int puntos;
    public GameManager gameManager;
    public float cooldownAtaque;
    public float fuerzaRebote;
    public float hitsDie;
    private float hits = 0f;
    private bool puedeAtacar = true;
    private bool recibiendoDanio;
    private SpriteRenderer spriteRenderer;

    public Transform objetivo;
    public float speed;
    private bool debePerseguir;

    private float distancia;
    private float distanciaAbsoluta;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private Vector3 escalaOriginal; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        escalaOriginal = transform.localScale; 

    }

    void Update()
    {

         if (objetivo == null || !objetivo.CompareTag("Player"))
    {
        // Busca al jugador activo si el objetivo no es válido
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            objetivo = player.transform;
        }
    }
    
        distancia = objetivo.position.x - transform.position.x;
        distanciaAbsoluta = math.abs(distancia);

        if (debePerseguir && !recibiendoDanio)
        {
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, speed * Time.deltaTime);
        }

       if (distancia > 0)
        {
             transform.localScale = new Vector3(-Mathf.Abs(escalaOriginal.x), escalaOriginal.y, escalaOriginal.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(escalaOriginal.x), escalaOriginal.y, escalaOriginal.z);

        }

        if (distanciaAbsoluta < 15)
        {
            debePerseguir = true;
            animator.SetBool("isFollowing", true);
        }
        else
        {
            debePerseguir = false;
            animator.SetBool("isFollowing", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("contactExist", true);

            if (!puedeAtacar) return;

            puedeAtacar = false;

            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;

            GameManager.Instance.PerderVida();
            collision.gameObject.GetComponent<HeroController>().AplicarGolpe();

            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Espada"))
        {
            hits += 1;
            animator.SetBool("damage", true);
            Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x, 0);
            RecibeDanio(direccionDanio, 1);

            if (hits >= hitsDie)
            {
                animator.SetBool("damage", false);
                animator.SetBool("die", true);

                // Llama a la corrutina para destruir el enemigo después de la animación
                gameManager.SumarPuntos(puntos);
                StartCoroutine(DestruirEnemigo());
            }
        }
    }

    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        if (!recibiendoDanio)
        {
            recibiendoDanio = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 2.0f).normalized;
            rigidBody.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
        }
    }

    void ReactivarAtaque()
    {
        puedeAtacar = true;
        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }

    public void DesactivarAtaque()
    {
        animator.SetBool("contactExist", false);
    }

    public void DesactivarAnimacion()
    {
        recibiendoDanio = false;
        animator.SetBool("damage", false);
    }

    private IEnumerator DestruirEnemigo()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    void VerificarLimites()
    {
        float limiteInferior = -10f;  
        
        if (transform.position.y < limiteInferior)
        {
            Destroy(gameObject);
        }
    }
}
