using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MankindGames;
using System.Linq;

public class InventoryUI : MonoBehaviour {

    Inventory inventory;
    Player player;

    GameObject weaponsPanel;
    GameObject colorsPanel;
    GameObject container;
    GameObject closePanel;

    //TextMeshProUGUI creditsTmp;

    public GameObject buttonPrefab;

    float buttonSize = 100;
    float separation = 5;
    public bool isactive;

    List<Weapon> weapons = new List<Weapon>();
    List<Colors> colors = new List<Colors>();

    public void Init(Player player, Inventory inventory){
        this.inventory = inventory;
        this.player = player;
        container = transform.Find("Container").gameObject;
        weaponsPanel = container.transform.Find("WeaponsPanel").gameObject;
        colorsPanel = container.transform.Find("ColorsPanel").gameObject;
        //creditsTmp = container.transform.Find("Credits").GetComponentInChildren<TextMeshProUGUI>();
        closePanel = transform.Find("ClosePanel").gameObject;
        weapons = inventory.Weapons;
        colors = inventory.BulletColors;

        int i = 0;
        foreach (Weapon w in inventory.Weapons){
            AddWeapon(i,w);
            i++;
        }

        i = 0;
        foreach (Colors c in inventory.BulletColors){
            AddColor(i,c);
            i++;
        }

        //UpdateCredits();
        CloseInventory();
    }

    void UpdateItems(){
        if(weapons != inventory.Weapons){
            List<Weapon> difference = inventory.Weapons.Where(w => !weapons.Contains(w)).ToList();
            int i = weapons.Count;
            foreach (Weapon w in difference){
                AddWeapon(i,w);
                i++;
            }
        }
        if(colors != inventory.BulletColors){
            List<Colors> difference = inventory.BulletColors.Where(c => !colors.Contains(c)).ToList();
            int i = colors.Count;
            foreach (Colors c in difference){
                AddColor(i,c);
                i++;
            }
        }
    }

    void AddWeapon(int i, Weapon w){
        GameObject btnObj = Instantiate(buttonPrefab,weaponsPanel.transform) as GameObject;
        Button btn = btnObj.GetComponent<Button>();
        Image icon = btnObj.transform.Find("Icon").GetComponentInChildren<Image>();
        icon.sprite = w.sprite;
        btn.onClick.AddListener(()=>{player.ActivateWeapon(w);});
        btn.onClick.AddListener(()=>{CloseInventory();});//yohan added
        btn.onClick.AddListener(()=>{DisplayBtnAfterClose();});//yohan added
        RectTransform rt = btnObj.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2((-buttonSize * (0.5f + i) - separation * (1 + i))+600, -600);
    }

    void AddColor(int i, Colors c){
        if(!Static.bulletColors.ContainsKey(c)){
            Log.log("This bullet color does not exist "+c.ToString());
            return;
        }
        GameObject btnObj = Instantiate(buttonPrefab,colorsPanel.transform) as GameObject;
        Button btn = btnObj.GetComponent<Button>();
        Image icon = btnObj.transform.Find("Icon").GetComponentInChildren<Image>();
        icon.color = Static.bulletColors[c];
        btn.onClick.AddListener(()=>{player.SetBulletColor(c);});
        btn.onClick.AddListener(() => { CloseInventory(); });//yohan added
        btn.onClick.AddListener(() => { DisplayBtnAfterClose(); });//yohan added
        RectTransform rt = btnObj.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2((-buttonSize * (0.5f + i) - separation * (1 + i)-300), -500);
        //Log.log("pos inventory " + rt.anchoredPosition);
    }

    void Start(){
    }

    //void UpdateCredits(){
    //    creditsTmp.SetText(inventory.Credits.ToString());
    //}

    void Update(){
        
    }

    void HideAll(){

    }

    public void OpenInventory(){
        UpdateItems();
        //UpdateCredits();
        container.SetActive(true);
        closePanel.SetActive(true);
        //Static.Pause(true);
    }

    public void CloseInventory(){
        container.SetActive(false);
        closePanel.SetActive(false);
        Static.Pause(false);

    }
    //yohan addeds
    // should be called on the CloseInventory() but doesn't work when the game start
    public void DisplayBtnAfterClose()
    {
        isactive = false;

        GameObject playerUI = GameObject.Find("PlayerStateUI(Clone)");
        playerUI.transform.Find("InventoryBtn").gameObject.SetActive(true);
    }

}
