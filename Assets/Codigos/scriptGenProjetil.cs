using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
//using UnityEditor.VersionControl;
using UnityEngine;

public class scriptGenProjetil : MonoBehaviour
{

    public static void spawn(GameObject spawnPoint, GameObject bullet, bool dir, bool atirCima, bool atirBaixo){
        GameObject projetil = Instantiate(bullet, spawnPoint.transform.position, spawnPoint.transform.rotation);
        projetil.GetComponent<scriptProjetil>().dir = dir;
        projetil.GetComponent<scriptProjetil>().atirCima = atirCima;
        projetil.GetComponent<scriptProjetil>().atirBaixo = atirBaixo;

    }
}
