using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class DieState : IState
{
    float duration;
    float elasedTime;
    int tempAlive;


    public void OnEnter(Bot bot)
    {
        elasedTime = 0f;
        duration = 2f;
        bot.StopMoving();
        bot.OnDeath(); 
        LevelManager.instance.currentAlive--;
        tempAlive = LevelManager.instance.currentAlive;
    }

    public void OnExecute(Bot bot)
    {
        if(elasedTime<duration)
        {
            elasedTime += Time.deltaTime;
        }
        else
        {
            BotManager.instance.DespawnBot(bot);
            if (tempAlive > BotManager.instance.botSize)
            {
                BotManager.instance.SpawnBot();
            }
        }
    }

    public void OnExit(Bot bot) { }
}
