using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptBulletImpactoInimigo : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Inimigo")
        {
            other.gameObject.GetComponent<scriptInimigo>().HP -= 5;
        }

        Destroy(gameObject);

    }
}
