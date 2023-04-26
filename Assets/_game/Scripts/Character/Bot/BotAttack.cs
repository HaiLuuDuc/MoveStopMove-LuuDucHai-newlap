using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttack : CharacterAttack
{
    [SerializeField] private CharacterAnimation characterAnim;
    [SerializeField] private Bot bot;


    public void Update()
    {
        if (bot.enemyList.Count > 0)
        {
            FindNearestTarget();
        }
        else
        {
            enemy = null;
        }
    }

    public override IEnumerator Attack()
    {
        Vector3 enemyPos = enemy.transform.position;

        bot.DisplayOnHandWeapon();

        characterAnim.ChangeAnim(Constant.ATTACK);

        RotateToTarget();

        yield return new WaitForSeconds(0.4f);//thời gian vung tay cho đến khi vũ khí rời bàn tay là 0.4s
        if (character.isDead)
        {
            yield break;
        }

        bot.UnDisplayOnHandWeapon(); // tat hien thi weapon tren tay
        Weapon newWeapon = weaponPool.GetObject().GetComponent<Weapon>(); // lay weapon tu` pool
        newWeapon.transform.position = rightHand.transform.position; // dat weapon vao tay phai character

        ReCaculateTargetWeapon(newWeapon.gameObject, enemyPos);// đảm bảo vũ khí bay từ lòng bàn tay chứ không phải từ bot.position
        newWeapon.Fly(targetWeapon.position, newWeapon.weaponData.flySpeed);

        //play sound
        if (AudioManager.instance.IsInDistance(this.transform))
        {
            AudioManager.instance.Play(SoundType.Throw);
        }
        yield return null;
    }
}
