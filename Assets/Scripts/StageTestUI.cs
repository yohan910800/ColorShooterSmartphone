using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System.Linq;
using UnityEngine.UI;


public class StageTestUI : MonoBehaviour
{
    
    GameObject PlayerObj;
    Transform MapManagerObj;
    GameObject Camera;
    GameObject GameManager;
    GameObject Essentail;

    Player player;
    MapManager mapManager;
    Camera camera;

    GameObject triggerzone;

    Transform SpawnArea;
    List<Transform> SpawnAreaList;

   public Dropdown dropdown;
   public InputField HPInput;
   public InputField AttInput;
   Stats playerStats;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        PlayerObj= GameObject.Find("Player");
        player = PlayerObj.GetComponent<Player>();
        Essentail = GameObject.Find("Essential");
        MapManagerObj = Essentail.transform.Find("MapManager");
        mapManager = MapManagerObj.gameObject.GetComponent<MapManager>();
        Camera = GameObject.Find("MainCamera");
        camera = Camera.GetComponent<Camera>();
        triggerzone= GameObject.Find("PhaseControllerTrigger");
        playerStats=player.GetStats();

        //testDebug();

    }




   public void testDebug()
    {
        Debug.Log(PlayerObj);
        Debug.Log(player);
        Debug.Log(mapManager);

        Debug.Log(triggerzone);

        Debug.Log(SpawnArea);

        Debug.Log(player.GetStats());
    }

    public void setAtt(){
         int value=int.Parse(AttInput.text);
        player.GetStats().SetAttack(value);
    }
    public void setHP(){
        int value=int.Parse(HPInput.text);
        player.GetStats().SetHP(value);
         player.GetStats().SetMaxHP(value);
         player.GetHeal(0);

         Debug.Log("Player hp set to"+player.GetStats().maxHP+"currnetHP="+player.GetStats().HP);
    }

    public void activePhaseTest(){
        Debug.Log(mapManager);
        int dropdownvalue = dropdown.value;
        mapManager.phase = dropdownvalue;
        foreach (ICharacter t in mapManager.enemyArray)
        {
            t.Kill();
        }
      
        mapManager.MoveTriggerZoneToTheNextPosition(dropdownvalue-1);
        PlayerObj.transform.position = new Vector3(mapManager.triggerZone.transform.position.x, mapManager.triggerZone.transform.position.y - 5, PlayerObj.transform.position.z);

        camera.transform.position = new Vector3(PlayerObj.transform.position.x, PlayerObj.transform.position.y, -10);
    }


 

}
