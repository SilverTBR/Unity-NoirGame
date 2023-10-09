using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptCaixa : MonoBehaviour
{
	public GameObject caixa;
	float y = 21.5f;
    // Start is called before the first frame update
    void Start()
    {
        
		InvokeRepeating("spawn",0, 2f);
    }


    void spawn()
    {
        Vector2 pos = new Vector2(28.5f, y);
        Instantiate(caixa, pos, Quaternion.identity);
		pos = new Vector2(41.61f, y);
        Instantiate(caixa, pos, Quaternion.identity);
		pos = new Vector2(50.03f, y);
        Instantiate(caixa, pos, Quaternion.identity);
		pos = new Vector2(65.67f, y);
        Instantiate(caixa, pos, Quaternion.identity);
    } 
	
}
