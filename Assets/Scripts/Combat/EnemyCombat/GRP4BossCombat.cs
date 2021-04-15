using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class GRP4BossCombat : EnemyCombatV1
{

    public Colors[] colors;
    public int[] ratios;

    public List<GameObject> targetList;
    public List<GameObject> targetListFX;

    public GameObject targetFX;//temp

    bool isTalking;
    bool isAiming;
    bool meleeMode;
    bool weaponForChangedMelee;

    float timer;
    int phase;
    IInputModule input;
    Weapon originWeapon;

    Inventory inventory;
    Ranged1 bossScript;
    GRP3BossInput bossInput;

    Transform[] originSocket;
    Transform[] singleSocket;
    Transform[] dualWieldSockets;

    public override void Init(ICharacter character)
    {
        base.Init(character);
        foreach (Colors c in colors)
        {
            this.character.AddColor(c);
        }
        character.SetBulletColor(colors[0]);

        weapon = character.GetActiveWeapon();
        character.OnWeaponChange += OnWeaponChange;

        var weaponSocket1 = transform.Find("Hand1");
        var weaponSocket2 = transform.Find("Hand2");
        dualWieldSockets = new Transform[] { weaponSocket1, weaponSocket2 };
        singleSocket = new Transform[] { weaponSocket2 };

        phase = character.phase;
    }
     void Start()
    {
        bossScript = GetComponent<Ranged1>();
        bossInput = GetComponent<GRP3BossInput>();
        EquipeQuickBazookaWeapon();
    }

    public override void Update()
    {
        phase = character.phase;

        if (phase == 0)
        {
            TimerBeforeMeleeAttStop();
            Attack();
        }
        else if(phase==3)
        {
            meleeMode = true;
            ActiveMeleeMode();
            
        }
    }

    void Attack()
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

            if (Shoot()) charge = 0;

        }
    }

    public void ActiveMeleeMode()
    {
        if (meleeMode == true)
        {
            if (weaponForChangedMelee == false)
            {
                BossChangesWeapon();
            }
            else
            {
                Aim();
                charge += Time.deltaTime * weapon.FireRate;
                if (target != null && charge >= fullCharge)
                {
                    charge = 0;
                    Shoot();
                }
            }
        }
    }

    void BossChangesWeapon()
    {
        /*another way to change wepaon*/
        Log.log("Activate");
        //this.character.GetStats().GainEnergy(100);//temp
        originWeapon = this.character.GetActiveWeapon();
        originSocket = this.character.GetActiveWeapon().sockets;
        Log.log("om basic weapon " + originWeapon.ToString());

        weapon = new MeleeMode();
        weapon.Init(character, dualWieldSockets);
        bossScript.AddWeapon(weapon);
        character.ActivateWeapon(weapon);
        weaponForChangedMelee = true;
    }

    
    void TimerBeforeMeleeAttStop()
    {
        //timer += Time.deltaTime;
        //if (timer >= 10)
        //{
            StopMeleeAtt();
            //character.GetStats().SetSpeed(30);
            bossInput.onPosition = false;
            timer = 0;
        //}
    }
    void StopMeleeAtt()
    {
        if (originWeapon != null)
        {
            Log.log("Desactivate");

            EquipeQuickBazookaWeapon();
            meleeMode = false;
            weaponForChangedMelee = false;
        }
    }
   
    void EquipeQuickBazookaWeapon()
    {
        OnWeaponChange(character.GetInventory().Weapons[0]);
        this.character.ActivateWeapon(character.GetInventory().Weapons[0]);
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
            if (colors.Length > 1) SetRandomBulletColor();
            return base.Shoot();
    }
    public int GetShotCount()
    {
        return shotCount;
    }
}
