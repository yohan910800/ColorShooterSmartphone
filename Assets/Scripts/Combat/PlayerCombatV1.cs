using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using UnityEngine.UI;
/*
Simple auto aim combat module 
*/
public class PlayerCombatV1 : MonoBehaviour, ICombat
{

    //imported code

    //public List<GameObject> targetList;
    //public List<GameObject> targetListFX;

    public GameObject targetFX;
    public bool pressRandomChooseWeaponJustOnce;
    public bool isMeleeModeActivate = false;
    public Weapon[] weapons;
    bool isTalking;
    bool isAiming;
    bool justOnce;
    bool justOnceMine;
    bool justOnceSound;//for sound
    bool meleeMode;
    public bool doesBonusMineIsActive = false;
    float timer;
    float dist;

    Inventory inventory;
    Player playerScript;
    //GameObject targetFX2;//temp

    Transform[] originSocket;
    Transform[] singleSocket;
    Transform[] singleSocket2;
    Transform[] dualWieldSockets;

    //


    GameManager gm;
    float fullCharge = 1.0f;
    float charge;
    float charge2;
    GameObject target;
    GameObject targe2;
    ICharacter character;
    ICharacter aimedTarget;
    IInputModule input;
    //Weapon weapon;
    //Weapon weapon2;
    Weapon originWeapon;
    Vector3 aimDir;
    Vector3 fingerPos;

    int shotCount;
    int bonusIndex;

    float reloadCountdown;
    float meleeModeReloadTouchTimer;

    CameraControl cameraControl;

    GameObject basicTarget;
    GameObject targetIcone;

    GameObject meleeModeHands;
    GameObject hand1Obj;
    GameObject hand2Obj;
    GameObject[] meleeModeImpactEffects;
    public GameObject[] meleeModeEffects;
    int meleeModeImpactIndex;

    int activeWeapNo = 0;

    int originAttAmount;
    public void Init(ICharacter character)
    {

        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        playerScript = GetComponent<Player>();
        this.character = character;
        input = character.GetInputModule();
        weapons = new Weapon[2];

        inventory = character.GetInventory();
        //character.OnWeaponChange += OnWeaponChange;
        aimDir = Vector3.down;

        var weaponSocket1 = transform.Find("Hand1");
        var weaponSocket2 = transform.Find("Hand2");
        dualWieldSockets = new Transform[] { weaponSocket1, weaponSocket2 };
        singleSocket = new Transform[] { weaponSocket2 };
        singleSocket2 = new Transform[] { weaponSocket1 };
        cameraControl = GameObject.FindWithTag("MainCamera").GetComponent<CameraControl>();
        targetFX = Resources.Load<GameObject>("Prefabs/Effects/TargetCircle");
        isMeleeModeActivate = false;

        targetIcone = Instantiate(targetFX, transform.position, Quaternion.identity);
        targetIcone.SetActive(false);

        hand1Obj = GameObject.Find("Player").transform.Find("Hand1").gameObject;
        hand2Obj = GameObject.Find("Player").transform.Find("Hand2").gameObject;
        meleeModeHands = GameObject.Find("Player").transform.Find("MeleeModeHands").gameObject;
        meleeModeImpactEffects = new GameObject[2];
        meleeModeImpactEffects[0] = GameObject.Find("Player").transform.Find("MeleeModeImpact").gameObject;
        meleeModeImpactEffects[1] = GameObject.Find("Player").transform.Find("MeleeModeImpact (1)").gameObject;

        //activeWeapNo = 1;
    }


    public void Update()
    {
        //Debug.Log("layer " + gameObject.layer);
        //weapons[1] = new TheEraser();
        //ActivateWeapon(new Shotgun());
        //ActivateWeapon2(weapons[1]);

        //doesBonusMineIsActive = true;
        //Log.log("poison " + weapon.GetBullet().GetComponent<StraightShotBullet>().doesPoisonedBulletIsActivate);
        //Log.log("" + weapon.doesDoubleShotIsActivate);
        //character.GetStats().GainEnergy(50);
        //Log.log("active weapon " + weapons[0]);
        //Log.log("active weapon 2 " + weapons[1]);
        //        Log.log("weapon name " + character.GetActiveWeapon().sprite.name);

        //check if the melee mode is active or not
        if (meleeMode == true)
        {
            //character.GetStateUI().Refresh();
            meleeModeReloadTouchTimer += Time.deltaTime;
            if (meleeModeReloadTouchTimer > 0.3f)
            {
                TeleportOnTouchPositionAndShoot(character.GetActiveWeapon());

            }
            LooseEnergy();
        }
        else
        {
            AttackWithWeapon();

            if (doesBonusMineIsActive == true)
            {
                OnShootMines();
            }
        }
    }

    public void ActiveMeleeMode()
    {
        Log.log("Activate");
        //if the player get 100 energy's point the meleemode is active
        Log.log("energy " + this.character.GetStats().Energy);

        if (this.character.GetStats().Energy >= 100)
        {
            if (meleeMode == false)
            {
                isMeleeModeActivate = true;
                originAttAmount = character.GetStats().Attack;
                character.GetStats().SetAttack(100);
                //this.character.GetStats().GainEnergy(100);//temp
                originWeapon = this.character.GetActiveWeapon();
                originSocket = this.character.GetActiveWeapon().sockets;
                Log.log("om basic weapon " + originWeapon.ToString());

                character.ActivateWeapon(inventory.Weapons[7]);
                weapons[0]= this.character.GetActiveWeapon();
                //
                playerScript.isInvincible = true;

                hand1Obj.SetActive(false);
                hand2Obj.SetActive(false);
                meleeModeHands.SetActive(true);
                foreach(GameObject effect in meleeModeEffects)
                {
                    effect.SetActive(true);
                }

                meleeMode = true;
            }
            else
            {
                Log.log("u cantt click twice");
            }
        }
        else
        {
            Log.log("energy is not full");
        }
    }

    void LooseEnergy()
    {
        character.GetStats().SetAttack(20);
        TimerBeforeMeleeAttStop();
        if (this.character.GetStats().Energy <= 0)
        {
            StopMeleeAtt(); //when the energy comes to 0 the got his weapon back
        }
    }

    void TimerBeforeMeleeAttStop()
    {
        //the player lose energy when he is in the MeleeAttack mode. 

        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            this.character.GetStats().LoseEnergy(10);
            playerScript.stateUIFixe.Refresh();
            timer = 0;
        }
    }
    void StopMeleeAtt()
    {
        if (originWeapon != null)
        {
            character.GetStats().SetAttack(originAttAmount);
            playerScript.isInvincible = false;
            character.GetStats().SetAttack(2);
            Log.log("Desactivate");
            weapons[0] = originWeapon;
            this.character.ActivateWeapon(weapons[0]);

            hand1Obj.SetActive(true);
            hand2Obj.SetActive(true);
            meleeModeHands.SetActive(false);
            foreach (GameObject effect in meleeModeEffects)
            {
                effect.SetActive(false);
            }
            //add effects

            meleeMode = false;
        }
    }

    void AttackWithWeapon()
    {
        //character.GetStats().SetAttack(2);
        if (character.ActivateAutoAim == true)
        {
            var closest = FindClosest();

            if (closest != null)
            {
                target = FindClosest();
                targetIcone.SetActive(true);
                UpdateTaregtEffect();
                //Log.log("target " + FindClosest().name);
            }
        }
        if (target == null)
        {
            //Destroy(targetIcone);
            targetIcone.SetActive(false);

        }
        Aim();

        ShootWithFirstWeapon();
        if (weapons[1] != null)
        {
            ShootWithSecondWeapon();
        }

    }


    void ShootWithFirstWeapon()
    {
        if (target != null)
        {
            dist = Vector3.Distance(transform.position, target.transform.position);
                if (dist < weapons[0].range /*&& target.tag != "NotAimable"*/)
                {
                    if (weapons[0].IsReloading)
                    {
                        reloadCountdown -= Time.deltaTime;
                        if (reloadCountdown <= 0)
                        {
                            weapons[0].IsReloading = false;
                        }
                        else
                        {
                            return;
                        }
                    }
                    charge += Time.deltaTime * weapons[0].FireRate;
                    if (target != null && charge >= fullCharge)
                    {
                    if (Shoot(weapons[0]))
                    {
                        ChooseWichSoundShouldBePlayed();
                        charge = 0;
                    }
                        
                        
                    }
                }
        }
    }
    void ShootWithSecondWeapon()
    {
        if (target != null)
        {
            dist = Vector3.Distance(transform.position, target.transform.position);
            
                if (dist < weapons[1].range /*&& target.tag != "NotAimable"*/)
                {
                    if (weapons[1].IsReloading)
                    {
                        reloadCountdown -= Time.deltaTime;
                        if (reloadCountdown <= 0)
                        {
                            weapons[1].IsReloading = false;
                        }
                        else
                        {
                            return;
                        }
                    }
                    charge2 += Time.deltaTime * weapons[1].FireRate;
                    if (target != null && charge2 >= fullCharge)
                    {
                    if (Shoot(weapons[1]))
                    {
                        charge2 = 0;
                        ChooseWichSoundShouldBePlayed2();
                        
                    }
                    }
                }
        }
    }


    bool Shoot(Weapon w)
    {
        if (input.GetDirection() == Vector2.zero)
        {
            bool shot = w.Shoot(target, aimDir);
            if (shot) shotCount++;
            if (w.MagSize > 0 && shotCount >= w.MagSize)
            {
                w.IsReloading = true;
                shotCount = 0;
                reloadCountdown = w.ReloadTime;

                return false;
            }
            StartCoroutine(cameraControl.CameraShake(0.1f, 0.2f));
            
            return shot;
        }
        else
        {
            return false;
        }
    }

    void Aim()
    {
        for (int i = 0; i <= activeWeapNo; i++)
        {
            Vector3 direction;
            if (target != null && dist < weapons[i].range)
            {//dist yohan added
                direction = target.transform.position - gameObject.transform.position;
            }
            else
            {
                direction = character.GetInputModule().GetDirection();
            }
            if (direction.magnitude > 0)
            {
                foreach (Transform t in weapons[i].sockets)
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
    }


    GameObject FindClosest()
    {
        GameObject closest = null;
        foreach (ICharacter enemy in gm.ActiveEnemies)
        {
            GameObject enemyObject = enemy.GetGameObject();
            if (enemyObject.tag == "NotAimable") continue;
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

    void UpdateTaregtEffect()
    {
        targetIcone.transform.position = target.transform.position;
    }


    void TeleportOnTouchPositionAndShoot(Weapon w)
    {
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {

            Log.log("it works !");
            // get mouse position in screen space
            // (if touch, gets average of all touches)
            Vector3 screenPos = Input.mousePosition;
            // set a distance from the camera
            screenPos.z = 10.0f;
            // convert mouse position to world space
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, worldPos/*, Vector3.Distance(transform.position,
               -lr.transform.position +
               Owner.GetGameObject().transform.position), layerMask*/);
            Debug.DrawRay(worldPos, worldPos);
            Log.log("coll name " + hit.collider.name);
            if (hit.collider.name == "Tilemap" || hit.collider.name == "Tilemap (1)")
            {
                Log.log("u cannot teleport there");
                return;
            }
            else
            {
                // get current position of this GameObject
                Vector3 newPos = transform.position;
                // set x position to mouse world-space x position
                newPos = worldPos;
                // apply new position
                transform.position = newPos;

                target = FindClosest();
                Shoot(w);
                meleeModeReloadTouchTimer = 0;
                character.GetAnimationManager().
                    animator.SetTrigger("ActiveMeleeAttack");
                
                meleeModeHands.SetActive(true);
                ChooseWichSoundShouldBePlayed();

                //var effect = gm.WorldColor == Colors.Brown ? runSmokeEffect : runSmokeEffectOrange;
                meleeModeImpactEffects[meleeModeImpactIndex % 2].transform.position = transform.position;
                meleeModeImpactEffects[meleeModeImpactIndex % 2].SetActive(true);
                meleeModeImpactIndex++;
                //timerSmoke = 0;
                meleeModeImpactEffects[meleeModeImpactIndex % 2].SetActive(false);
                meleeModeImpactEffects[meleeModeImpactIndex % 2].SetActive(false);
            }
        }
    }
    void CheckIfGrid(Vector3 worldPos)
    {


    }
    //void OnWeaponChange(Weapon weapon)
    //{
    //    this.weapon = weapon;
    //    foreach (Transform t in weapon.sockets)
    //    {
    //        t.right = aimDir;
    //        if (t.localEulerAngles.y != 0)
    //        {
    //            t.localEulerAngles = new Vector3(0, 0, 180.0f);
    //        }
    //    }
    //}

    public Vector3 GetAimDirection()
    {
        return aimDir;
    }


    ///
    ///bonus part
    ///


    void OnShootMines()
    {
        if (input.GetDirection() != Vector2.zero)
        {
            justOnceMine = true;
        }
        else
        {
            if (justOnceMine == true)
            {
                GameObject mine = Instantiate(Resources.Load<GameObject>("Prefabs/Bonus/Mine"),
                    transform.position, Quaternion.identity);
                Bullet mineBullet = mine.GetComponent<Bullet>();
                mineBullet.Init(character, Vector3.zero, 0.005f, 10, 0.1f, Colors.Red, target);

                justOnceMine = false;
            }
        }
    }

    //yohan added
    //public void ChooseRandomWeapon()
    //{
    //    //if (pressRandomChooseWeaponJustOnce == false)
    //    //{
    //        bonusIndex = Random.Range(1, 3);
    //        TierRandomNumber(bonusIndex);
    //        Log.log("bonus index" +bonusIndex);
    //        //weapon.GetBullet().doesPoisonedBulletIsActivate = true;
    //        //originWeapon = character.GetActiveWeapon();

    //        //int rnd = Random.Range(0, character.GetInventory().Weapons.Count);

    //        //character.ActivateWeapon(character.GetInventory().Weapons[rnd]);

    //        //while (originWeapon == character.GetActiveWeapon())
    //        //{
    //        //    rnd = Random.Range(0, character.GetInventory().Weapons.Count);
    //        //    character.ActivateWeapon(character.GetInventory().Weapons[rnd]);
    //        //    if (originWeapon != character.GetActiveWeapon())
    //        //    {
    //        //        break;
    //        //    }
    //        //}


    //        //character.ActivateWeapon(character.GetInventory().Weapons[rnd]);
    //        pressRandomChooseWeaponJustOnce = true;
    //    //}
    //}

    public void TierRandomNumber(int tierIndex, int index)
    {
        if (tierIndex == 0)
        {
            OnChooseWeapon(index, character.GetActiveWeapon());
        }
        else if (tierIndex == 1)
        {
            OnChooseBonus(index);
        }
        else if (tierIndex == 2)
        {
            OnChooseBonus(index);
        }
        else if (tierIndex == 3)
        {
            OnChooseWeapon2(index, weapons[1]);
            activeWeapNo = 1;
        }
        else if (tierIndex == 4)
        {
            Log.log("bonus not created yet");
        }
        else if (tierIndex == 5)
        {
            Log.log("bonus not created yet");

        }
    }

    public void OnChooseWeapon(int index, Weapon weapon) //note from dohan- unused parameter (Weapon weapon)
    {
        switch (index)
        {
            case 1:
                weapons[0] = inventory.Weapons[8];//TripleSingleShooter
                ActivateWeapon(weapons[0]);
                break;
            case 2:
                weapons[0] = inventory.Weapons[3];//Rifle
                ActivateWeapon(weapons[0]);
                break;
            case 3:
                weapons[0] = inventory.Weapons[2];//ShotGun
                ActivateWeapon(weapons[0]);
                break;
            default:
                Debug.Log("weapon index not matching onchooseWeapon index");
                break;
        }
    }
    public void OnChooseWeapon2(int index, Weapon weapon)
    {
        switch (index)
        {
            case 1:
                weapons[1] = new TheMiddleFingerGun() /*inventory.Weapons[4]*/;//TheMiddleFingerGun
                ActivateWeapon2(weapons[1]);
                break;
            case 2:
                weapons[1] = new TheEraser() /*inventory.Weapons[5]*/;//TheEraser
                ActivateWeapon2(weapons[1]);

                character.GetStats().ModifySpeed(-1.0f);
                break;
            case 3:
                weapons[1] = new LaserSurgeryGun()/*inventory.Weapons[9]*/;//LaserSurgeryGun
                ActivateWeapon2(weapons[1]);
                break;
            default:
                Debug.Log("weapon index not matching onchooseWeapon index");
                break;
        }

    }
    void ActivateWeapon(Weapon weapon)
    {
        //weapon.Init(character, singleSocket);
        //playerScript.AddWeapon(weapon);
        playerScript.ActivateWeapon(weapon);
    }
    void ActivateWeapon2(Weapon weapon2)
    {
        weapon2.Init(character, singleSocket2);
        playerScript.AddWeapon(weapon2);
        playerScript.ActivateAllWeapons(weapon2);
        //playerScript.ActivateWeapon2(weapon2);
    }

    void OnChooseBonus(int index)
    {
        switch (index)
        {
            case 1:
                weapons[0].doesItHaveDoubleShot = true;
                break;
            case 2:
                weapons[0].doesItHavePoisonBullet = true;
                break;
            case 3:
                doesBonusMineIsActive = true;
                break;
        }
    }

    /// <summary>
    /// touch to aim. not use anymore
    /// </summary>
    ///
    /// 

    void OnAimLock(ICharacter character)
    {
        //aimedTarget = character;
        Log.log("clicked character is " + character.GetGameObject().name);
        if (meleeMode == true)
        {
            //TeleportOnTouchPosition();
            //target = character.GetGameObject();
            //transform.position = target.transform.position;
            //Shoot();
        }
        else
        {
            if (character.GetGameObject() != gameObject && character.GetGameObject().tag != "NPCTutorial")
            {
                //ChooseTarget(character);
            }
        }
    }
    public void Terminate()
    {
        input.OnAimLock -= OnAimLock;
    }

    void ChooseWichSoundShouldBePlayed()
    {
        switch (weapons[0].ToString())
        {
            case "SingleShooter":
                PlayWeaponSound("SingleShooter1");
                break;
            case "Shotgun":
                PlayWeaponSound("ShotGun1");
                break;
            case "Rifle":
                PlayWeaponSound("Rifle1");
                break;
            case "TripleSingleShooter":
                PlayWeaponSound("TripleSingleShooter1");
                break;
            case "MeleeMode":
                PlayWeaponSound("MeleeModeImpact1");
                break;
        }
    }
    void ChooseWichSoundShouldBePlayed2()
    {
        if (weapons[1] != null)
        {
            switch (weapons[1].ToString())
            {
                case "TheEraser":
                    PlayWeaponSound("TheEraser");
                    break;
                case "TheMiddleFingerGun":
                    PlayWeaponSound("MiddleFingerGun1");
                    break;
                case "LaserSurgeryGun":
                    PlayWeaponSound("LaserShoot1");
                    break;
                case "MeleeMode":
                    PlayWeaponSound("MeleeModeImpact1");
                    break;
            }
        }
    }

    void PlayWeaponSound(string soundName)
    {
        FindObjectOfType<AudioManager>().Play(soundName);
    }
    public Weapon getweaponfromWeaponsArray(int index)
    {
        return weapons[index];

    }
}



//void AimingFXFollowsTarget()
//{
//    if (targetFX != null)
//    {
//        if (target == null)
//        {
//            //targetListFX.Clear();
//            Destroy(targetFX2);
//        }
//        else
//        {
//            if (targetFX2 != null)
//            {
//                targetFX2.transform.position = target.transform.position;
//                //targetListFX[0].transform.position += target.transform.position;
//            }
//        }
//    }
//}
//void DisableAim(ICharacter character)
//{
//    Destroy(targetFX2);
//    target = FindClosest();
//    isAiming = false;
//    //targetListFX.Clear();
//    targetList.RemoveAt(1);
//    targetList.RemoveAt(0);
//}
//void ChooseTarget(ICharacter character)
//{
//    if (character.GetGameObject().tag != "Player")
//    {
//        character.GetGameObject().GetComponent<Ranged1>().isAimedByPlayer = true;
//        Log.log("aimed by  " + character.GetGameObject().GetComponent<Ranged1>().isAimedByPlayer);
//        if (targetList.Count == 0)
//        {
//            OnFirstAim(character);
//        }
//        else if (targetList.Count == 1)
//        {
//            targetList.Insert(0, character.GetGameObject());
//            if (targetList[0] == targetList[1])
//            {
//                DisableAim(character);
//            }
//            else
//            {
//                ChangeTarget(character);
//            }
//        }
//    }
//}
//void OnFirstAim(ICharacter character)
//{
//    targetList.Add(character.GetGameObject());
//    target = targetList[0];
//    targetFX2 = Instantiate(targetFX, target.transform.position, Quaternion.identity);
//    //targetListFX.Add(targetFX2);
//    isAiming = true;
//}
//void ChangeTarget(ICharacter character)
//{
//    Destroy(targetFX2);
//    targetList.RemoveAt(1);
//    target = targetList[0];
//    targetFX2 = Instantiate(targetFX, target.transform.position, Quaternion.identity);
//    //targetListFX.Insert(0, targetFX2);
//    //targetListFX.RemoveAt(1);
//    isAiming = true;
//}