using System;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

[System.Serializable]

public class Inventory
{

    public int Credits { get; private set; }
    public int Life { get; private set; }

    public List<Colors> BulletColors { get; private set; }
    public List<Weapon> Weapons { get; private set; }

    public Inventory(int credits = 0, List<Colors> colors = null, List<Weapon> weapons = null, int life = 1)
    {
        Credits = credits;
        Life = life;
        BulletColors = colors == null ? new List<Colors>() : colors;
        Weapons = weapons == null ? new List<Weapon>() : weapons;
    }
    public void SetCredits(int amount)
    {
        Credits = amount;
    }

    public int AddCredits(int amount)
    {
        return Credits += amount;
    }
    public void AddLife(int amount)
    {
        Life += amount;
    }

    public bool RemoveCredits(int amount)
    {
        int temp = Credits - amount;
        if (temp >= 0)
        {
            Credits = temp;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool RemoveLife(int amount)
    {
        int temp = Life - amount;
        if (temp >= 0)
        {
            Life = temp;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        Weapons.Add(weapon);
    }

    public bool RemoveWeapon(Weapon weapon)
    {
        if (Weapons.Contains(weapon))
        {
            Weapons.Remove(weapon);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddColor(Colors color)
    {
        BulletColors.Add(color);
    }

    public bool RemoveColor(Colors color)
    {
        if (BulletColors.Contains(color))
        {
            BulletColors.Remove(color);
            return true;
        }
        else
        {
            return false;
        }
    }

}