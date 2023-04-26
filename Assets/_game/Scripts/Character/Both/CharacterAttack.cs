using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAttack : MonoBehaviour
{
    [Header("Character Properties:")]
    [SerializeField] protected Character character;
    [SerializeField] protected Transform rightHand;

    [Header("Weapon Properties:")]
    [SerializeField] protected Transform targetWeapon;
    [SerializeField] protected WeaponPool weaponPool;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float weaponFlySpeed;
    [SerializeField] private float weaponRotateSpeed;



    [Header("Animation:")]
    [SerializeField] protected CharacterAnimation characterAnimation;

    [Header("Enemy:")]
    public Character enemy;


    public void FindNearestTarget()
    {
        this.enemy = null;
        if (character.enemyList.Count > 0)
        {
            float minDistance = 100f;
            for (int i = 0; i < this.character.enemyList.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, this.character.enemyList[i].transform.position);
                if (distance < minDistance)
                {
                    this.enemy = this.character.enemyList[i];
                    minDistance = distance;
                }
            }
        }
    }

    public void RotateToTarget()
    {
        if (this.enemy != null)
        {
            Vector3 dir;
            dir = this.enemy.transform.position - this.transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    public void ReCaculateTargetWeapon(GameObject obj, Vector3 enemyPos)
    {
        Vector3 dir = enemyPos - obj.transform.position;
        dir.y = 0;
        dir = dir.normalized;
        this.targetWeapon.position = obj.transform.position + dir * this.attackRange;
    }

    public abstract IEnumerator Attack();

}
