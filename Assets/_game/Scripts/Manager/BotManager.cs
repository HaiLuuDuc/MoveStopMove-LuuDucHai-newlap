using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    [Header("Position:")]
    public Transform topLeftCorner;
    public Transform bottomRightCorner;
    [SerializeField] private float spawnDistance;
    public float initialY;

    [Header("Manager:")]
    [SerializeField] private Transform bots;
    public BotPool botPool;
    public int botSize;
    public List<Bot> botList = new List<Bot>();

    
    //singleton
    public static BotManager instance;
    private void Awake()
    {
        instance= this;
    }

    void Start()
    {
        SpawnBots();
    }

    public void SpawnBots()
    {
        for(int i = 0; i < botSize; i++)
        {
            SpawnBot();
        }
    }

    public void SpawnBot()
    {
        Bot pooledBot = botPool.GetObject().GetComponent<Bot>();
        SetPosAndRotFarAwayFromOthers(pooledBot);
        pooledBot.transform.SetParent(bots);
        pooledBot.OnInit();

        SpawnBotName(pooledBot);
        SpawnBotIndicator(pooledBot);
        SpawnBotPants(pooledBot);
        SpawnBotHat(pooledBot);
        SpawnBotWeapon(pooledBot);

        if (botList.Count<botSize)
        {
            botList.Add(pooledBot);
        }
        if (!LevelManager.instance.characterList.Contains(pooledBot))
        {
            LevelManager.instance.characterList.Add(pooledBot);
        }
    }

    public void DespawnBot(Bot bot)
    {
        //Debug.Log("despawn bot");
        bot.DeActiveNavmeshAgent();
        BotNamePool.instance.ReturnToPool(bot.botName);// despawn pooledbotname
        LevelManager.instance.characterList.Remove(bot);
        botPool.ReturnToPool(bot.gameObject);
    }

    public void SetPosAndRotFarAwayFromOthers(Bot bot)
    {
        Vector3 spawnPosition;
        Vector3 spawnRotation;
        spawnRotation = new Vector3(0, Random.Range(0, 360), 0);
        do
        {
            int randomX = (int)Random.Range(topLeftCorner.position.x, bottomRightCorner.position.x);
            int randomZ = (int)Random.Range(bottomRightCorner.position.z, topLeftCorner.position.z);
            spawnPosition = new Vector3(randomX, initialY, randomZ);
        } while (CheckPosition(spawnPosition) == false); //spawn position cho bot sao cho nó không đứng gần các thằng khác
        bot.transform.position = spawnPosition;
        bot.transform.rotation = Quaternion.Euler(spawnRotation);
    }
    
    public bool CheckPosition(Vector3 pos)
    {
        for(int i = 0; i < LevelManager.instance.characterList.Count; i++)
        {
            if (Vector3.Distance(LevelManager.instance.characterList[i].transform.position, pos) < spawnDistance)
            {
                return false;
            }
        }
        return true;
    }

    public void SpawnBotName(Bot bot)
    {
        //Debug.Log("spawn bot name");
        GameObject botName = BotNamePool.instance.GetObject();
        botName.GetComponent<CanvasNameOnUI>().SetTargetTransform(bot.transform);
        botName.GetComponent<CanvasNameOnUI>().SetColor(bot);
        bot.botName = botName;
        botName.SetActive(true);
    }
   
    public void SpawnBotIndicator(Bot bot)
    {
        bot.indicator.SetColor(bot);
    }

    public void SpawnBotPants(Bot bot)
    {
        int index = (int)Random.Range(0, SkinShopManager.instance.pants.Length);
        bot.botWearSkinItems.WearPants(index);
    }

    public void SpawnBotHat(Bot bot)
    {
        if (bot.isHaveHat == false)
        {
            int index = (int)Random.Range(0, SkinShopManager.instance.hats.Length);
            bot.botWearSkinItems.WearHat(index);
            bot.isHaveHat = true;
        }
    }

    public void SpawnBotWeapon(Bot bot)
    {
        if (bot.isHaveWeapon == false)
        {
            int randomWeaponIndex = Random.Range(0, WeaponShopManager.Instance.weapons.Length);
            Weapon newWeapon = Instantiate(WeaponShopManager.Instance.weapons[randomWeaponIndex].gameObject).GetComponent<Weapon>();
            newWeapon.gameObject.SetActive(true);
            newWeapon.transform.SetParent(bot.rightHand.transform);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
            newWeapon.SetOwnerAndWeaponPool(bot, bot.weaponPool);
            bot.onHandWeapon = newWeapon.gameObject;
            bot.weaponPool.prefab = newWeapon.gameObject;
            bot.onHandWeapon.GetComponent<BoxCollider>().enabled = false;
            bot.isHaveWeapon = true;
        }
    }
}
