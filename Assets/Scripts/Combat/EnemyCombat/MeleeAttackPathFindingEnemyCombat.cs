using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
public class MeleeAttackPathFindingEnemyCombat : EnemyCombatV1
{
    public Colors[] colors;
    public int[] ratios;
    Animator animator;
    GameObject hand2;
    GameObject hand2MeleeAttack;

    public override void Init(ICharacter character)
    {
        base.Init(character);
        foreach (Colors c in colors)
        {
            this.character.AddColor(c);
        }
        character.SetBulletColor(colors[0]);
        animator = GetComponent<Animator>();
        hand2 = gameObject.transform.Find("Hand2").gameObject;
        hand2MeleeAttack = gameObject.transform.Find("Hand2MeleeMode").gameObject;
    }
    public override void Update()
    {
        if (aimDir.x >= 2.2 && aimDir.x <= 3)
        {
            //right
            animator.SetFloat("moveX",1);
            animator.SetFloat("moveY", 0);
        }
        else if (aimDir.y>=2.2f&&aimDir.y <= 3)
        {
            //up
            animator.SetFloat("moveY", 1);
            animator.SetFloat("moveX", 0);
        }
        else if (aimDir.y >= -2.2f && aimDir.y <= 0) 
        {
            //left
            animator.SetFloat("moveX", -1);
            animator.SetFloat("moveY", 0);
        }
        else if(aimDir.x >= -2.2f && aimDir.x <= 0)
        {
            //down
            animator.SetFloat("moveY", -1);
            animator.SetFloat("moveX", 0);
        }



        //Log.log("dist "+dist);
        
        if (target == null) return;

        if (character.ActivateAutoAim == true)
        {
            Aim();
            if (gameObject.tag != "NotAimable")
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
                if (target != null)
                {
                    float dist = Vector3.Distance(target.transform.position, transform.position);

                    if (dist < 3f)
                    {
                        charge += Time.deltaTime * weapon.FireRate;

                        if (charge >= fullCharge)
                        {
                            hand2.SetActive(true);
                            hand2MeleeAttack.SetActive(false);
                            animator.ResetTrigger("ActiveEnemyMeleeAttack");
                            if (Shoot())
                            {

                                hand2.SetActive(false);
                                hand2MeleeAttack.SetActive(true);
                                animator.SetTrigger("ActiveEnemyMeleeAttack");
                                charge = 0;

                            }

                        }
                    }
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<BoxCollider2D>());
        }
    }
}
