using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System.Linq;
using System;

public class PlatoonManager : MonoBehaviour {

    List<Platoon> platoons = new List<Platoon>();

    void Start() {

    }

    void Update() {
        foreach(Platoon pl in platoons){
            pl.Update();
        }
    }

    public void SpawnPlatoon(string prefabName, Vector3[] positions, int rows, int cols, Dictionary<Colors,int> colorRates, float fireRate){
        Platoon pl = new Platoon(prefabName, positions, rows, cols, colorRates, fireRate);
        platoons.Add(pl);
        pl.OnPlatoonDefeat+=OnPlatoonDefeat;
    }

    void OnPlatoonDefeat(Platoon pl){
        pl.OnPlatoonDefeat-=OnPlatoonDefeat;
        platoons.Remove(pl);
    }
}