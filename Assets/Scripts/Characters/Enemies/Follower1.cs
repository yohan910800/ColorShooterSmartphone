using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System;

public class Follower1 : Character
{
    public string jsonFileDialogName;
    public FloatingDialogManager floatingDialog;

    GameObject gObj;

    [Range(1, 3)]
    public int socketNo;
    public GruntWeapon weapon;
    public bool isAimedByPlayer;// yohan added


    public enum GruntWeapon
    {
        SingleShooter,

    }
    Transform[][] sockeColl;

   
    public override void Init()
    {
        base.Init();
          var weaponSocket1 = transform.Find("Hand1");
            var weaponSocket2 = transform.Find("Hand2");
            sockeColl = new Transform[][]{

            new Transform[]{weaponSocket2},
            new Transform[]{weaponSocket1,weaponSocket2},
        };
            Weapon weap = Activator.CreateInstance(Type.GetType(weapon.ToString())) as Weapon;
            weap.Init(this, sockeColl[socketNo - 1]);
            AddWeapon(weap);
            ActivateWeapon(inventory.Weapons[0]);
    }


    protected override void InitInput()
    {
        base.InitInput();
    }

    void GetHit(int dmg)
    {
        stats.LowerHP(dmg);
        stateUI.Refresh();
        if (stats.HP == 0 && IsAlive)
        {
            OnDeath();
        }
        if (gm.showDamageText)
        {
            var gObj = Instantiate(damageTextPrefab, transform.position, Quaternion.identity) as GameObject;
            DamageText dt = gObj.GetComponent<DamageText>();
            dt.Init(dmg, Color.black);
        }
    }

    //imported from testdialogmanager
    void ActiveDialogWithoutTrigger()
    {
        floatingDialog.NewDialog(jsonFileDialogName);
        floatingDialog.OnDialogEnded += OnDialogEnd;
    }



    //instantiate the dialog text
    public void ShowDialogText()
    {
        float dist = Vector3.Distance(transform.position, gm.player.transform.position);

        if (gm.showDialogText == true)
        {
            gObj = Instantiate(dialogTextPrefab, transform.position, Quaternion.identity) as GameObject;
            floatingDialog = gObj.GetComponent<FloatingDialogManager>();
            //dialog.Init("Test", Color.white);
        }
    }

    public void TextFollowEnemy()
    {
        if (gObj != null)
        {
            gObj.transform.position = transform.position;
        }
    }
    public void ActivateDialogFromMapManager()
    {
        ShowDialogText();
        ActiveDialogWithoutTrigger();
    }

    void OnDialogEnd()
    {
        if (floatingDialog != null)
        {
            floatingDialog.OnDialogEnded -= OnDialogEnd;
        }

    }

    public override bool HitCheck(Bullet bullet)
    {
        return false;
    }

    protected override void OnDeath()
    {
        if (isAimedByPlayer == true)
        {
            Destroy(GameObject.Find("TargetCircle(Clone)"));
        }
        IsAlive = false;
        stateUI.OnDeath();
        base.OnDeath();
        if (floatingDialog != null)
        {

            floatingDialog.OnDialogEnded -= OnDialogEnd;
        }
        Destroy(gameObject);
    }
}
