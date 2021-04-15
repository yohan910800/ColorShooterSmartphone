using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MankindGames;

public class ShieldHeadFishGun : MonoBehaviour
{
    Stats stats;
    Image hpBar;
    Image energyBar;
    Color tmp;
    GameObject target;

    bool isFixed;
    bool hasEnergy;
    int shieldHP;
    int shieldMaxHP;
    string bulletOwnerTag;

    void Start()
    {
        shieldHP = 20;
        shieldMaxHP = 20;
        tmp = GetComponent<SpriteRenderer>().color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>().Owner.GetGameObject().tag == "Player")
        {
            LowerHP(collision.GetComponent<Bullet>().Damage);
            Log.log("Enter " + collision.GetComponent<Bullet>().Damage);
            Log.log("current hp " + shieldHP);
            Destroy(collision.gameObject);
        }
    }
    //public void Refresh()
    //{
    //    hpBar.fillAmount = GetNormalizedHP();
    //    if (hasEnergy) energyBar.fillAmount = stats.GetNormalizedEnergy();
    //}

    public int LowerHP(int dmg)
    {
        shieldHP -= dmg;
        tmp.a -= 0.1f;
        GetComponent<SpriteRenderer>().color = tmp;
        if (shieldHP < 0)
        {
            shieldHP = 0;

            Destroy(gameObject);
        }
        return shieldHP;
    }
    //public float GetNormalizedHP()
    //{
    //    return (float)shieldHP / (float)shieldMaxHP;
    //}




    //void Update()
    //{

    //}
}
