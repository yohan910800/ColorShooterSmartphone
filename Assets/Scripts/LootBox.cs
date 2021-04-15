using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MankindGames;
using TMPro;

public class LootBox: MonoBehaviour {

    /*note
        ◆lootbot item list
        100% coins (50/100/200)-temp

        ▲relates class = stats
        ●Attack+1
        ●HP
        ●Life+1

        ▲related class = playerCombat
        ●Delete Enemy on screen
        ●Pet

        ▲Others
        ●
        ●



        ◆Other classes that's being used
        ▲SaveSystem
    */

    //Variables

    int[] creditList;
    Inventory inventory;
    string LootName;
    public int credit;

    //For animation
    Sprite BoxSprite_opened;
    Sprite Boxsprite_closed;
    Image Boxsprite;
   RectTransform BoxSpriteTr;
   public GameObject LootText;
   public GameObject LootPanel;
   public GameObject gotItBtn;

    

    void Awake() {
        Init();
    }

    private void Init() {
        LoadResource();
        inventory = new Inventory(); //temp(Need save/load stats,inventory)
        //Set prefab's rect transform to match it's parent(Canvas)----------
        RectTransform canvasRecT=GameObject.Find("Canvas").GetComponent<RectTransform>();
        Vector2 parentRTsize = canvasRecT.sizeDelta;
        Vector3 canvaspos= canvasRecT.position;
        gameObject.GetComponent<RectTransform>().position=canvaspos;
        gameObject.GetComponent<RectTransform>().sizeDelta = parentRTsize;
        // -------------------------------------------------------------------

        //
        Boxsprite= transform.Find("BoxSprite").GetComponent<Image>();
        BoxSpriteTr= transform.Find("BoxSprite").GetComponent<RectTransform>();
 
        //


        creditList = new int[3]{
            50,
            100,
            200
        }; //temp
    }
    private void LoadResource(){

        Boxsprite_closed=Resources.Load<Sprite>("Sprites/LootBox/Treasure Chest closed 254x254");
        BoxSprite_opened=Resources.Load<Sprite>("Sprites/LootBox/Treasure Chest open 254x254");
    }
    public void onOpenButtonPress() {

        OpenLoot(RollDice());
        StartCoroutine(LootAnimation());
    }

   private string RollDice() {
        
        int dice = Random.Range(0, 100);
        if (dice < 20) 
            LootName = "Att+1";
        else if (dice < 40) 
            LootName = "HP+1";
        else if (dice < 60) 
            LootName = "Life+1";
        else if (dice < 80) 
            LootName = "Pet";
        else 
            LootName = "DeleteEnemy";
        
        return LootName;

    }

    private void OpenLoot(string LootName) {

        //addCredit---------------------------------------
        int dice = Random.Range(0, creditList.Length);
       
        credit=creditList[dice];
         inventory.AddCredits(credit);
        Debug.Log(creditList[dice] + "Credit added");
        // ------------------------------------------------

        switch (LootName) {

            case "Att+1"://implement 
                break;

            case "HP+1":
                break;

            case "Life+1":
                break;

            case "Pet":
                break;

            case "DeleteEnemy":
                break;

            default:
                Debug.Log("Encorrect LootName");
                break;

        }

        Debug.Log(LootName + "\n Credit=" + inventory.Credits);
    }


    

public IEnumerator LootAnimation(){
        Boxsprite.sprite=Boxsprite_closed;
        LootPanel.SetActive(false);
        float time = 0.0f;
        float magnitude=2.0f;
        Vector3 currentpos=BoxSpriteTr.localPosition;
        while (time <3f)
        {   
          
            magnitude+=Time.deltaTime*5;
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            BoxSpriteTr.localPosition = currentpos + new Vector3(x, y, 0);
            time += Time.deltaTime;
            yield return null;
        }
        Debug.Log(time);
        BoxSpriteTr.localPosition = currentpos;
        Boxsprite.sprite=BoxSprite_opened;

        LootPanel.SetActive(true);
        LootPanel.transform.localScale=Vector3.zero;
        LootText.GetComponent<TextMeshProUGUI>().text=LootName+"\n"+credit+"Coin";
        
       while (time<3.25f){
           BoxSpriteTr.localScale+=new Vector3(0.05f,0.05f,0);
           LootPanel.transform.localScale+=new Vector3(0.15f,0.15f,0);
           time += Time.deltaTime;
            yield return null;
       }
       while(time<3.5f){
            BoxSpriteTr.localScale-=new Vector3(0.05f,0.05f,0);
            gotItBtn.SetActive(true);
            time += Time.deltaTime;
            yield return null;
       }
    }
}
