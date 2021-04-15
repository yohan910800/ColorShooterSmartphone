using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MankindGames;
using TMPro;
using UnityEngine.SceneManagement;

public class StateUI : MonoBehaviour
{
    
    Stats stats;
    Image hpBar;

    Color tmp;

    Color tmp2;
    Color tmp3;
    Color tmp4;

    public GameObject meleeAttackButton;
    public Image energyBar;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI creditsTmp;
    public GameObject menuPanel;
    public GameObject joystickUI;

    public GameObject randomSlot;// for bonus
    public GameObject caseWeapBonus;// for bonus
    Image caseWeapBonusImg;
    Image caseWeapBackgroundImg;
    public GameObject randomButton;// for bonus
    public GameObject rndBtnGoldBorder;// for bonus
    public GameObject colorBtn;// for bonus
    public GameObject EnableDebugUiBtn;// for bonus

    Sprite tripleSingleShooterSprite;
    Sprite rifleSprite;
    Sprite shotgunSprite;
    Sprite middleFingerGunSprite;
    Sprite theEraserSprite;
    Sprite laserSurgeryGunSprite;
    Sprite mineSprite;
    Sprite doubleSprite;
    Sprite poisonSprite;



    public Color energyBarColor;
    public Color energyTextColor;

    float timeLeft=0.0f;
    float timerSlot;//for bonus

    int bonusIndex;// for bonus
    //public int tierIndex=0;// for bonus
    int rndNumIndex;
    int min = 1;
    int max = 4;
    List<int> alreadyTier = new List<int>();
    bool justOnceRndNum;
    bool justOncePressTheRndButton;


    bool isFixed;
    bool isItThePlayer;
    bool showingMenu;
    bool activateRandomSlot;//for bonus
    bool justOnceAnim;

    public bool doesPlayerAlreayUseOneLife=false;
    ICharacter character;
    ICharacter player;

    GameManager gm;

    GameObject target;

    //for tutorial 

   
    bool activeTutorialErrorMessage = false;
    float timerTutorialErrorMessage;
    ///

    string[,] bonusNameArray=new string[5,200];//size tmp

    public void Init(ICharacter character, bool isItThePlayer, bool isFixed){

        this.character = character;
        
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        hpBar = transform.Find("HpBar").GetComponent<Image>();

        if (isItThePlayer)
        {
            this.player = character;
            joystickUI = transform.Find("Floating Joystick").gameObject;
            energyBar = transform.Find("EnergyBar").GetComponent<Image>();
            energyText = transform.Find("EnergyBarPercent")
                .GetComponent<TextMeshProUGUI>();

            tmp = energyBar.GetComponent<Image>().color;
            creditsTmp = transform.Find("Credits").GetComponentInChildren<TextMeshProUGUI>();
            meleeAttackButton= transform.Find("MeleeButton").gameObject;
            randomSlot = transform.Find("randomSlot").gameObject;
            caseWeapBonus = transform.Find("CaseBonus").gameObject;
            caseWeapBonusImg = caseWeapBonus.GetComponent<Image>();
            caseWeapBackgroundImg = transform.Find("CaseWeapBackGround").gameObject.GetComponent<Image>();
           rndBtnGoldBorder = transform.Find("InventoryBtn").transform.Find("BorderGold").gameObject;
            rndBtnGoldBorder.SetActive(false);
            colorBtn = transform.Find("color").gameObject;

            UpdateCredits();

            energyBarColor = energyBar.color;
            energyTextColor = energyText.color;

            bonusNameArray[0, 1] = "Super single shoter";
            bonusNameArray[0, 2] = "Rifle";
            bonusNameArray[0, 3] = "Shot gun";

            bonusNameArray[1,1] = "Double shot";
            bonusNameArray[1,2] = "Poison bullets";
            bonusNameArray[1,3] = "Character shoots mines";

            bonusNameArray[2, 1] = "Double shot";
            bonusNameArray[2, 2] = "Poison bullets";
            bonusNameArray[2, 3] = "Character shoots mines";

            bonusNameArray[3, 1] = "The Middle finger gun";
            bonusNameArray[3, 2] = "The eraser";
            bonusNameArray[3, 3] = "Laser surgery gun";

             tripleSingleShooterSprite=Resources.Load<Sprite>("Sprites/CaseUI/CaseTripleSingleShooter64x64");
             rifleSprite = Resources.Load<Sprite>("Sprites/CaseUI/CaseRifle64x64");
             shotgunSprite = Resources.Load<Sprite>("Sprites/CaseUI/CaseShotGun64x64");
             middleFingerGunSprite = Resources.Load<Sprite>("Sprites/CaseUI/CaseMiddleFingerGun64x64");
             theEraserSprite = Resources.Load<Sprite>("Sprites/CaseUI/CaseTheEraser64x64");
             laserSurgeryGunSprite = Resources.Load<Sprite>("Sprites/CaseUI/CaseLaser64x64");
             mineSprite = Resources.Load<Sprite>("Sprites/CaseUI/CaseMine64x64");
             doubleSprite = Resources.Load<Sprite>("Sprites/CaseUI/CaseDoubleShot64x64"); 
             poisonSprite = Resources.Load<Sprite>("Sprites/CaseUI/CasePoison64x64");

        }
        
        stats = character.GetStats();
        target = character.GetGameObject();
        this.isFixed = isFixed;
        this.isItThePlayer = isItThePlayer;
        
        Refresh();
    }
    
    public void Refresh(){
        hpBar.fillAmount = stats.GetNormalizedHP();
        if (isItThePlayer)
        {
            energyBar.fillAmount = stats.GetNormalizedEnergy();
            energyText.text = (stats.GetNormalizedEnergy() * 100).ToString() + "%";
            energyBar.GetComponent<Animator>().SetTrigger("BarEnergyAnim");
        }
        HpBarGetRedOnLowHP();
    }

    public void ChooseRandomWeapon()//ChooseRandomWeapon
    {
        if (justOncePressTheRndButton == false)
        {
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                Player playerRef = player.GetGameObject().GetComponent<Player>();
                if (playerRef
                    .didThePlayerOverComeTheArea6 == false
                    && playerRef.justOnceTutorialRandomBtn == true)
                {
                    activeTutorialErrorMessage = true;
                }

                
                if (playerRef.justOnceTutorialRandomBtn == false)
                {
                    
                    if (player.GetStats().Energy >= 50)
                    {
                        player.GetStats().LoseEnergy(50);//tmp
                        Refresh();
                        activateRandomSlot = true;
                        gm.audioManager.Play("rndSlot1");
                        justOnceRndNum = false;
                        rndNumIndex++;
                        justOncePressTheRndButton = true;
                        randomSlot.SetActive(true);
                        playerRef.justOnceTutorialRandomBtn = true;
                    }
                }
                if (/*player.GetGameObject().GetComponent<Player>()*/playerRef.didThePlayerOverComeTheArea6 == true
                    && playerRef.meleeBtnPressedForTheFirsTime == true)
                {
                    if (player.GetStats().Energy >= 50)
                    {

                        player.GetStats().LoseEnergy(50);//tmp
                        Refresh();
                        activateRandomSlot = true;
                        rndNumIndex++;
                        justOnceRndNum = false;
                        gm.audioManager.Play("rndSlot1");
                        justOncePressTheRndButton = true;
                        randomSlot.SetActive(true);

                    }
                }
                
            }
            else
            {
                player.GetStats().LoseEnergy(50);//tmp
                Refresh();
                activateRandomSlot = true;
                rndNumIndex++;
                justOnceRndNum = false;
                gm.audioManager.Play("rndSlot1");
                justOncePressTheRndButton = true;
                randomSlot.SetActive(true);

            }
            
        }
    }
    
    void DisplayNumberSlot()
    {
        timerSlot += Time.deltaTime;
        Player playerRef = player.GetGameObject().GetComponent<Player>();
        if (timerSlot<=2.0f)
        {
            bonusIndex = Random.Range(1, 4);
            randomSlot.GetComponent<TextMeshProUGUI>().text =
                bonusIndex.ToString();
            gm.audioManager.Play("rndSlotEnd1");
        }

        if (timerSlot <= 4.0f&&timerSlot>=2.0f)
        {
            gm.audioManager.Pause("rndSlot1");
            
            if (justOnceRndNum == false)
            {
                bonusIndex = ChooseRndNumber();
                justOnceRndNum = true;
            }
            randomSlot.GetComponent<Animator>().SetBool("SlotRandom", true);
            randomSlot.GetComponent<TextMeshProUGUI>().text = 
                bonusIndex.ToString()+ "\n"+ bonusNameArray[playerRef.tierIndex,bonusIndex];
            CheckWichCaseShouldBeDisplayed();
            caseWeapBonus.SetActive(true);
            caseWeapBackgroundImg.gameObject.SetActive(true);
            
            
        }
        //after finish to tier
        if (timerSlot >= 5.0f)
        {
            player.GetGameObject().GetComponent<PlayerCombatV1>().
                TierRandomNumber(playerRef.tierIndex,bonusIndex);
            //reset the button    
            playerRef.tierIndex++;
            timerSlot = 0.0f;
            activateRandomSlot = false;
            caseWeapBonus.SetActive(false);
            caseWeapBackgroundImg.gameObject.SetActive(false);
            justOncePressTheRndButton = false;
        }
    }

    int ChooseRndNumber()
    {
        
        int theTired = Random.Range(min, max);
        while (alreadyTier.Contains(theTired))
            theTired = Random.Range(min, max);

        alreadyTier.Add(theTired);
        if (rndNumIndex == 3)
        {
            alreadyTier.Clear();
            rndNumIndex = 0;
        }
        return theTired;
    }

    void CheckWichCaseShouldBeDisplayed()
    {
        Player playerRef = player.GetGameObject().GetComponent<Player>();
        switch (playerRef.tierIndex)
        {
            case 0:
                switch (bonusIndex)
                {
                    case 1:
                        caseWeapBonusImg.sprite = tripleSingleShooterSprite;
                        caseWeapBackgroundImg.color = Color.yellow;
                        break;
                    case 2:
                        caseWeapBonusImg.sprite = rifleSprite;
                        caseWeapBackgroundImg.color = Color.yellow;
                        break;
                    case 3:
                        caseWeapBonusImg.sprite = shotgunSprite;
                        caseWeapBackgroundImg.color = Color.yellow;
                        break;
                }

                break;
            case 1:
                CheckWichBonusCaseShouldBeDisplayed();
                break;
            case 2:
                CheckWichBonusCaseShouldBeDisplayed();
                break;
            case 3:
                switch (bonusIndex)
                {
                    case 1:
                        caseWeapBonusImg.sprite = middleFingerGunSprite;
                        caseWeapBackgroundImg.color = Color.red;
                        Log.log("display MiddleFingerGun");
                        break;
                    case 2:
                        caseWeapBonusImg.sprite = theEraserSprite;
                        caseWeapBackgroundImg.color = Color.red;
                        Log.log("display TheEraser");
                        break;
                    case 3:
                        caseWeapBonusImg.sprite = laserSurgeryGunSprite;
                        caseWeapBackgroundImg.color = Color.red;
                        Log.log("display LaserSurgeryGun");
                        break;
                }
                break;
            case 4:
                break;

        }

    }
    void CheckWichBonusCaseShouldBeDisplayed()
    {
        switch (bonusIndex)
        {
            case 1:
                caseWeapBonusImg.sprite = doubleSprite;
                caseWeapBackgroundImg.color = Color.green;
                Log.log("Double Shot");

                break;
            case 2:
                caseWeapBonusImg.sprite = poisonSprite;
                caseWeapBackgroundImg.color = Color.green;
                Log.log("Poisoned bullet");
                break;
            case 3:
                caseWeapBonusImg.sprite = mineSprite;
                caseWeapBackgroundImg.color = Color.green;
                Log.log("Mine");
                break;
        }
    }



    void Update() {
        
        if (isItThePlayer)
        {
            Player playerRef = player.GetGameObject().GetComponent<Player>();

            UpdateRandomBtnColor();
            if (activateRandomSlot==true)
            {
                DisplayNumberSlot();
            }
            else
            {
                randomSlot.SetActive(false);
            }

            UpdateTuotorialErrorMessage();
            UpdateMeleeAttackBtnDisplay();
            DisableJoystickIfMeleeMode();//yohan added
            ReturnMeleeAttackBtnOnBasicColor();//yohan added
        }

        if (!isFixed) Move();
    }
    void UpdateTuotorialErrorMessage()
    {
        if (activeTutorialErrorMessage == true)
        {
            timerTutorialErrorMessage += Time.deltaTime;
            if (timerTutorialErrorMessage >= 2)
            {
                GameObject.Find("TutorialUI").
                                transform.Find("RndBtnErrorMessageTXT").
                                gameObject.SetActive(false);
                activeTutorialErrorMessage = false;
            }
            else
            {
                GameObject.Find("TutorialUI").
                transform.Find("RndBtnErrorMessageTXT").gameObject.SetActive(true);
            }

        }
        else
        {
            timerTutorialErrorMessage = 0.0f;
        }
    }
    void UpdateRandomBtnColor()
    {
        if (player.GetStats().Energy >= 50)
        {
            rndBtnGoldBorder.SetActive(true);
            randomButton.GetComponent<Animator>().SetBool("EnergyHigherThan50", true);
            randomButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            rndBtnGoldBorder.SetActive(false);
            randomButton.GetComponent<Animator>().SetBool("EnergyHigherThan50", false);
            randomButton.GetComponent<Image>().color = Color.grey;
        }
    }
    void UpdateMeleeAttackBtnDisplay()
    {
        if (player.GetStats().Energy >= 100.0f)
        {
            meleeAttackButton.SetActive(true);
            DisplayMeleeAttackButton();
        }
        else
        {
            meleeAttackButton.SetActive(false);
        }
    }

    void DisplayMeleeAttackButton()
    {
        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            meleeAttackButton.SetActive(true);
        }
        else
        {
            if (player.GetGameObject().GetComponent<Player>().didThePlayerOverComeTheArea6==true)
            {
                meleeAttackButton.SetActive(true);
            }
            else
            {
                meleeAttackButton.SetActive(false);
            }
        }
    }

    void ReturnMeleeAttackBtnOnBasicColor()
    {
            if (character.GetStats().Energy == 0)
            {
                energyBar.GetComponent<Image>().color = tmp;
                energyText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            }
    }
    void DisableJoystickIfMeleeMode()
    {
        if (character.isInvincible == true)
        {
            joystickUI.GetComponent<Image>().enabled = false;
            joystickUI.GetComponent<FloatingJoystick>().enabled = false;
            joystickUI.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
        else
        {
            joystickUI.GetComponent<Image>().enabled = true;
            joystickUI.GetComponent<FloatingJoystick>().enabled = true;
            joystickUI.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
    }

    void Move(){
        if(target == null) return;
        transform.position = target.transform.position + Vector3.up;
    }

    public void ActivateMeleeMode()
    {
        Time.timeScale = 1;
        player.GetGameObject().GetComponent<Player>().meleeBtnPressedForTheFirsTime = true;
        target.GetComponent<PlayerCombatV1>().ActiveMeleeMode();
    }

    public void UpdateCredits()
    {
        creditsTmp.SetText(player.GetInventory().Credits.ToString());
        creditsTmp.GetComponent<Animator>().SetTrigger("ActiveGetCoinAnim");
    }

    public void ToggleMenu()
    {
        showingMenu = !showingMenu;
        menuPanel.SetActive(showingMenu);
        meleeAttackButton.SetActive(!showingMenu);
        //colorBtn.SetActive(!showingMenu);

        Static.Pause(showingMenu);
    }
    public void ToggleDebugUIMenu()
    {
        showingMenu = !showingMenu;
        EnableDebugUiBtn.SetActive(showingMenu);

    }


    public void Replay()
    {
        
        player = GameObject.Find("Player").GetComponent<Player>();
        GameObject.Find("Player").GetComponent<PlayerCombatV1>().enabled = true;
        GameObject.Find("Player").GetComponent<MovementV1>().enabled = true;
        GameObject.Find("Player").GetComponent<BoxCollider2D>().enabled = true;
        if (GameObject.FindGameObjectWithTag("Boss") != null|| GameObject.FindGameObjectWithTag("FourHandsBoss") != null)
        {
            GameObject.Find(/*"GameManager"*/"AudioManager").GetComponent<AudioManager>().Play("BossBgm");
        }
        else
        {
            GameObject.Find(/*"GameManager"*/"AudioManager").GetComponent<AudioManager>().Play("MainBgm");
        }

        player.deadEffectObj.SetActive(false);
        player.deadEffectAnimObj.SetActive(false);

        foreach (GameObject partOfBody in player.playerMainBody)
        {
            partOfBody.SetActive(true);
        }
        player.playerDeadBody.SetActive(false);

        if (player.GetInventory().Life >= 1)
        {
            Time.timeScale = 1;
            //Application.LoadLevel(Application.loadedLevel);// tmp
            player.GetHeal(100);
            //Refresh();
            player.activeInvisible = true;
            player.GetInventory().RemoveLife(1);
            Destroy(GameObject.Find("GameOverPanel(Clone)"));
            Destroy(GameObject.Find("NoLifeGameOverPanel(Clone)"));
        }
        
    }
    

    public void GoBackToHub()
    {
        Time.timeScale = 1;
        //need save system
        SceneManager.LoadScene("Hub");
    }
    public void GoToNextStage()
    {
        Time.timeScale = 1;
        //need save system
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EnabledGameOverPanel()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/StateUIs/GameOverPanel"), 
            Vector3.zero, Quaternion.identity);
    }
    public void EnabledDemoEndPanel()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/StateUIs/DemoEndPanel"),
            Vector3.zero, Quaternion.identity);
    }
    public void EnableWatchAdvertissementPanel()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/StateUIs/NoLifeGameOverPanel"),
            Vector3.zero, Quaternion.identity);
    }

    public void ChangeTheAlphaOfMeleeAttackButton()//yohan added
    {
        tmp2 = Color.blue;
        energyBar.GetComponent<Image>().color = tmp2;
        energyText.GetComponent<TextMeshProUGUI>().fontStyle=FontStyles.Bold;
        energyText.GetComponent<TextMeshProUGUI>().color=Color.red;
    }

     void HpBarGetRedOnLowHP()
    {
        if (hpBar != null)
        {
            if (stats.HP <= 30)
            {
                hpBar.color = Color.red;
            }
            else
            {
                if (gameObject.name != "HpBoss(Clone)")
                {
                    hpBar.color = Color.green;
                }
                else
                {
                    return;
                }
            }
        }
    }

    public void ColorChangeButton()
    {

        gm.NextColor(1);
    }

    public void OnDeath()
    {
        if (!isFixed) Destroy(gameObject);
    }


    
}
