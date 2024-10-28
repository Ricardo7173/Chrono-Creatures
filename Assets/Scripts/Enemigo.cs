using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float cooldownAtaque;
    public float fuerzaRebote;
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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        distancia = objetivo.position.x - transform.position.x;

        distanciaAbsoluta = math.abs(distancia);

        if (debePerseguir == true)
        {
            if (!recibiendoDanio)
            {
                transform.position = Vector2.MoveTowards(transform.position, objetivo.position, speed * Time.deltaTime);
            }
        }

        // Controlar la dirección del sprite según el objetivo
        if (distancia > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Comienza a perseguir si está dentro del rango de distancia
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
            Vector2 direcionDanio = new Vector2(collision.gameObject.transform.position.x, 0);

            RecibeDanio(direcionDanio, 1);
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

    public void DesactivarAtaque(){
        animator.SetBool("contactExist", false);
    }

}