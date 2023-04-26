using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Data:")]
    public WeaponData weaponData;
    public Transform child;
    public MeshRenderer meshRenderer;
    [Header("Manage:")]
    protected WeaponPool weaponPool;
    protected Character owner;
    public int currentMaterialIndex;
    [Header("Bool Variables:")]
    public bool isStuckAtObstacle;
    public bool isPurchased;


    protected void Start()
    {
        currentMaterialIndex = 0;
        isStuckAtObstacle = false;
    }

    public void ChangeMaterial(int index)
    {
        meshRenderer.material = weaponData.GetWeaponMaterial(index);
    }

    public Character GetOwner()
    {
        return this.owner;
    }

    public void SetOwnerAndWeaponPool(Character owner, WeaponPool weaponPool)
    {
        this.owner = owner;
        this.weaponPool = weaponPool;
    }

    public virtual void Fly(Vector3 target, float flySpeed)
    {
        StartCoroutine(FlyStraight(target,flySpeed));
    }

    public virtual IEnumerator FlyStraight(Vector3 target, float flySpeed)
    {
        while(Vector3.Distance(this.transform.position,target) > 0.1f && this.gameObject.activeSelf && !this.isStuckAtObstacle)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, flySpeed * Time.deltaTime);
            yield return null;
        }
        if (!isStuckAtObstacle)
        {
            this.weaponPool.ReturnToPool(this.gameObject);
        }
        yield return null;
    }

    public IEnumerator StuckAtObstacle()
    {
        isStuckAtObstacle = true;
        yield return new WaitForSeconds(1);
        isStuckAtObstacle = false;
        weaponPool.ReturnToPool(this.gameObject);
        yield return null;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.BOT)|| other.gameObject.CompareTag(Constant.PLAYER))
        {
            Character character = other.gameObject.GetComponent<Character>();
            if(character != this.owner){
                weaponPool.ReturnToPool(this.gameObject);
                this.owner.TurnBigger();
                //play sound
                if (AudioManager.instance.IsInDistance(this.transform))
                {
                    AudioManager.instance.Play(SoundType.Die);
                }
            }
            if(character is Bot && this.owner is Player)
            {
                DataManager.ins.playerData.coin += 10;
                UIManager.instance.UpdateUICoin();
            }
        }
        if (other.gameObject.CompareTag(Constant.OBSTACLE))
        {
            StartCoroutine(StuckAtObstacle());
        }
    }
  
}

