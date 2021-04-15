using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEW : MonoBehaviour
{
    GameObject particleprefab;

    void Start(){

        particleprefab = Resources.Load("Prefabs/Effects/Particles") as GameObject;
    }

    public void trigger(){

        Instantiate(particleprefab, transform.position, Quaternion.identity);

    }
}
