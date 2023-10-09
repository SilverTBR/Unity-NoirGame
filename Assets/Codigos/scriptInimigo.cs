using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class scriptInimigo : MonoBehaviour
{
    public float HP = 10;
    private GameObject alvo;
    public GameObject pontaArma, bala;
    private float distanciaAlvo = 0, firerate;
    private Rigidbody2D rbd;
    private int xAlvo;
    public float speed = 1;
    private Vector2 pontoInicial;
    public bool dir = true;
    private bool atirando = false;
    private Animator anim;
    public float RangeDetection = 13, ShootDetection = 10, firerateTime = 3;


    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        alvo = GameObject.FindGameObjectWithTag("Player");
        pontoInicial = transform.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(HP<=0){
            Destroy(gameObject);
        }

        
        verificarAlvo();
        rotacionar();
    }

    void verificarAlvo()
    {
        detectarLado();
        if (Mathf.Abs(distanciaAlvo) < 13)
        {
            anim.SetBool("Detectado", true);
            if (Mathf.Abs(distanciaAlvo) <= 10)
            {
                rbd.velocity = new Vector2(0, rbd.velocity.y);
                atirar();
            }
            else {
                atirando = false;
                moverAoAlvo();
                
            }
            
        }
        else
        {
            anim.SetBool("Detectado", false);

            rbd.velocity = new Vector2(0, rbd.velocity.y);

            transform.position = Vector2.MoveTowards(transform.position, pontoInicial, speed*Time.deltaTime);
        }

    }

   void atirar()
    {
        if (firerate <= Time.time)
        {
            scriptGenProjetil.spawn(pontaArma, bala, dir, false, false);
            firerate = Time.time + firerateTime;
            atirando = true;
        }
    }

    void detectarLado()
    {
        distanciaAlvo =  alvo.transform.position.x - gameObject.transform.position.x ;
        if (distanciaAlvo < 0)
        {
            xAlvo = -1;
        }
        else 
        {
            xAlvo = 1;
        }

    }

    void moverAoAlvo()
    {
        if (!atirando)
        {
            rbd.velocity = new Vector2(xAlvo * speed, rbd.velocity.y);
        }
    }

    void rotacionar()
    {
        if (dir && xAlvo < 0 || !dir && xAlvo > 0)
        {
            transform.Rotate(0, 180, 0);
            dir = !dir;

        }
    }
}
