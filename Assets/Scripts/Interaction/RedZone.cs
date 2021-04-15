using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class RedZone : Interactable
{
    int basicAttack;
    int i;
    int j;
    float timer;
    float timer2;
    bool triggered;
    bool playerGetDmg;
    GameManager gm;
    Collider2D coll;
    SpriteRenderer sprite;
    List<ICharacter> triggeredChara=new List<ICharacter>();

    protected override void Start()
    {
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        gm.WorldColorChange += OnWorldColorChange;
       
        OnWorldColorChange(gm.WorldColor);
        sprite.color = Color.red;
    }

    void Update()
    {
        DisableAndEnableTriggerOnChangeColor();
        CheckIfShouldBeEnabled();
    }
    void CheckIfShouldBeEnabled()
    {
        if (Vector2.Distance(transform.position, gm.player.transform.position) < 50)
        {
            sprite.enabled = true;
            coll.enabled = true;
        }
        else
        {
            sprite.enabled = false;
            coll.enabled = false;
        }
    }
    void DisableAndEnableTriggerOnChangeColor()
    {
        timer2 += Time.deltaTime;
        GetComponent<BoxCollider2D>().enabled = true;

        if (timer2 >= 1.0f)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            timer2 = 0.0f;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (sprite.color == Red())
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                if (collision.name != "Player"&& collision.GetComponent<ICharacter>() != null)
                {
                    collision.GetComponent<ICharacter>().GetGameObject().
                        GetComponent<Ranged1>().GetHit(5);
                }
                else
                {
                    if (collision.GetComponent<ICharacter>() != null)
                    {
                        collision.GetComponent<ICharacter>().GetGameObject().
                            GetComponent<Player>().GetHit(5);
                    }
                }
                timer = 0;
            }
        }
        else if (sprite.color == Grey())
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                if (collision.name != "Player" && collision.GetComponent<ICharacter>() != null)
                {
                    collision.GetComponent<ICharacter>().GetGameObject().
                        GetComponent<Ranged1>().GetHeal(5);
                }
                else
                {
                    if (collision.GetComponent<ICharacter>() != null)
                    {
                        collision.GetComponent<ICharacter>().GetGameObject().
                            GetComponent<Player>().GetHeal(5);
                    }
                }

                timer = 0;
            }
        }
    }
   
    void OnWorldColorChange(Colors color)
    {
        if (color != Colors.Brown)
        {
            if (gameObject.tag == "FromBeginingInverseColor")
            {
                sprite.color = Red();
            }
            else
            {
                sprite.color = Grey();
            }
        }
        else
        {
            if (gameObject.tag == "FromBeginingInverseColor")
            {
                sprite.color = Grey();
            }
            else
            {
                sprite.color = Red();
            }
        }
    }

    Color Red()
    {
        Color tmp = sprite.color;

        tmp.r = 1.0f;
        tmp.g = 0.25f;

        tmp.b = 0.0f;

        tmp.a = 0.5f;

        return tmp;
    }
    Color Grey()
    {
        Color tmp = sprite.color;

        tmp.r = 0.80f;
        tmp.g = 0.80f;

        tmp.b = 0.80f;

        tmp.a = 0.5f;
        return tmp;
    }

    void OnDestroy()
    {
        gm.WorldColorChange -= OnWorldColorChange;
    }
}
