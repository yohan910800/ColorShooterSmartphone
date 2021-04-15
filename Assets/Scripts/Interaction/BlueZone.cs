using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class BlueZone : Interactable
{
    int basicAttack;
    int i;
    float timer;
    bool triggered;

    GameManager gm;
    Collider2D coll;
    SpriteRenderer sprite;

    Color tmp;
    protected override void Start()
    {
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        gm.WorldColorChange += OnWorldColorChange;

        OnWorldColorChange(gm.WorldColor);
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
    void Update()
    {
        CheckIfShouldBeEnabled();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (sprite.color.g == 1)//Cyan
        {
            if (collision.GetComponent<ICharacter>() != null)
            {
                collision.gameObject.GetComponent<ICharacter>().GetStats().SetSpeed(10.0f);
            }
        }
        else if (sprite.color.g == 0)//blue
        {
            if (collision.GetComponent<ICharacter>() != null)
            {
                collision.gameObject.GetComponent<ICharacter>().GetStats().SetSpeed(1.0f);
            }
        }
        triggered = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ICharacter>() != null)
        {
            collision.gameObject.GetComponent<ICharacter>().GetStats().ResetSpeed();
            triggered = false;
        }
    }

    void OnWorldColorChange(Colors color)
    {
        if (color != Colors.Brown)
        {
            if (gameObject.tag == "FromBeginingInverseColor")
            {
                sprite.color = Blue();
            }
            else
            {
                sprite.color = Cyan();
            }
        }
        else
        {
            if (gameObject.tag == "FromBeginingInverseColor")
            {
                sprite.color = Cyan();
            }
            else
            {
                sprite.color = Blue();
            }
        }
    }

    Color Cyan()
    {
        Color tmp = sprite.color;
        tmp.r = 0.0f;
        tmp.g = 1f;
        tmp.b = 1f;
        tmp.a = 0.5f;
        return tmp;
    }
    Color Blue()
    {
        Color tmp = sprite.color;
        tmp.r = 0.0f;
        tmp.g = 0.0f;
        tmp.b = 1f;
        tmp.a = 0.5f;
        return tmp;
    }
    
    void OnDestroy()
    {
        gm.WorldColorChange -= OnWorldColorChange;
    }
}
