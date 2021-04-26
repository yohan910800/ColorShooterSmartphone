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
    public GameObject targetFX;
    public bool pressRandomChooseWeaponJustOnce;
    public bool isMeleeModeActivate = false;
    public Weapon[] weapons;
    public GameObject[] meleeModeEffects;
    public bool doesBonusMineIsActive = false;

    bool isTalking;
    bool isAiming;
    bool justOnce;
    bool justOnceMine;
    bool justOnceSound;//for sound
    bool meleeMode;
    
    float timer;
    float dist;
    float fullCharge = 1.0f;
    float charge;
    float charge2;
    float reloadCountdown;
    float meleeModeReloadTouchTimer;

    Inventory inventory;
    Player playerScript;

    Transform[] originSocket;
    Transform[] singleSocket;
    Transform[] singleSocket2;
    Transform[] dualWieldSockets;

    GameManager gm;

    GameObject target;
    GameObject targe2;
    ICharacter character;
    ICharacter aimedTarget;
    IInputModule input;
    Weapon originWeapon;
    Vector3 aimDir;
    Vector3 fingerPos;

    int shotCount;
    int bonusIndex;
    int meleeModeImpactIndex;
    int activeWeapNo = 0;
    int originAttAmount;
    
    CameraControl cameraControl;

    GameObject basicTarget;
    GameObject targetIcone;
    GameObject meleeModeHands;
    GameObject hand1Obj;
    GameObject hand2Obj;
    GameObject[] meleeModeImpactEffects;
    public void Init(ICharacter character)
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        playerScript = GetComponent<Player>();
        this.character = character;
        input = character.GetInputModule();
        weapons = new Weapon[2];
        inventory = character.GetInventory();
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
    }


    public void Update()
    {
        
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
                originWeapon = this.character.GetActiveWeapon();
                originSocket = this.character.GetActiveWeapon().sockets;
                Log.log("om basic weapon " + originWeapon.ToString());

                character.ActivateWeapon(inventory.Weapons[7]);
                weapons[0]= this.character.GetActiveWeapon();
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
            }
        }
        if (target == null)
        {
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
                if (dist < weapons[0].range )
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
            
                if (dist < weapons[1].range)
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
            RaycastHit2D hit = Physics2D.Raycast(worldPos, worldPos);
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

                meleeModeImpactEffects[meleeModeImpactIndex % 2].transform.position = transform.position;
                meleeModeImpactEffects[meleeModeImpactIndex % 2].SetActive(true);
                meleeModeImpactIndex++;
                meleeModeImpactEffects[meleeModeImpactIndex % 2].SetActive(false);
                meleeModeImpactEffects[meleeModeImpactIndex % 2].SetActive(false);
            }
        }
    }
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
                weapons[1] = new TheMiddleFingerGun();//TheMiddleFingerGun
                ActivateWeapon2(weapons[1]);
                break;
            case 2:
                weapons[1] = new TheEraser() ;//TheEraser
                ActivateWeapon2(weapons[1]);

                character.GetStats().ModifySpeed(-1.0f);
                break;
            case 3:
                weapons[1] = new LaserSurgeryGun();//LaserSurgeryGun
                ActivateWeapon2(weapons[1]);
                break;
            default:
                Debug.Log("weapon index not matching onchooseWeapon index");
                break;
        }

    }
    void ActivateWeapon(Weapon weapon)
    {
        playerScript.ActivateWeapon(weapon);
    }
    void ActivateWeapon2(Weapon weapon2)
    {
        weapon2.Init(character, singleSocket2);
        playerScript.AddWeapon(weapon2);
        playerScript.ActivateAllWeapons(weapon2);
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

    //will probably be delete later
    void OnAimLock(ICharacter character)
    {
        Log.log("clicked character is " + character.GetGameObject().name);
        if (meleeMode == true)
        {
        }
        else
        {
            if (character.GetGameObject() != gameObject && character.GetGameObject().tag != "NPCTutorial")
            {
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
