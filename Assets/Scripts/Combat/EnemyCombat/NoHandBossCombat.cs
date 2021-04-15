using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using Random = UnityEngine.Random;

public class NoHandBossCombat : EnemyCombatV1
{
    public Colors[] colors;
    public int[] ratios;

    public bool playerIsOnPosition;


    float charge2;
    float playerSpeed;
    float playerHP;
    float timerSpawnMinienemy;

    GameObject stick;
    GameObject stickBullet;

    GameManager gm;
    GameObject miniEnemy;

    Vector3 curentPos;

    public GameObject handsContainer;

    public override void Init(ICharacter character)
    {
        base.Init(character);
        foreach (Colors c in colors)
        {
            this.character.AddColor(c);
        }
        character.SetBulletColor(colors[0]);

        target = GameObject.FindWithTag("Player");
        weapon = character.GetActiveWeapon();
        character.OnWeaponChange += OnWeaponChange;
        //this.character = character;
        aimDir = Vector3.down;
        //phase = 1;// begin on the tentacle attack

        target = GameObject.FindWithTag("Player");

        weapon = character.GetActiveWeapon();
        character.OnWeaponChange += OnWeaponChange;
        aimDir = Vector3.down;

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //CreateNewWeapon();//create a new weapon in the inventory

        miniEnemy = Resources.Load<GameObject>("Prefabs/Characters/MeleeAttackPathFinding");

    }

    public override void Update()
    {
        CheckIfPlayerIsOnPos();
        if (playerIsOnPosition == true)
        {
            target.GetComponent<MovementV1>().enabled = false;
            if (target == null) return;
            Aim();


            charge += Time.deltaTime * weapon.FireRate;
            if (charge >= fullCharge + 1)
            {
                SpawnMiniEnemy();
                charge = 0;
            }
            charge2 += Time.deltaTime * weapon.FireRate;
            if (charge2 >= fullCharge + 10)
            {
                Log.log("Shoot");
                charge2 = 0;
                Shoot();
            }
        }
        else
        {
            target.GetComponent<MovementV1>().enabled = true;
        }
    }
    void CheckIfPlayerIsOnPos()
    {
        float distY = target.transform.position.y - transform.position.y;
        float distX = target.transform.position.x - transform.position.x;
        //Log.log("distX" +distX);
        //Log.log("distY" +distY);
        if (distY > 5 && (distX < 0.5f && distX > -0.5f))
        {
            playerIsOnPosition = true;
        }
    }
    void SpawnMiniEnemy()
    {
            ICharacter enemy = Instantiate
                (miniEnemy, transform.position, Quaternion.identity).
                GetComponent<ICharacter>();
            gm.AddEnemy(enemy);
            enemy.SetColor(Colors.Blue);
            enemy.Init();
            timerSpawnMinienemy = 0;
    }
}
