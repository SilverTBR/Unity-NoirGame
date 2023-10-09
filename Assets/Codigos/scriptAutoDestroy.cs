using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptAutoDestroy : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 10);
    }
}
