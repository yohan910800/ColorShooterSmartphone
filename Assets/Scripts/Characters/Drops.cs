using System;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class Drops {
    public int credits;
    public int energy;

    public Drops(int credits, int energy){
        this.credits = credits;
        this.energy = energy;
    }
    public int SetEnergy(int amount)
    {
        energy = amount;
        return energy;
    }
    public int SetCredits(int amount)
    {
        credits = amount;
        return credits;
    }
}