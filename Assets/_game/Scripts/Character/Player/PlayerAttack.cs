using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : CharacterAttack
{
    [SerializeField] private TargetCircle targetCircle;
    private bool canAttack;


    protected  void Start()
    {
        canAttack = true; // character dung yen tu dau game van co the attack enemy di vao circle
        targetCircle.Deactive();
    }

    protected  void Update()
    {
        if (LevelManager.instance.isGaming == false)
        {
            return;
        }
        if (character.isDead == true)
        {
            targetCircle.Deactive();
            return;
        }
        if (character.isMoving)
        {
            canAttack = false;
        }
        // only correct for player, not for bots
        if (Input.GetMouseButton(0))
        {
            if (!character.onHandWeapon.activeSelf)
            {
                character.DisplayOnHandWeapon();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            canAttack = true;
        }

        if(character.enemyList.Count>0)
        {
            FindNearestTarget();
        }
        else
        {
            enemy = null;
        }

        if (canAttack && character.isMoving==false && enemy!=null)
        {
            RotateToTarget();
            StartCoroutine(Attack());
            StartCoroutine(DelayAttack(1.1f));
        }

        if(character.enemyList.Count > 0)
        {
            targetCircle.Active();
        }
        else
        {
            targetCircle.Deactive();
        }
    }

    public override IEnumerator Attack()
    {
        if (enemy != null)
        {
            Vector3 enemyPos = enemy.transform.position;
            
            character.DisplayOnHandWeapon();// hien thi weapon tren tay

            characterAnimation.ChangeAnim(Constant.ATTACK);// vung tay trong 0.4s

            float elapsedTime = 0f;
            float duration = 0.4f;
            while (elapsedTime < duration)
            {
                if (character.isMoving) // neu character di chuyen thi cancel vung tay, dong thoi cancel weapon fly
                {
                    goto label;
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                }
                yield return null;
            }

            character.UnDisplayOnHandWeapon(); // tat hien thi weapon tren tay
            Weapon newWeapon = character.weaponPool.GetObject().GetComponent<Weapon>(); // lay weapon tu` pool
            newWeapon.transform.position = rightHand.transform.position; // dat weapon vao tay phai character
            
            ReCaculateTargetWeapon(newWeapon.gameObject, enemyPos); // dam bao weapon bay qua center cua enemy
            newWeapon.Fly(targetWeapon.position, newWeapon.weaponData.flySpeed);

            //play sound
            if (AudioManager.instance.IsInDistance(this.transform))
            {
                AudioManager.instance.Play(SoundType.Throw);
            }
        }
    label:;
        yield return null;
    }

    public IEnumerator DelayAttack(float delayTime) // set canAttack = false trong 2s, sau do set canAttack = true (tranh attack lien tuc)
    {
        canAttack = false;
        float elapsedTime = 0f;
        float duration = delayTime;
        while (elapsedTime < duration)
        {
            if (character.isMoving) // neu di chuyen thi cancel coroutine, vi khi do canAttack == true
            {
                goto label1;
            }
            else
            {
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }
        canAttack = true;
        if (character.isDead==false && LevelManager.instance.isGaming==true)
        {
            characterAnimation.ChangeAnim(Constant.IDLE);
        }
        character.DisplayOnHandWeapon();
        label1:;
    }

}


