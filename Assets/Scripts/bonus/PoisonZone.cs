using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
public class PoisonZone : Interactable
{

    protected override  void Start()
    {
        Destroy(gameObject, 4.0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Tutorial") == true)
        {
            //Log.log("enter ");
            other.GetComponent<ICharacter>().isPoisoned = true;
        }
    }

}
