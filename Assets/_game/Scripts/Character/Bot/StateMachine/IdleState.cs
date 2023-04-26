using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IdleState : IState
{
    float elapsedTime;
    float duration;


    public void OnEnter(Bot bot)
    {
        duration = Random.Range(1f, 2f);
        elapsedTime = 0f;
        bot.DisplayOnHandWeapon();
        bot.StopMoving();
    }

    public void OnExecute(Bot bot)
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if(bot.botAttack.enemy != null && LevelManager.instance.isGaming)
            {
                bot.ChangeState(new AttackState());
            }
        }
        else if(LevelManager.instance.isGaming == true)
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot) { }

}