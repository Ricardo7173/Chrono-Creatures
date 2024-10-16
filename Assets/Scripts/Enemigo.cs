using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float cooldownAtaque;
    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
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
