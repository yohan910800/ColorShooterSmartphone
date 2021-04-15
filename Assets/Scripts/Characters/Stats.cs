[System.Serializable]//yohan added
public class Stats
{
    // Properties
    public int HP { get; private set; }
    public int Attack { get; private set; }
    public float Speed { get; private set; }
    public float baseSpeed { get; private set; }
    public float AttackSpeed { get; private set; }
    public int Energy { get; private set; }
    public int Defence { get; private set; }
    public int CriticalChance { get; private set; }
    public int[][][][][][] Weaponstatus { get; private set; }

    // Members
    public int maxHP;
    int maxEnergy;


    // Constructor
    public Stats(int hp, int att, float speed, float attSpeed, int defence, int criticalChance)
    {
        maxHP = hp;
        HP = hp;
        Attack = att;
        baseSpeed = speed;
        Speed = speed;
        AttackSpeed = attSpeed;
        Energy = 0;
        maxEnergy = 100;
        Defence = defence;
        CriticalChance = criticalChance;
    }
    public int[][][][][][] SetWeaponStatus(int[][][][][][] weaponstatus)
    { //----------------- want to ask daniel and yohan about this.
        Weaponstatus = weaponstatus;
        return weaponstatus;
    }
    public int[][][][][][] GetWeaponStatus(int[][][][][][] weaponstatus)
    {
        return weaponstatus;
    }
    // Methods
    public int LowerHP(int dmg)
    {
        HP -= dmg;
        if (HP < 0) HP = 0;
        return HP;
    }

    public int Heal(int val)
    {
        HP += val;
        if (HP > maxHP) HP = maxHP;
        return HP;
    }

    public float GetNormalizedHP()
    {
        return (float)HP / (float)maxHP;
    }

    public int GainEnergy(int amount)
    {
        Energy += amount;
        if (Energy > maxEnergy) Energy = maxEnergy;
        return Energy;
    }

    public int LoseEnergy(int amount)
    {
        Energy -= amount;
        if (Energy < 0) Energy = 0;
        return Energy;
    }

    public float GetNormalizedEnergy()
    {
        return (float)Energy / (float)maxEnergy;
    }

    public float ModifySpeed(float amount)
    {
        Speed += amount;
        if (Speed < 0) Speed = 0;
        return Speed;
    }

    public float ResetSpeed()
    {
        Speed = baseSpeed;
        return Speed;
    }
    public float SetBaseSpeed(float amount)
    {
        baseSpeed = amount;
        return baseSpeed;
    }
    public float SetSpeed(float amount)
    {
        Speed = amount;

        return Speed;
    }
    public int SetHP(int amount)
    {
        HP = amount;
        return HP;
    }
    public int SetMaxHP(int amount)
    {

        maxHP = amount;
        return HP;
    }
    public int SetAttack(int amount)
    {
        Attack = amount;
        return Attack;

    }
    public int SetDefence(int amount)
    {
        Defence = amount;
        return Defence;
    }
    public int SetCriticalChance(int amount)
    {
        CriticalChance = amount;
        return CriticalChance;
    }
    public int setEnergy(int amount)
    {
        Energy = amount;
        return Energy;
    }


}