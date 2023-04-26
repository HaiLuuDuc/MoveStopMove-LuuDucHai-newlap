using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Player:")]
    [SerializeField] private Player player;

    

    [Header("Lists:")]
    public List<Character> characterList = new List<Character>();

    [Header("Alive:")]
    public int initialAlive;
    public int currentAlive;

    [Header("Bool Variables:")]
    public bool isGaming;
    public bool isWin;

    [Header("NavMesh:")]
    public NavMeshData[] navMeshDatas;
    private int currentNavMeshIndex = 0;

    //singleton
    public static LevelManager instance;
    private void Awake()
    {
        instance = this;
        characterList.Add(player);
    }

    void Start()
    {
        currentAlive = initialAlive;
        isGaming = false;
        isWin = false;
        DisnableALlCharacters();
        UIManager.instance.UpdateUICoin();
    }

    private void Update()
    {
        if (currentAlive == 1 && player.isDead == false && isGaming == true)
        {
            UIManager.instance.ShowWinPanel();
            player.Dance();
            isGaming = false;
            isWin = true;
            AudioManager.instance.Play(SoundType.Win);
        }
    }

    public void DeleteCharacters()
    {
        if (!player.isDead)
        {
            characterList.Remove(player);
        }
        while(characterList.Count>0)
        {
            // tat het cac weapon dang bay
            for (int j = 0; j < characterList[Constant.FIRST_INDEX].pooledWeaponList.Count; j++)
            {
                if (characterList[Constant.FIRST_INDEX].pooledWeaponList[j].gameObject.activeSelf)
                {
                    characterList[Constant.FIRST_INDEX].pooledWeaponList[j].gameObject.SetActive(false);
                }
            }
            // despawn bot
            BotManager.instance.DespawnBot(characterList[Constant.FIRST_INDEX] as Bot);
        }
        characterList.Clear();
    }

    public void RespawnCharacters()
    {
        characterList.Add(player);
        player.OnInit();
        for (int i = 0; i < BotManager.instance.botList.Count; i++)
        {
            BotManager.instance.SpawnBot();
        }
    }

    public void DeleteThisElementInEnemyLists(Character character)
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            if (characterList[i].enemyList.Contains(character))
            {
                characterList[i].enemyList.Remove(character);
            }
        }
    }

    public void PlayGame()
    {
        /*DeleteCharacters();
        RespawnCharacters();*/
        UIManager.instance.ShowJoystick();  
        UIManager.instance.ShowIndicators();
        UIManager.instance.ShowCanvasName();
        UIManager.instance.ShowAliveText();
        UIManager.instance.ShowSettingsObj();
        UIManager.instance.HideCoin();
        EnableALlCharacters();
        UIManager.instance.SetPlayerNameFromInputField();
        currentAlive = initialAlive;
        isGaming = true;
    }

    public void EnableALlCharacters()
    {
        for(int i = 0; i < BotManager.instance.botList.Count; i++)
        {
            BotManager.instance.botList[i].ChangeState(new PatrolState());
        }
        isGaming = true;
    }

    public void DisnableALlCharacters()
    {
        for (int i = 0; i < BotManager.instance.botList.Count; i++)
        {
            BotManager.instance.botList[i].ChangeState(new IdleState());
        }
        isGaming = false;
    }



}
