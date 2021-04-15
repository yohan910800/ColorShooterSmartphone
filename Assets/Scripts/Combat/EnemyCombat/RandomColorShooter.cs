using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

/*
Auto aim at player and shoot weighted random color combat module.
*/
public class RandomColorShooter : EnemyCombatV1 {
    
    public Colors[] colors;
    public int[] ratios;

    public override void Init(ICharacter character){
        base.Init(character);
        foreach(Colors c in colors){
            this.character.AddColor(c);
        }
        character.SetBulletColor(colors[0]);
        
    }

    void SetRandomBulletColor(){
        int rnd = Random.Range(0,100);
        int lowLimit = 0;
        for(int i=0; i<ratios.Length; i++){
            int limit = lowLimit + ratios[i];
            if(rnd >= lowLimit && rnd < limit){
                character.SetBulletColor(colors[i]);
                return;
            }
            lowLimit = limit;
        }
    }

    protected override bool Shoot(){
        //if (gameObject.tag != "NotAimable")
        //{
            if (colors.Length > 1) SetRandomBulletColor();
            return base.Shoot();
        //}
        //else
        //{
            //return false;
        //}
    }
}