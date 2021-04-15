using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class GRP4BulletCombat : EnemyCombatV1
{
    public Vector3[] direction = new Vector3[4];

    public Colors[] colors;
    public int[] ratios;
    //float fullCharge = 1.0f;
    //float charge;
    //GameObject target;
    //ICharacter character;
    //Weapon weapon;
    //Vector3 aimDir;

    float time;
    int num;
    GameObject t;

    public override void Init(ICharacter character)
    {
        base.Init(character);
        foreach (Colors c in colors)
        {
            this.character.AddColor(c);
        }
        character.SetBulletColor(colors[0]);
        //target = GameObject.FindWithTag("Player");
        weapon = character.GetActiveWeapon();
        character.OnWeaponChange += OnWeaponChange;
        //this.character = character;
        aimDir = Vector3.down;
        this.character = character as Character;
        target = transform.GetChild(3).gameObject;//target
    }

    public override void Update()
    {
        if (target == null) return;
                Aim();
                if (weapon.IsReloading)
                {
                    reloadCountdown -= Time.deltaTime;
                    if (reloadCountdown <= 0)
                    {
                        weapon.IsReloading = false;
                    }
                    else
                    {
                        return;
                    }
                }
                charge += Time.deltaTime * weapon.FireRate;
                if (charge >= fullCharge)
                {
                    charge = 0;
                    Log.log("the enemy shot ");
                    if (Shoot()) charge = 0;
                }
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
    //public void Update()
    //{
    //    target = t;
    //    //t.transform.RotateAround(transform.position, 2f);
    //    if (target == null) return;
    //    Aim();
    //    charge += Time.deltaTime * weapon.FireRate;
    //    if (charge >= fullCharge)
    //    {
    //        charge = 0;
    //        Shoot();
    //    }
    //}

    //void Shoot()
    //{
    //    //weapon.Shoot(target);
    //}

    //void Aim()
    //{
    //    Vector3 direction;
    //    if (target != null)
    //    {
    //        //direction = transform.localEulerAngles = new Vector3(2 * Time.deltaTime, 0, 0);
    //        /*Vector3.down*/ /*target.transform.position - gameObject.transform.position*/
    //        direction = Vector3.down*Time.deltaTime;
    //    }
    //    else
    //    {
    //        direction = character.GetInputModule().GetDirection();
    //    }
    //    if (direction.magnitude > 0)
    //    {
    //        foreach (Transform t in weapon.sockets)
    //        {
    //            t.right = direction;
    //            if (t.localEulerAngles.y != 0)
    //            {
    //                t.localEulerAngles = new Vector3(0, 0, 180.0f);
    //            }
    //        }
    //        aimDir = direction;
    //    }
    //}

    //void OnWeaponChange(Weapon weapon)
    //{
    //    this.weapon = weapon;
    //}

    //public void Terminate()
    //{

    //}

    //public Vector3 GetAimDirection()
    //{
    //    return aimDir;
    //}
}
