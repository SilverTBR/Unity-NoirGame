using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class scriptProjetil : MonoBehaviour
{
    private Rigidbody2D rbd;
    public bool dir, atirCima, atirBaixo;
    private int bulletX = 0, bulletY = 0;
    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        if (atirCima || atirBaixo)
        {
            if(atirCima) {
                bulletY = 1;
            }
            else
            {
                bulletY= -1;
            }
        }
        else
        {
            if (dir)
            {
                bulletX = 1;
            }
            else
            {
                bulletX = -1;
            }
        }
        rbd.velocity = new Vector2(bulletX*20, bulletY*20);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Inimigo")
        {
            other.gameObject.GetComponent<scriptInimigo>().HP -= 5;
        }

        Destroy(gameObject);

    }
}
