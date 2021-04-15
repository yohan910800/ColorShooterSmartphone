using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using MankindGames;

public class ShopInteraction: Interactable {

    public bool hasActiveArea = true;
    public GameObject target;

    GameManager gameManager;

    public GameObject Player;

    Player PlayerScript;

    int Credit;
    public TextMeshProUGUI creditTxt;
    GameObject StateUI;

    public GameObject ShopUI;
    InventoryUI inventoryUI;
    Inventory inventory;

    bool isMoving;
    bool inArea;
    IInputModule input;

    Dictionary<string, int> items = new Dictionary<string, int>();
[Header("ShopType- 0=WeaponShop 1=Heal")]
    public int ShopType = 0;
    void Update() {

        if (PlayerScript.inventoryUI != null && PlayerScript.inventoryUI.isactive) {
            ShopUI.SetActive(false);
        }
       // Credit = inventory.Credits;
        UpdateCredit();

    }
    protected override void Init() {
        if (player.GetInputModule() != null) 
            OnInputModuleChange();
        player.OnInputModuleChange += OnInputModuleChange;
        if (target == null) 
            target = gameObject;
        
        PlayerScript = Player.GetComponent<Player>();
        
        creditTxt = ShopUI
            .transform
            .Find("Credits")
            .GetComponentInChildren<TextMeshProUGUI>();
if(ShopType==1)
        items.Add("hpPotion", 10);
    else if (ShopType == 0)
        {
            items.Add("Rifle", 10);
            items.Add("Shotgun", 10);
            items.Add("TheMiddleFingerGun", 10);
            items.Add("RedColor", 10);
        }

        Credit = 0;
    }
    public void InitShopInventory(Inventory inv) {
        this.inventory = inv;
    }

    void OnInputModuleChange() {
        if (input != null) 
            input.OnTap -= OnTap;
        input = player.GetInputModule();
        input.OnTap += OnTap;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            inArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            inArea = false;
            ShopUI.SetActive(false);
        }
    }

    void OnTap(GameObject gObj) {
        isMoving = input.GetDirection() != Vector2.zero;
        inArea = hasActiveArea
            ? inArea
            : true;
        if (inArea && !isMoving && gObj == target) {
            OnTriggered();
            ShopUI.SetActive(true);
            UpdateCredit();
        }
    }

    void OnDestroy() {
        input.OnTap -= OnTap;
    }

    public void UpdateCredit() {
        Credit = PlayerScript
            .GetInventory()
            .Credits;
        creditTxt.SetText(Credit.ToString());

    }
    public void OnPurchaseClick(string itemName) {
        int price = items[itemName];
        if (PlayerScript.GetInventory().Credits < price) {
            Log.log("Not Enough Credit");
            return;
        }

        if (itemName == "hpPotion") {
            PlayerScript.GetHeal(50);
            PlayerScript
                .RemoveCredits(price);
            return;
        }

        if (itemName == "RedColor") {

            PlayerScript.AddColor(Colors.Red);
            PlayerScript
                .RemoveCredits(price);
                ShopUI.transform.Find("Panel").gameObject.transform.Find(itemName).gameObject.transform.Find("SoldOut").gameObject.SetActive(true);
            return;
        };

        // weapon instantiation part
        Weapon weap = Activator.CreateInstance(Type.GetType(itemName))as Weapon;
        bool isSuccess = PlayerScript.AddWeapon(weap, 1); // second argument is 2 if dualwielding
        if (isSuccess) {
            PlayerScript
                .RemoveCredits(price);
            ShopUI.transform.Find("Panel").gameObject.transform.Find(itemName).gameObject.transform.Find("SoldOut").gameObject.SetActive(true);
        }
    }

}
