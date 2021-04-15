using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System.Linq;
using System;

class Squad {
    public Dictionary<ICharacter,SquadMemberCombat> members = new Dictionary<ICharacter,SquadMemberCombat>();
    Colors squadColor;
    public event Action<Squad> OnSquadDefeat;

    public Squad(string prefabName, int rows, int cols, Vector3 pos) {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Characters/" + prefabName);
        GameManager gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        float separation = prefab.transform.localScale.x * 1.1f;
        float totalWidth = separation*(cols-1);
        float totalHeight = separation*(rows-1);
        for(int i=0; i<rows; i++){
            for(int j=0; j<cols; j++){
                float x = pos.x - totalWidth/2 + j*separation;
                float y = pos.y - totalHeight/2 + i*separation;
                GameObject member = MonoBehaviour.Instantiate(prefab, new Vector3(x,y,0), Quaternion.identity) as GameObject;
                ICharacter ch = member.GetComponent<ICharacter>();
                ch.Init();
                members.Add(ch,member.GetComponent<SquadMemberCombat>());
                if(gm != null) gm.AddEnemy(ch);
                ch.OnDeathEvent+=OnMemberDeath;
            }
        }        
    }

    public void SetSquadColor(Colors color){
        squadColor = color;
    }

    public void Shoot(){
        foreach(var member in members.Values){
            member.SetColor(squadColor);
            member.Shoot();
        }
    }

    void OnMemberDeath(ICharacter ch){
        ch.OnDeathEvent-=OnMemberDeath;
        members.Remove(ch);
        if(members.Count==0 && OnSquadDefeat!=null) OnSquadDefeat(this);
    }
}