using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class StatusEffect
{
    protected Player player;
    protected StatusEffect(Player player){
        this.player=player;
    }
}

public class Slow : StatusEffect
{
    int stack;
    float lastApplied;
    float duration = 3;

    public Slow(Player player) : base(player)
    {
    }

    public void Apply(){
        if (stack<3)
        {
            stack++;
            player.speed=player.maxSpeed/(1+stack);
        }
        lastApplied=Time.time;
    }
    public void update(){
        if (lastApplied+duration<Time.time && stack > 0)
        {
            lastApplied=Time.time;
            stack--;
            player.speed=player.maxSpeed/(1+stack);
        }
    }
}
public class Burn : StatusEffect
{
    float previousApplicationTime;
    float burnDelay = 0.5f;

    public Burn(Player player) : base(player)
    {
        previousApplicationTime=-1;
    }

    public void Apply(){
        if (previousApplicationTime+burnDelay<Time.time)
        {
            player.vie--;
            previousApplicationTime=Time.time;
        }
    }
}