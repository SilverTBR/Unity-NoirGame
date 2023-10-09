using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptRanged : MonoBehaviour
{
    public float HP = 10;
    public GameObject alvo;
    private float distanciaAlvo = 0;
    private Rigidbody2D rbd;
    private int x;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }


        verificarAlvo();
    }

    void verificarAlvo()
    {
        distanciaAlvo = gameObject.transform.position.x - alvo.transform.position.x;
        if (distanciaAlvo < 10)
        {
            moverAoAlvo();
        }

    }

    void moverAoAlvo()
    {
        if (distanciaAlvo < 0)
        {
            x = 1;
        }
        else
        {
            x = -1;
        }
        //rbd.velocity = new Vector2(x * speed, rbd.velocity.y);
    }

}
