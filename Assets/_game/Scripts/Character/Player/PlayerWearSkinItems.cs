using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class PlayerWearSkinItems : CharacterWearSkinItems
{
    [SerializeField] private Player player;
    public GameObject currentHat;
    public ItemController[] itemControllers;
    private GameObject currentShield;
    private GameObject currentWing;
    private GameObject currentLeftHandObject;
    private GameObject currentTail;

    public override GameObject WearHat(int index)
    {
        DestroyCurrentHat();
        currentHat = base.WearHat(index);
        return null;
    }

    public void DestroyCurrentHat()
    {
        if (currentHat != null)
        {
            Destroy(currentHat);
        }
    }

    public override void WearPants(int index)
    {
        if (index >= 0)
        {
            pants.material = SkinShopManager.instance.pants[index];
        }
    }

    public void DestroyCurrentPants()
    {
        pants.material = Colors.instance.transparent100;
    }

    public override GameObject WearShield(int index)
    {
        DestroyCurrentShield();
        currentShield = base.WearShield(index);
        return null;
    }

    public void DestroyCurrentShield()
    {
        if (currentShield != null)
        {
            Destroy(currentShield);
        }
    }

    public void WearFullSet(int index)
    {
        DestroyCurrentFullSet();
        player.SetSkinnedMeshRenderer(fullsetDatas[(int)index].mat);
        DataManager.ins.playerData.currentBodyMat = fullsetDatas[(int)index].mat;
        player.SetCurrentBodyMat(fullsetDatas[(int)index].mat);
        if (fullsetDatas[index].head != null)
        {
            currentHat = Instantiate(fullsetDatas[index].head);
            currentHat.transform.SetParent(dinhdau.transform);
            ZeroTheChild(currentHat);
        }
        if (fullsetDatas[index].wing != null)
        {
            currentWing = Instantiate(fullsetDatas[index].wing);
            currentWing.transform.SetParent(lung.transform);
            ZeroTheChild(currentWing);
        }
        if (fullsetDatas[index].leftHandObject != null)
        {
            currentLeftHandObject = Instantiate(fullsetDatas[index].leftHandObject);
            currentLeftHandObject.transform.SetParent(leftHand2.transform);
            ZeroTheChild(currentLeftHandObject);
        }
        if (fullsetDatas[index].tail != null)
        {
            currentTail = Instantiate(fullsetDatas[index].tail);
            currentTail.transform.SetParent(tail.transform);
            ZeroTheChild(currentTail);
        }
    }

    public void DestroyCurrentFullSet()
    {
        player.SetSkinnedMeshRenderer(player.whiteMat);
        player.SetCurrentBodyMat(player.whiteMat);
        DataManager.ins.playerData.currentBodyMat = player.whiteMat;
        if (currentHat != null)
        {
            Destroy(currentHat);
        }
        if (currentWing != null)
        {
            Destroy(currentWing);
        }
        if (currentLeftHandObject != null)
        {
            Destroy(currentLeftHandObject);
        }
        if(currentTail != null)
        {
            Destroy(currentTail);
        }
    }

    public void DestroyAllItemsOnBody()
    {
        DestroyCurrentHat();
        DestroyCurrentPants();
        DestroyCurrentShield();
        DestroyCurrentFullSet();
    }

    public void ZeroTheChild(GameObject obj)
    {
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void PutOnItems()
    {
        //wear fullset
        ItemController fullsetController = itemControllers[(int)ItemType.FullSet];
        if (fullsetController.usingIndex >= 0)
        {
            WearFullSet(fullsetController.usingIndex);
        }
        else
        {
            DestroyCurrentFullSet();
        }
        //wear hat
        ItemController hatController = itemControllers[(int)ItemType.Hat];
        if (hatController.usingIndex >= 0)//nếu đã mua mũ rồi thì dùng cái mũ đó
        {
            WearHat(hatController.usingIndex);
        }
        else//nếu chưa mua cái nào mà chỉ đang thử thì sẽ phải trả lại mũ đang thử cho shop
        {
            if (itemControllers[(int)ItemType.FullSet].usingIndex < 0)//tranh truong hop destroy hat cua fullset
                DestroyCurrentHat();
        }
        //wear pants
        ItemController pantsController = itemControllers[(int)ItemType.Pants];
        if (pantsController.usingIndex >= 0)
        {
            WearPants(pantsController.usingIndex);
        }
        else
        {
            DestroyCurrentPants();
        }
        //wear shield
        ItemController shieldController = itemControllers[(int)ItemType.Shield];
        if (shieldController.usingIndex >= 0)
        {
            WearShield(shieldController.usingIndex);
        }
        else
        {
            DestroyCurrentShield();
        }

    }

}
