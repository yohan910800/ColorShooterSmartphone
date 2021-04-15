using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using Random = UnityEngine.Random;

public class LastBossCombat : EnemyCombatV1
{
    public Colors[] colors;
    public int[] ratios;

    public bool playerIsOnPosition;
    public bool isOnTheMiddle;
    public bool didBossShot;
    public bool didBossShotPillar;
    public bool didBossShotUltimAtt;
    public bool ultimAttIsSpreading;

    public GameObject handsContainer;
    public GameObject ultimateBullet;
    public float numberOfAliveBullet;
    public ICharacter chara;

    float chargeZoneAttack;
    float chargeMeleeAttack;
    float chargeShootAliveBullet;
    float chargeShootPillar;
    float chargeShootUltimAtt;
    float playerSpeed;
    float playerHP;

    float timerSpawnAliveBullet;
    float numberOfShoot;

    

    GameObject stick;
    GameObject stickBullet;
    Vector3 curentPos;
    public Vector3 originPos;
    LastBossInput getBossInput;

    Transform[][] sockeColl;// take the weapon socket

    Weapon weap2;

    GameManager gm;

    int phase; 

    public override void Init(ICharacter character)
    {
        base.Init(character);
        foreach (Colors c in colors)
        {
            this.character.AddColor(c);
        }
        character.SetBulletColor(colors[0]);
        chara = character;
        target = GameObject.FindWithTag("Player");
        weapon = character.GetActiveWeapon();
        character.OnWeaponChange += OnWeaponChange;
        //this.character = character;
        aimDir = Vector3.down;

        getBossInput = GetComponent<LastBossInput>();

        originPos = transform.position;
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        CreateNewWeapon();
        phase = character.phase;
    }

    void CreateNewWeapon()
    {
        var weaponSocket1 = transform.Find("Hand1");
        var weaponSocket2 = transform.Find("Hand2");
        sockeColl = new Transform[][]{
            new Transform[]{weaponSocket1},
            new Transform[]{weaponSocket2},
        };
        Weapon weap = new LastBossUltimateBulletAttack() as Weapon;
        weap.Init(character, sockeColl[0]);
        character.AddWeapon(weap);

        weap2 = new LastBossMeleeMode() as Weapon;
        weap2.Init(character, sockeColl[0]);
        character.AddWeapon(weap2);
    }
    void EquipeWeaponUltimateBulletAttack()
    {
        OnWeaponChange(character.GetInventory().Weapons[0]);
        character.ActivateWeapon(character.GetInventory().Weapons[0]);
    }

    void EquipeWeaponMeleeMode()
    {
        OnWeaponChange(character.GetInventory().Weapons[1]);
        character.ActivateWeapon(character.GetInventory().Weapons[1]);
    }
    

    public override void Update()
    {
        phase = character.phase;
        if (target == null) return;
        Aim();

        if (getBossInput.zoneShooted == false && phase == 3)
        {
            OnZoneAttack();//phase3
        }

        if (phase == 2)
        {
            MeleeAttack();//phase 2
        }

        if (isOnTheMiddle == true)
        {
            
            if (phase == 4)
            {
                OnShootAliveBullet();//phase 4
            }
            if (phase == 5 && didBossShotPillar == false)
            {
                OnShootPillar();
            }
            if (phase == 6)
            {
                SpreadUltimeAttack();
            }
            if (phase == 6 && didBossShotUltimAtt == false)
            {
                OnShootPillar();
                OnShootUltimateBullet();
            }
        }
    }
    void OnShootUltimateBullet()
    {
        EquipeWeaponUltimateBulletAttack();
        chargeShootUltimAtt += Time.deltaTime;
        if (chargeShootUltimAtt > 3)
        {
            if (didBossShotPillar == false)
            {
                //Shoot5();//shoot the pillar
                InstantiatePillar();
                InstantiateSafeZone();
                didBossShotPillar = true;//new
                Shoot();//shoot the ultimate bullet

                didBossShotUltimAtt = true;
                ultimAttIsSpreading = true;
                SpreadUltimeAttack();
            }
            else
            {
                //Shoot6();
                Shoot();//shoot the ultimate bullet
                InstantiateSafeZone();
                didBossShotUltimAtt = true;
                ultimAttIsSpreading = true;
                SpreadUltimeAttack();
            }
        }
    }
    
    void OnShootPillar()
    {
        chargeShootPillar += Time.deltaTime;
        if (chargeShootPillar > 3)
        {
            //Shoot5();
            InstantiatePillar();
            InstantiateSafeZone();
            didBossShotPillar = true;
            chargeShootPillar = 0;
        }
    }
    void OnShootAliveBullet()
    {
        chargeShootAliveBullet += Time.deltaTime;
        if (chargeShootAliveBullet > 1)
        {
            if (numberOfAliveBullet <= 3)
            {
                //Shoot4();
                ShootAliveBullet();
                numberOfAliveBullet++;
                chargeShootAliveBullet = 0;
            }
        }
    }

    //phase 6
    void SpreadUltimeAttack()
    {
        if (didBossShotPillar == true)
        {
            if (ultimAttIsSpreading == true)
            {
                ultimateBullet = GameObject.FindGameObjectWithTag("UltimateBullet");
                if (ultimateBullet != null)
                {
                    //Log.log("ultimate bullet" + ultimateBullet.name);
                    ultimateBullet.transform.localScale +=
                    new Vector3(5 * Time.deltaTime, 5 * Time.deltaTime, 0);
                    if (ultimateBullet.transform.localScale.x > 100 ||
                        ultimateBullet.transform.localScale.y > 100)
                    {
                        ultimAttIsSpreading = false;
                    }
                }
            }
        }
    }
    //phase 2
    void MeleeAttack()
    {
        EquipeWeaponMeleeMode();
        if (getBossInput.onMeleeAttackPosition == true)
        {
            chargeMeleeAttack += Time.deltaTime * weapon.FireRate;

            if (chargeMeleeAttack > 1f)
            {
                Log.log("SHOOT PHASE 2");
                Shoot();
                chargeMeleeAttack = 0;
                getBossInput.onMeleeAttackPosition = false;
            }
        }
    }
    //phase3
    void OnZoneAttack()
    {
        chargeZoneAttack += Time.deltaTime;
        if (chargeZoneAttack > 5)//temp condition
        {
            Log.log("attack!");
            getBossInput.zoneShooted = true;
            // zone attack
            ZoneAttack();
            //SecondShoot();
            //didBossShot = true;
            chargeZoneAttack = 0;
        }
    }
    //tmp
    public void InstantiatePillar()
    {
        //instantiate safe zone only if it instantiate at the same time than the ultimate bullet
        if (phase == 6)
        {

            // instantitiate pillar
            GameObject pillar = Resources.Load<GameObject>("Prefabs/Bullets/PillarLastBossSprite");

            GameObject pillarObj = MonoBehaviour.Instantiate
                   (pillar, originPos/*new Vector3(0.0f, 0.0f, 0.0f)*/,
                   Quaternion.identity) as GameObject;
        }
        else
        {
            // instantitiate pillar
            if (GameObject.Find("PillarLastBossSprite(Clone)") == null)
            {
                GameObject pillar = Resources.Load<GameObject>("Prefabs/Bullets/PillarLastBossSprite");

                GameObject pillarObj = MonoBehaviour.Instantiate
                       (pillar, originPos/*new Vector3(0.0f, 0.0f, 0.0f)*/,
                       Quaternion.identity) as GameObject;
            }
        }
    }

    void InstantiateSafeZone()
    {
        GameObject safeZone = Resources.Load<GameObject>("Prefabs/Bullets/CircleProtectPlayer");

        GameObject safeZoneObj = MonoBehaviour.Instantiate
                   (safeZone, originPos - new Vector3(1f, 1f, 0.0f),
                   Quaternion.identity) as GameObject;
        //safeZone.transform.position = new Vector3(0.33f, 0.5f, 0.0f);

    }

    void ZoneAttack()
    {
        GameObject lastBossZone = Resources.Load<GameObject>("Prefabs/Bullets/LastBossZone");
        //foreach (Transform t in sockets)
        //{
            Vector3[] directionDown = new Vector3[8];//size temp

            Vector3[] directionUp = new Vector3[8];//size temp

            for (int j = 0; j < 7; j++)
            {
                for (int i = 0; i < /*directionDown.Length*/10; i++)
                {
                    directionDown[j] = originPos + new Vector3(12.0f, -10.0f, 0.0f) + 
                    new Vector3(-j * 4, -i * 4, 0.0f);

                    //GameObject lastBossZoneObj = MonoBehaviour.Instantiate
                    //        (lastBossZone, directionDown[j],
                    //        Quaternion.identity) as GameObject;
                    //GameObject lastBossZoneObj3 = MonoBehaviour.Instantiate
                    //        (lastBossZone, -directionDown[j],
                    //        Quaternion.identity) as GameObject;

                    directionUp[j] = originPos+new Vector3(13.0f,-10.0f,0.0f) + 
                    new Vector3(-i * 4, j * 4, 0.0f);

                    GameObject lastBossZoneObj2 = MonoBehaviour.Instantiate
                            (lastBossZone, -directionUp[j],
                            Quaternion.identity) as GameObject;
                    GameObject lastBossZoneObj4 = MonoBehaviour.Instantiate
                            (lastBossZone, directionUp[j],
                            Quaternion.identity) as GameObject;

                //lastBossZoneObj.transform.localScale = new Vector3(2.5f, 2.5f, 1.0f);
                lastBossZoneObj2.transform.localScale = new Vector3(2.5f, 2.5f, 1.0f);
                //lastBossZoneObj3.transform.localScale = new Vector3(2.5f, 2.5f, 1.0f);
                lastBossZoneObj4.transform.localScale = new Vector3(2.5f, 2.5f, 1.0f);

                    //getBossInput.zoneContainer.Add(lastBossZoneObj.transform.position);
                    getBossInput.zoneContainer.Add(lastBossZoneObj2.transform.position);
                    //getBossInput.zoneContainer.Add(lastBossZoneObj3.transform.position);
                    getBossInput.zoneContainer.Add(lastBossZoneObj4.transform.position);

                }
            //}
        }
    }
    void ShootAliveBullet()
    {
        GameObject aliveBullet = Resources.Load<GameObject>
            ("Prefabs/Bullets/GRAliveBullet");

        ICharacter enemy = Instantiate
            (aliveBullet, transform.position, Quaternion.identity).GetComponent<ICharacter>();
        gm.AddEnemy(enemy);

        enemy.SetColor(Colors.Green);
        enemy.Init();
        enemy.GetStats().SetMaxHP(1000);
        enemy.GetStats().SetHP(1000);
        enemy.GetStats().SetSpeed(2);
    }
}
