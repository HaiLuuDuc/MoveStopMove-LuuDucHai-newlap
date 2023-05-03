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
        UIManager.instance.SetRankText(LevelManager.instance.currentAlive);
        BotManager.instance.DisableAllBots();
        UIManager.instance.ShowRevivePanel();
        UIManager.instance.HideJoystick();
    }

    public void OnRevive()
    {
        EnableCollider();
        StopMoving();
        //GetWeaponFromInventory();
        characterAnim.ChangeAnim(Constant.IDLE);
        SetSkinnedMeshRenderer(currentBodyMat);
        DataManager.ins.playerData.currentBodyMat = currentBodyMat;
        isDead = false;
        isMoving = false;
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
            UIManager.instance.loseText.text = Constant.YOU_VE_BEEN_KILLED_BY + enemyName;
            OnDeath();
        }
    }
}
