using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System.Linq;
using System;

class Platoon {
    float fullCharge = 1.0f;
    float charge;
    public List<Squad> squads = new List<Squad>();
    public float fireRate;
    Dictionary<Colors,int> colorRates;
    public event Action<Platoon> OnPlatoonDefeat;

    public Platoon(string prefabName, Vector3[] positions, int rows, int cols, Dictionary<Colors,int> colorRates, float fireRate) {
        foreach(Vector3 pos in positions){
            Squad squad = new Squad(prefabName,rows,cols,pos);
            squads.Add(squad);
            squad.OnSquadDefeat+=OnSquadDefeat;
        }
        this.colorRates = colorRates;
        this.fireRate = fireRate;
    }

    public void Update(){
        charge += Time.deltaTime * fireRate;
        if(charge >= fullCharge){
            charge=0;
            Shoot();
        }
    }

    Colors[] GetColors(){
        int[] ratios;
        Colors[] colors;
        Dictionary<Colors,int> clone = new Dictionary<Colors, int>(colorRates);
        Colors[] retColors = new Colors[squads.Count];
        for(int i=0; i<squads.Count; i++){
            ratios = clone.Values.ToArray();
            colors = clone.Keys.ToArray();
            retColors[i] = ChooseColor(colors,ratios);
            clone.Remove(retColors[i]);
        }
        return retColors;
    }

    Colors ChooseColor(Colors[] colors, int[] ratios){
        int max = ratios.Sum();
        int rnd = UnityEngine.Random.Range(0,max);
        int lowLimit = 0;
        for(int i=0; i<ratios.Length; i++){
            int limit = lowLimit + ratios[i];
            if(rnd >= lowLimit && rnd < limit){
                return colors[i];
            }
            lowLimit = limit;
        }
        return colors[0];
    }

    void Shoot(){
        Colors[] colors = GetColors();
        int i=0;
        foreach(Squad s in squads){
            s.SetSquadColor(colors[i]);
            s.Shoot();
            i++;
        }
    }

    void OnSquadDefeat(Squad s){
        s.OnSquadDefeat-=OnSquadDefeat;
        squads.Remove(s);
        if(squads.Count==0 && OnPlatoonDefeat!=null) OnPlatoonDefeat(this);
    }
}