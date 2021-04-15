using System;
using UnityEngine;
using MankindGames;

public interface ICharacter {
    // Events
    event Action OnInputModuleChange;
    event Action<Weapon> OnWeaponChange;
    event Action<ICharacter> OnDeathEvent;

    // Methods
    void Init();
    IInputModule GetInputModule();
    GameObject GetGameObject();
    Stats GetStats();
    Stats SetStats(Stats gotStats);
    Weapon GetActiveWeapon();
    Weapon GetActiveWeapon2();
    
    Inventory GetInventory();//yohan added
    StateUI GetStateUI();//yohan added
    void ActivateWeapon(Weapon weapon);
    void ActivateAllWeapons(Weapon weapon);
    void SetBulletColor(Colors color);
    void SetColor(Colors setColor);//yohan added
    void Kill();//yohan added
    bool HitCheck(Bullet bullet);
    Vector3 GetAimDirection();
    Colors GetColor();
    void OnEnemyDeath(ICharacter enemy);
    Drops GetDrops();
    void GetHeal(int val);
    AudioManager GetAudioManager();
    AnimationManager GetAnimationManager();

    // Properties
    GameObject deadEffectObj { get; set; }
    GameObject deadEffectAnimObj { get; set; }
    GameObject playerDeadBody { get; set; }
    GameObject[] playerMainBody { get; set; }
    bool activeInvisible { get; set; }
    bool IsAlive {get; set;}
    bool ActivateAutoAim {get; set; }//yohan added
    int phase { get; set; }//yohan added
    bool isItAllowToAttack { get; set; }//yohan added
    bool isInvincible { get; set; }//yohan added
    bool isPoisoned { get; set; }//yohan added
    int activeWeaponIndex { get; set; }//yohan added
    Weapon[] equipedWeapons { get; set; }//yohan added

}