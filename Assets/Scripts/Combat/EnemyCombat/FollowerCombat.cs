using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System;

public class FollowerCombat : MonoBehaviour,ICombat{


    public Colors[] colors;
    public int[] ratios;

     Character character;
    Inventory inventory;
    GameManager gm;
    GameObject target;
    Weapon weapon;
    Vector3 aimDir;
    int shotCount;
    float reloadCountdown;
    float charge;
    float fullCharge=1.0f;
    float dist;

    Transform[] singleSocket;
    Transform[] dualWieldSockets;

    public void Init(ICharacter character)
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        ////playerScript = GetComponent<Player>();
        //this.character = character;
        ////input = character.GetInputModule();
        //weapon = character.GetActiveWeapon();
        ////inventory = character.GetInventory();
        ////input.OnAimLock += OnAimLock;
        //aimDir = Vector3.down;

        //var weaponSocket1 = transform.Find("Hand1");
        //var weaponSocket2 = transform.Find("Hand2");
        //dualWieldSockets = new Transform[] { weaponSocket1, weaponSocket2 };
        //singleSocket = new Transform[] { weaponSocket2 };



        weapon = character.GetActiveWeapon();
        this.character = character as Character;
        aimDir = /*new Vector3(0.0f,0.0f,0.0f)*/Vector3.down;//tmp
        foreach (Colors c in colors)
        {
            this.character.AddColor(c);
        }
        character.SetBulletColor(colors[0]);
    }

    public void Update()
    {
        //character.ActivateAutoAim = gm.player.ActivateAutoAim;
        weapon = character.GetActiveWeapon();
        //Log.log("act " + character.GetActiveWeapon());
        AttackWithWeapon();
        
    }

    void AttackWithWeapon()
    {
        //if (character.ActivateAutoAim == true)
        //{    
                target = FindClosest();
        //}
        

        Aim();
        if (target != null)
        {
            dist = Vector3.Distance(transform.position, target.transform.position);

            if (dist < 20 && target.tag != "NotAimable")
            {
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
                if (target != null && charge >= fullCharge)
                {
                    if (Shoot()) charge = 0;
                }
            }
        }
    }

    bool Shoot()
    {
        bool shot = weapon.Shoot(target, aimDir);
        if (shot) shotCount++;
        if (weapon.MagSize > 0 && shotCount >= weapon.MagSize)
        {
            weapon.IsReloading = true;
            shotCount = 0;
            reloadCountdown = weapon.ReloadTime;
            return false;
        }
        return shot;
    }

    void Aim()
    {
        Vector3 direction;
        if (target != null)
        {
            direction = target.transform.position - gameObject.transform.position;
        }
        else
        {
            direction = character.GetInputModule().GetDirection();
        }
        if (direction.magnitude > 0)
        {
            foreach (Transform t in weapon.sockets)
            {
                t.right = direction;
                if (t.localEulerAngles.y != 0)
                {
                    t.localEulerAngles = new Vector3(0, 0, 180.0f);
                }
            }
            aimDir = direction;
        }
    }

    GameObject FindClosest()
    {
        GameObject closest = null;
        foreach (ICharacter enemy in gm.ActiveEnemies)
        {
            GameObject enemyObject = enemy.GetGameObject();
            if (closest == null)
            {
                closest = enemyObject;
                continue;
            }
            float enemyDistance = Vector3.Distance(enemyObject.transform.position, gameObject.transform.position);
            float closestDistance = Vector3.Distance(closest.transform.position, gameObject.transform.position);
            if (enemyDistance < closestDistance)
            {
                closest = enemyObject;
            }
        }
        return closest;
    }

    public void Terminate()
    {
        //input.OnAimLock -= OnAimLock;
    }
    public Vector3 GetAimDirection()
    {
        return aimDir;
    }
}
