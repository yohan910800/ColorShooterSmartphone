using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class EnemyChargeCombat : EnemyCombatV1
{
    public Colors[] colors;
    public int[] ratios;

    public override void Init(ICharacter character)
    {
        base.Init(character);
        foreach (Colors c in colors)
        {
            this.character.AddColor(c);
        }
        character.SetBulletColor(colors[0]);

    }

    void SetRandomBulletColor()
    {
        int rnd = Random.Range(0, 100);
        int lowLimit = 0;
        for (int i = 0; i < ratios.Length; i++)
        {
            int limit = lowLimit + ratios[i];
            if (rnd >= lowLimit && rnd < limit)
            {
                character.SetBulletColor(colors[i]);
                return;
            }
            lowLimit = limit;
        }
    }

    protected override bool Shoot()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        //Log.log("dist " + dist);
        //Log.log("phase " + GetComponent<EnemyChargeInput>().phase);
        if (dist >3&& GetComponent<EnemyChargeInput>().isAttacking==false
            || GetComponent<EnemyChargeInput>().stopBurst==true)
        {
            return false;
        }

        else
        {
            if (colors.Length > 1) SetRandomBulletColor();
            return base.Shoot();
        }
    }
}
