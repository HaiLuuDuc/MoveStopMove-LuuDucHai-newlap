using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    [Header("Player class:")]
    public Material whiteMat;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 initialPos;
    [SerializeField] private Vector3 initialRot;

    protected void Start()
    {
        if (DataManager.ins.playerData.currentBodyMat != null)
        {
            currentBodyMat = DataManager.ins.playerData.currentBodyMat;
        }
        else
        {
            currentBodyMat = whiteMat;
        }
        OnInit();
    }

    public override void OnInit() {
        base.OnInit();
        SetPosAndRot(initialPos, initialRot);
        StopMoving();
        GetWeaponFromInventory();
        characterAnim.ChangeAnim(Constant.IDLE);
        SetSkinnedMeshRenderer(currentBodyMat);
        DataManager.ins.playerData.currentBodyMat = currentBodyMat;
        isMoving = false;
    }

    public override void OnDeath() {
        DisableCollider();
        characterAnim.ChangeAnim(Constant.DIE);
        isDead = true;
        SetSkinnedMeshRenderer(deathMaterial);
        DataManager.ins.playerData.currentBodyMat = deathMaterial;
        LevelManager.instance.DeleteThisElementInEnemyLists(this);
        LevelManager.instance.currentAlive--;
        LevelManager.instance.characterList.Remove(this);
        UIManager.instance.ShowLosePanel();
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play(SoundType.Lose);
        }   
    }

    public override void EnableCollider()
    {
        capsulCollider.enabled = true;
        rb.velocity = Vector3.zero;
        rb.useGravity = true;
    }

    public override void DisableCollider()
    {
        capsulCollider.enabled = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    public void Idle()
    {
        characterAnim.ChangeAnim(Constant.IDLE);
    }

    public void Dance()
    {
        StopMoving();
        characterAnim.ChangeAnim(Constant.DANCE);
    }

    public void GetWeaponFromInventory()
    {
        GameObject wp = Instantiate(WeaponShopManager.Instance.GetWeapon());
        if (onHandWeapon != null)
        {
            Destroy(onHandWeapon);
        }
        onHandWeapon = wp;
        DisplayOnHandWeapon();
        onHandWeapon.transform.SetParent(rightHand.transform);
        onHandWeapon.transform.localPosition = Vector3.zero;
        onHandWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        onHandWeapon.GetComponent<BoxCollider>().enabled = false;
        wp.GetComponent<Weapon>().SetOwnerAndWeaponPool(this, this.weaponPool);
        weaponPool.prefab = wp;
        weaponPool.OnDestroy();
        weaponPool.OnInit();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.WEAPON) && other.GetComponent<Weapon>().GetOwner() != this)
        {
            string enemyName = other.GetComponent<Weapon>().GetOwner().GetComponent<Bot>().botName.GetComponent<CanvasNameOnUI>().nameString;
            UIManager.instance.loseText.text = Constant.YOU_WERE_KILLED_BY + enemyName;
            OnDeath();
        }
    }
}
