using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptBulletPC : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Inimigo")
        {
            other.gameObject.GetComponent<scriptInimigo>().HP -= 5;
        }

        Destroy(gameObject);

    }
}
