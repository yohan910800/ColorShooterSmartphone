using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
[System.Serializable]

public class Weapon
{

    public float FireRate { get { return fireRate; } protected set { fireRate = value; } }
    public float range;
    public Transform[] sockets;
    public Sprite sprite;
    public bool IsReloading { get; set; }
    public int MagSize { get; protected set; }
    public float ReloadTime { get; protected set; }
    public int socketIndex { get; set; }

    public float barrelLen;
    // bonus
    public int bonusIndex;
    public bool doesItHaveDoubleShot = false;
    public bool doesItHavePoisonBullet = false;

    protected int damage;
    protected float fireRate;
    protected string bulletName;
    protected string spriteName;
    protected GameObject bulletPrefab;
    protected GameObject blankPrefab;
    protected float bulletSpeed;
    protected ICharacter owner;
    protected Colors bulletColor;
    protected bool isActive;
    
    protected List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    protected GameManager gm;
    protected GameObject particlePrefab;

    Bullet bullet;
    GameObject bulletObj;
    Stats stats;

    public virtual void Init(ICharacter character, Transform[] sockets)
    {
        owner = character;
        this.sockets = sockets;
        sprite = Resources.Load<Sprite>("Sprites/Weapons32x32/" + spriteName);
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullets/" + bulletName);
        blankPrefab = Resources.Load<GameObject>("Prefabs/Bullets/Blank");
        foreach (Transform t in sockets)
        {
            spriteRenderers.Add(t.GetComponentInChildren<SpriteRenderer>());
        }
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        particlePrefab = Resources.Load<GameObject>("Prefabs/Effects/Particles");

    }

    public virtual void Activate(Colors color)
    {
        bulletColor = color;
        isActive = true;
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.sprite = sprite;
        }
    }
    public virtual void Deactivate()
    {
        isActive = false;
    }
    public virtual void ChangeColor(Colors color)
    {
        bulletColor = color;
    }
    public virtual bool Shoot(GameObject target, Vector3 aimDir)
    {
        foreach (Transform t in sockets)
        {
            if (target != null)//yohan added
            {
                Vector3 direction = target.transform.position - t.position;
                direction = direction.normalized;
                if (gm.WorldColor == bulletColor)
                {
                    MonoBehaviour.Instantiate(particlePrefab, t.position +
                        (direction * barrelLen), t.rotation);
                    MonoBehaviour.Instantiate(blankPrefab, t.position + (direction * barrelLen),
                        t.rotation);
                }
                else
                {
                    MonoBehaviour.Instantiate(particlePrefab, t.position + (direction * barrelLen), t.rotation);
                    bulletObj = MonoBehaviour.Instantiate(bulletPrefab, t.position + (direction * barrelLen), t.rotation) as GameObject;
                    bullet = bulletObj.GetComponent<Bullet>();
                    bullet.Init(owner, direction, bulletSpeed, DamageCalculation(damage), range, bulletColor);
                    if (doesItHavePoisonBullet == true)
                    {
                        bullet.doesPoisonedBulletIsActivate = true;
                    }
                    if (doesItHaveDoubleShot == true)
                    {
                        GameObject bulletObj2 = MonoBehaviour.Instantiate
                            (bulletPrefab, t.position + (direction * barrelLen)
                            + new Vector3(0.5f, 0.0f, 0.0f), t.rotation) as GameObject;
                        Bullet bullet2 = bulletObj2.GetComponent<Bullet>();
                        bullet2.Init(owner, direction, bulletSpeed, DamageCalculation(damage), range, bulletColor);
                    }
                }
            }

        }
        return true;
    }
    public virtual int DamageCalculation(int damage)
    {//<-- this damage is weapon damage.
        int result = damage;
        //============================================
        // attack stats calculation
        //============================================
        result += owner.GetStats().Attack;
        //-------------------------------------
        // Critical Chance
        //-------------------------------------
        int dice = Random.Range(1, 101);

        if (dice <= owner.GetStats().CriticalChance)
        {
            result *= 2;

        }
        return result;
    }
}


