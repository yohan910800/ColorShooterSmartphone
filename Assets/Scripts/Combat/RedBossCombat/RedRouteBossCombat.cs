using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
/*
Simple auto aim at player combat module.
*/
public class RedRouteBossCombat : BossCombat
{

    Character characterinstance;
    Inventory inventory;
    RedRouteBoss redRouteBoss;
  
    Stats stats;

    bool weaponToggle = true;
    int phase2ThresholdHP = 100;

    enum Phase
    {
        Phase1,
        Phase2
    }
    Phase phase;

    public override void Init(ICharacter character)
    {
        base.Init(character);
        stats = character.GetStats();
        redRouteBoss = character as RedRouteBoss;
    }

    public override void Update()
    {
        //Debug.Log(stats.HP);
       
        // Checking for state changes before calling the parent update
        if (phase == Phase.Phase1 && 500 <= phase2ThresholdHP)
        {
            ChangeState(state.Phase2Enter);
        }
        // Parent update
        base.Update();


    }

    protected override void Phase1Enter()
    {
        phase = Phase.Phase1;
        ChangeState(state.Phase1Pattern1);

    }

    protected override void Phase1Pattern1()
    {
        if (target == null) return;
        charge += Time.deltaTime * weapon.FireRate;
        if (charge >= fullCharge)
        {
            Shoot();
            charge = 0;
            if (weaponToggle)
            {
                redRouteBoss.ActivateWeaponP1W1();
            }
            else
            {
                redRouteBoss.ActivateWeaponP1W2();
            }
            weaponToggle = !weaponToggle;

        }
    }

    protected override void Phase1Pattern2()
    {

    }
    protected override void Phase2Enter()
    {
        phase = Phase.Phase2;
        ChangeState(state.Phase2Pattern1);
    }
    protected override void Phase2Pattern1()
    {
        if (target == null) return;
        charge += Time.deltaTime * weapon.FireRate;
        if (charge >= fullCharge)
        {
            charge = 0;
            redRouteBoss.ActivateWeaponP2W1();
            int patternNum = (int)Random.Range(0, 4);
            Shoot();
            //  Log.log(""+patternNum);
        }
    }
    protected override void Phase2Pattern2()
    {

    }
    protected override void EndPattern()
    {

    }
}