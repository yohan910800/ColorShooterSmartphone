using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class Character : MonoBehaviour, ICharacter {
    // Events
    public event Action OnInputModuleChange;
    public event Action<Weapon> OnWeaponChange;
    public event Action<ICharacter> OnDeathEvent;

    // Public fields
    [HideInInspector]
    public StateUI stateUI;
    //public StateUI stateUIFixe;
    public GameObject stateUIPrefab;
    //public GameObject stateUIPrefabFixe;

    // Serialized fields for the character stats
    [SerializeField] int initHP = 0;
    [SerializeField] int initAtt = 0;
    [SerializeField] float initSpeed = 0;
    [SerializeField] float initAttSpeed = 0;

    [SerializeField] int initDefence = 0;
    [SerializeField] int initCriticalChance = 0;
    [SerializeField] int creditDrop = 10;
    [SerializeField] int energyDrop = 10;

    [SerializeField] protected Colors color = Colors.Blue;

    // Protected fields
    protected ICombat combat;
    protected GameManager gm;
    protected IInputModule input;
    protected IMovement movement;
    protected Stats stats;
    protected Weapon activeWeapon;
    protected Weapon activeWeapon2;
    
    protected Colors bulletColor;
    protected GameObject damageTextPrefab;
    protected GameObject dialogTextPrefab;
    protected Inventory inventory;
    protected Drops drops;
    protected AudioManager audioManager;
    protected AnimationManager animationManager;

    public bool activeInvisible { get; set; }
    ////-----------------------------

    // die anim
    public GameObject deadEffectObj { get; set; }
    public GameObject deadEffectAnimObj { get; set; }
    public GameObject playerDeadBody { get; set; }
    public GameObject[] playerMainBody { get; set; }
    //
    public bool IsAlive {get; set;}
    public bool ActivateAutoAim {get; set;}
    public int phase { get; set; }//for boss probably tmp
    public bool isItAllowToAttack { get; set; }//for boss probably tmp
    public bool isInvincible { get; set; }//yohan added

    public int activeWeaponIndex{ get; set; }//yohan added
    public Weapon[] equipedWeapons { get; set; }//yohan added

//bonus 
public bool isPoisoned { get; set; }//yohan added


    public virtual void Init(){
        // Initialization

        if (gameObject.name == "Player")
        {
            //stats = SaveSystem.LoadStats();
            //stats.maxHP = SaveSystem.LoadStats().maxHP;
            stats = new Stats(initHP, initAtt, initSpeed, initAttSpeed, initDefence, initCriticalChance);


        }
        else
        {
            stats = new Stats(initHP, initAtt, initSpeed, initAttSpeed, initDefence, initCriticalChance);
        }
        InitInput();
        InitInventory();
        movement = GetComponent<IMovement>();
        combat = GetComponent<ICombat>();
        movement.Init(this);
        combat.Init(this);
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        damageTextPrefab = Resources.Load<GameObject>("Prefabs/DamageText");
        dialogTextPrefab = Resources.Load<GameObject>("Prefabs/StateUIs/FloatingDialog");
        GameObject stateUIInstance = Instantiate(stateUIPrefab,Vector3.zero,Quaternion.identity) as GameObject;
        //GameObject stateUIInstanceFixe = Instantiate(stateUIPrefabFixe,Vector3.zero,Quaternion.identity) as GameObject;
        stateUI = stateUIInstance.GetComponentInChildren<StateUI>();
        //stateUIFixe = stateUIInstanceFixe.GetComponentInChildren<StateUI>();
        InitStateUI();
        
        animationManager = GetComponent<AnimationManager>();
        if(animationManager != null) animationManager.Init();
        IsAlive = true;
        ActivateAutoAim = true;
        drops = new Drops(creditDrop,energyDrop);
        gm.WorldColorChange+=OnWorldColorChange;
        audioManager = FindObjectOfType<AudioManager>();
    }

    protected virtual void InitInput(){
        ChangeInputModule(GetComponent<IInputModule>());
        input.Init();
    }


    protected virtual void InitStateUI(){
        stateUI.Init(this,false,false);
        //stateUIFixe.Init(this,true,true);
    }

    protected virtual void InitInventory(){
        inventory = new Inventory(0, null, null, 0);
    }

    protected virtual void Update(){}

    public virtual void GetHeal(int val){
        stats.Heal(val);
        stateUI.Refresh();
        if(gm.showDamageText){
            var gObj = Instantiate(damageTextPrefab,transform.position,Quaternion.identity) as GameObject;
            DamageText dt = gObj.GetComponent<DamageText>();
            dt.Init(val, Color.green);
            
        }
        audioManager.Play("Heal1");
    }

    public  virtual IInputModule GetInputModule(){
        return input;
    }

    public GameObject GetGameObject(){
        return gameObject;
    }

    public Stats GetStats(){
        return stats;
    }
    public Stats SetStats(Stats gotStats)
    {
        stats = gotStats;
        return stats;
    }

    public Weapon GetActiveWeapon(){
        return activeWeapon;
    }
    public Weapon GetActiveWeapon2()
    {
        return activeWeapon2;
    }
   
    //yohan added
    public Inventory GetInventory()
    {
        return inventory;
    }
    //yohan added
    public Colors GetBulletColor()
    {
        return bulletColor;
    }
    public StateUI GetStateUI()
    {
        return stateUI;
    }

    public void ChangeInputModule (IInputModule input){
        if(this.input != null) this.input.OnColorSwitch -= BroadcastColorSwitch;
        this.input = input;
        this.input.OnColorSwitch += BroadcastColorSwitch;
        // Let all the modules know we had an input module change
        if(OnInputModuleChange != null) OnInputModuleChange();
    }

    public void AddWeapon(Weapon weapon){
        inventory.AddWeapon(weapon);
    }

    public void RemoveWeapon(Weapon weapon){
        if(activeWeapon == weapon){
            Log.log("Can't remove active weapon");
        }else if(!inventory.RemoveWeapon(weapon)){
            Log.log("Can't remove a weapon this character doesn't own");
        }
    }

    public void ActivateWeapon(Weapon weapon){
        if(inventory.Weapons.Contains(weapon)){
            if(activeWeapon!=null) activeWeapon.Deactivate();
            activeWeapon = weapon;
            activeWeapon.Activate(bulletColor);
            if(OnWeaponChange!=null) OnWeaponChange(activeWeapon);
        }else{
            Log.log(gameObject.name+" doesn't own this weapon");
        }
    }

    public void ActivateAllWeapons(Weapon weapon)
    {
        
        if (inventory.Weapons.Contains(weapon))
        {
            activeWeapon = weapon;
            activeWeapon.Activate(bulletColor);
            //GetGameObject().GetComponent<AnimationManager>().hand1.spriteRenderer.color = Color.white;
        }
        else
        {
            Log.log(gameObject.name + " doesn't own this weapon");
        }
    }

    public void AddColor(Colors color){
        if(Static.bulletColors.ContainsKey(color)){
            inventory.AddColor(color);
        }else{
            Log.log("This bullet color does not exist. " + color.ToString());
        }
    }
    

    public void RemoveColor(Colors color){
        if(bulletColor == color){
            Log.log("Can't remove active color");
        }else if(!inventory.RemoveColor(color)){
            Log.log("Can't remove a color this character doesn't own");
        }
    }

    public void SetBulletColor(Colors color){
        if(inventory.BulletColors.Contains(color)){
            bulletColor = color;
            if(activeWeapon!=null){
                activeWeapon.ChangeColor(color);
            }
        }else{
            Log.log(gameObject.name+" doesn't own this color");
        }
    }
    //yohan added
    public void Kill()
    {
        foreach (ICharacter enemy in gm.ActiveEnemies)
        {
            enemy.GetStats().LowerHP(50000);
        }
        stateUI.Refresh();
        if (stats.HP == 0 && IsAlive)
        {
            OnDeath();
        }
    }

    protected virtual void OnWorldColorChange(Colors color){}

    protected virtual void BroadcastColorSwitch(int dir){
        gm.NextColor(dir);
    }

    public virtual bool HitCheck(Bullet bullet){
        Log.log("This character has no HitCheck implementation (" + gameObject.name +")");
        return false;
    }

    protected virtual void OnDeath(){
        if(OnDeathEvent!=null) OnDeathEvent(this);
    }

    public virtual Vector3 GetAimDirection(){
        return combat.GetAimDirection();
    }

    public Colors GetColor(){
        return color;
    }
    public void SetColor(Colors setColor)//yohan added
    {
        color =setColor ;
    }

    public virtual void OnEnemyDeath(ICharacter enemy){
        Log.log("This character has no OnEnemyDeath implementation (" + gameObject.name +")");
    }

    public Drops GetDrops(){
        return drops;
    }
    public AudioManager GetAudioManager()
    {
        return audioManager;
    }
    public AnimationManager GetAnimationManager()
    {
        return animationManager;
    }
    protected virtual void OnDestroy(){
        if (GetGameObject() != null)
        {
            input.OnColorSwitch -= BroadcastColorSwitch;
            gm.WorldColorChange -= OnWorldColorChange;
            movement.Terminate();
            combat.Terminate();
        }
    }
}
