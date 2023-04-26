using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PatrolState : IState
{
    float elapsedTime;
    float duration;
    float randomRotateValue;


    public void OnEnter(Bot bot)
    {
        elapsedTime = 0f;
        duration = Random.Range(4f, 7f);
        randomRotateValue = Random.Range(0.4f,0.6f);
        bot.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(-120f, 120f), 0));
        bot.DisplayOnHandWeapon();
        bot.Move();
    }

    public void OnExecute(Bot bot)
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if(bot.CheckDestinationIsOutOfMap()){
                bot.navMeshAgent.velocity = Vector3.zero; // dung lai
                bot.GoToCenterOfMap();
            }
            bot.navMeshAgent.SetDestination(bot.destinationTransform.position); // di ve phia truoc mat
            bot.transform.Rotate(0, randomRotateValue, 0); // chuyen dong tron deu
            if (bot.botAttack.enemy!=null && elapsedTime > 0.5f) // bot only attacks after 0.8 seconds patrol
            {
                bot.ChangeState(new AttackState());
            }
        }
        else
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot) { }
}
