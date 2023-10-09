using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptTilleColider : MonoBehaviour
{
    public float HP = 10;
    private Rigidbody2D rbd;
    private float metadeLargNPC;
    public float speed = 2;
    private int x = 1;
    public RaycastHit2D hitParede;
    public LayerMask camadaChao;
    public bool dir = true;


    // Start is called before the first frame update
    void Start()
    {
        
        rbd = GetComponent<Rigidbody2D>();
        metadeLargNPC = GetComponent<SpriteRenderer>().bounds.size.x/2;

        if (!dir)
        {
            transform.Rotate(0, 180, 0);
            x *= -1;
        }

    }

    // Update is called once per frame
    void Update() { 
    
        rbd.velocity = new Vector2(speed*x, rbd.velocity.y);
        hitParede = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.right, metadeLargNPC + 0.1f, camadaChao);

        if (hitParede.collider != null)
        {
            transform.Rotate(0, 180, 0);
            x *= -1;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.transform.position.y < gameObject.transform.position.y)
        {
            Debug.Log("Tocou player");
        }
    }

}