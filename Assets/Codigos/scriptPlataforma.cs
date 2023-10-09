using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPlataforma : MonoBehaviour
{
    private float cont = 0;
    public float deslocamento = 0.1f;
    private Vector2 posIni;
    public float tamanhoX = 1;
    public float tamanhoY = 1;
    // Start is called before the first frame update
    void Start()
    {
        posIni = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Sin(cont) * tamanhoX;
        float y = Mathf.Cos(cont) * tamanhoY;

        cont += deslocamento * Time.deltaTime;

        Vector2 novaPos = new Vector2(posIni.x+x, posIni.y+y);
        transform.position = novaPos;   
    }
}
