using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float cooldownAtaque;
    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;

    public Transform objetivo;
    public float speed;
    private bool debePerseguir;

    private float distancia;
    private float distanciaAbsoluta;
    private Animator animator;
    private Rigidbody2D rigidBody;

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update(){

        distancia = objetivo.position.x - transform.position.x;

        distanciaAbsoluta = math.abs(distancia);

        if (debePerseguir == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, speed * Time.deltaTime);
        }

        if (distancia > 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }else{
            transform.localScale=new Vector3(1,1,1);
        }

        if (distanciaAbsoluta < 15)
        {
            debePerseguir = true;
            animator.SetBool("isFollowing", true);
        }else
        {
            debePerseguir = false;
            animator.SetBool("isFollowing", false);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

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

    void ReactivarAtaque(){
        puedeAtacar = true;
        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;

    }

}