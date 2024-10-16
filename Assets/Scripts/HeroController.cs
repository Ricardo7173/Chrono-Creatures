using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
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
    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltosRestantes = satosMaximos;
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
    }

    bool EstaEnSuelo()
    {
       RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo); 
       return raycastHit.collider != null;
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
        }
    }
    
    void ProcesarMovimiento(){
        if (!puedeMoverse)
        {
            return;
        }

        float inputMovimiento = Input.GetAxis("Horizontal");

        if(inputMovimiento != 0f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        rigidBody.velocity = new Vector2(inputMovimiento * velocidad, rigidBody.velocity.y);
        GestionarOrientacion(inputMovimiento);
    }

    void GestionarOrientacion(float inputMovimiento){
        if((mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false && inputMovimiento > 0)){
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    public void AplicarGolpe(){
        puedeMoverse = false;

        Vector2 direccionGolpe;

        if (rigidBody.velocity.x > 0)
        {
            direccionGolpe = new Vector2(-1, 1);
        } else
        {
            direccionGolpe = new Vector2(1, 1);
        }

        rigidBody.AddForce(direccionGolpe * fuerzaGolpe);

        StartCoroutine(EsperarYActivarMovimiento());
    }

    IEnumerator EsperarYActivarMovimiento(){
        yield return new WaitForSeconds(0.1f);

        while (!EstaEnSuelo())
        {
            yield return null;
        }

        puedeMoverse = true;
    }
}
