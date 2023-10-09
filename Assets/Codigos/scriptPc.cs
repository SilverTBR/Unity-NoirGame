using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class pcScript : MonoBehaviour
{
    private Rigidbody2D rbd;
    private Animator anim;
    private float speed = 5.5f;
    private bool dir = true, atirCima = false;
    private float largPC, firerate, coyoteTimeCounter, yAtual;
    //private int bulletX = 1;
    public GameObject pe, pontaArma, bullet, cabeca, arma;
    private GameObject projetilSpawn;
    public float forPulo;
    public LayerMask camadaChao, camadaInimigo;
    private bool isChao = false, pulou = false;
    private int jumpCount = 2;
    private float x, armaX, armaY, rotacaoArma, iFrameTime;
    public float coyoteTime = 0.3f;
    private bool canCoyote, DanoTomado = false;
    private AudioSource somTiro;
    private GameObject[] vidas;
    private static int vidasCount = 2;
    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        largPC = GetComponent<SpriteRenderer>().bounds.size.x;
        somTiro = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        projetilSpawn = pontaArma;
        atirCima = false;
        rotacaoArma = 1;
        vidas = GameObject.FindGameObjectsWithTag("vida");
    }


    // Update is called once per frame
    void Update()
    {
        movimentar();
        atirar();
        rotacionar();
        pular();
        verificaChao();
        resetPC();
        animacao();
        ativarTimeIFrame();

    }

    private void ativarTimeIFrame()
    {
        if (DanoTomado && iFrameTime > 0)
        {
            iFrameTime -= Time.deltaTime;
        }
        else
        {
            Physics2D.IgnoreLayerCollision(9, 8, false);
            Physics2D.IgnoreLayerCollision(9, 7, false);
            Physics2D.IgnoreLayerCollision(9, 10, false);
        }
    }

    private void ativarIFrame()
    {
        Physics2D.IgnoreLayerCollision(9, 8, true);
        Physics2D.IgnoreLayerCollision(9, 7, true);
        Physics2D.IgnoreLayerCollision(9, 10, true);


    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Inimigo" || col.gameObject.tag == "projetil")
        {
            ativarIFrame();
            if (vidasCount == 0) {
                Destroy(gameObject);
				SceneManager.LoadScene("Menu");
            }
            vidas[vidasCount].SetActive(false);
            vidasCount--;
            DanoTomado = true;
            iFrameTime = 1;
 
        }

    }




    private void movimentar()
    {
        //Movimentaçãos
        x = Input.GetAxisRaw("Horizontal");
        //Movimentação por fisica
        rbd.velocity = new Vector2(x * speed, rbd.velocity.y);
        //Movimentação sem a fisica
        //transform.Translate(transform.TransformDirection(new Vector2(x * 10f *Time.deltaTime,0)));
    }

    private void animacao()
    {
        if (x != 0)
        {
            anim.SetBool("andando", true);
        }
        else
        {
            anim.SetBool("andando", false);
        }
        if(!isChao && !canCoyote )
        {
            if(rbd.velocity.y > 0.1f)
            {
                anim.SetBool("pulando", true);
                anim.SetBool("caindo", false);
                yAtual = transform.position.y;
            }


            if (rbd.velocity.y < -0.1f)            
            {
                anim.SetBool("caindo", true);
                anim.SetBool("pulando", false);
            }
        }
        else
        {
            anim.SetBool("caindo", false);
        }
    }

    private void rotacionar()
    {
        //rotacionar
        if (dir && x < 0 || !dir && x > 0)
        {
            transform.Rotate(0, 180, 0);
            dir = !dir;
            
        }
        if(x != 0)
        {
            rotacaoArma = x;
        }
    }

    private void pular()
    {
        //Pular
        if (Input.GetKeyDown(KeyCode.Z) && jumpCount > 0)
        {
            if ((isChao || canCoyote))
            {

                //rbd.AddForce(new Vector2(0, forPulo));
                rbd.velocity = new Vector2(rbd.velocity.x, forPulo);
                pulou = true;

            }
            else
            {
                    //rbd.AddForce(new Vector2(0, forPulo));
                    rbd.velocity = new Vector2(rbd.velocity.x, forPulo);
                pulou = true;
               
            }
            jumpCount--;
        }


    }

    private void verificaChao()
    {
        RaycastHit2D hitChao;
        //Verifica se esta chao
        hitChao = Physics2D.Raycast(pe.transform.position, -pe.transform.up , 0.1f, camadaChao);
        if (hitChao.collider != null)
        {
            isChao = true;
            coyoteTimeCounter = 0f;
            canCoyote = true;
            transform.parent = hitChao.collider.transform;

            if (!pulou)
            {
                jumpCount = 2;
            }
            if(jumpCount == 0)
            {
                pulou = false;
            }
        }
        else
        {
            isChao = false;
            transform.parent = null;
            verificaCoyote();
            verificaPisaInimigo();
        }
    }


    private void verificaPisaInimigo()
    {
        RaycastHit2D hitInimigo = Physics2D.Raycast(pe.transform.position, -pe.transform.up, 0.1f, camadaInimigo);
        if(hitInimigo.collider != null)
        {
            GameObject inimigo = hitInimigo.collider.gameObject;
            Destroy(inimigo);
            rbd.velocity = new Vector2(rbd.velocity.x, forPulo);                
        }
    }


    private void verificaCoyote()
    {
        coyoteTimeCounter += Time.deltaTime;
        if (coyoteTimeCounter > coyoteTime)
        {
            canCoyote = false;
        }
    }

    private void resetPC(){
        if(Input.GetKey(KeyCode.R))
            transform.position = new Vector2(-4.44f,3.64f);

    }

    void atirar()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            atirCima = true;
            projetilSpawn = cabeca;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            arma.transform.Rotate(0, 0, 90);
            arma.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.079f);

        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            projetilSpawn = pontaArma;
            atirCima = false;
            arma.transform.Rotate(0, 0, -90);
            arma.transform.position = new Vector2(gameObject.transform.position.x+(0.21f*rotacaoArma), gameObject.transform.position.y- 0.26f);


        }
        if (Input.GetKeyDown(KeyCode.X) && firerate <= Time.time){
        somTiro.Play();
        scriptGenProjetil.spawn(projetilSpawn, bullet, dir, atirCima, false);
        firerate = Time.time + 1f;
        }
}

    //Função que verifica se está correndo e quando para.
    //void Correr()
    //{
    //    if (Input.GetKey(KeyCode.LeftShift))
    //    {
    //        //Aplica aceleração com o tempo
    //        //speed = speed < 5.5 ? speed += Time.deltaTime*aceler: speed;

    //        //Aplicação de aceleração maxima instantanea
    //        speed = 5.5f;
    //    }
    //    //Retorna velocidade padrão ao soltar o shift esquerdo
    //    if (Input.GetKeyUp(KeyCode.LeftShift))
    //    {
    //        speed = 2.5f;
    //    }

    //}



}

