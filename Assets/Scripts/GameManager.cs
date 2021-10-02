using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject enemyTarget;
    [SerializeField] GameObject playerTarget;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] DayNightCycle dayNightCycleManager;
    [SerializeField] UIManager uiManager;
    public Pumpkin pumpkin;
    public PlayerController player;
    public bool isGameOver;
    public bool allEnemyDead = false;
    public bool shootgunOnHand = true;

    public float water;
    public int money;
    private int moneyReward = 400;
    public int essence;
    internal int requiredEssenceToExchange = 8;

    public int fertilizer;
    internal int requiredMoneyToBuyFertilizer = 400;

    public int fence;
    internal int requiredMoneyToBuyFence = 600;
    private int winPlace = 1;
    public bool testPlace;

    private void Update()
    {
        if (pumpkin.health <= 0 || player.health <= 0)
        {
            LoseGame();
        }
        uiManager.moneyText.text = $"{money} $";
        uiManager.hpBar.fillAmount = player.health / 100.0f;
    }

    void Start()
    {
        if (testPlace)
        {
            GetPlace();
            Debug.Log(winPlace);
        }
        pumpkin = enemyTarget.gameObject.GetComponent<Pumpkin>();
        dayNightCycleManager.SetDay();
        InvokeRepeating(nameof(CheckConditions), 5, 1);
        money = 975;
        water = 10;
        fence = 1;
    }
    public void SkipDay()
    {
        dayNightCycleManager.skipDay = true;
    }
    public void DayInformer(string text)
    {
        uiManager.SetDayText(text);
    }
    public void LoseGame()
    {
        isGameOver = true;
        DisableGUI();
        uiManager.LoseGame();
    }
    private void DisableGUI()
    {
        var temp = GameObject.FindObjectsOfType<NonPlayerCharacter>();
        foreach (var item in temp)
        {
            item.enabled = false;
        }
    }
    private void GetPlace()
    {
        float iteration = 25.0f;
        float temp = 0;
        for (int i = 10; i > 0; i--)
        {
            temp += iteration;
            if (pumpkin.weight <= temp)
            {
                winPlace = i;
                return;
            }
        }
    }
    public void WinGame()
    {
        isGameOver = true;
        DisableGUI();
        GetPlace();
        uiManager.winText.text = $"your pumpkin({pumpkin.weight}kg) took {winPlace} place \ngood job";
        uiManager.WinGame();
    }
    public Transform GetPlayerPos()
    {
        return playerTarget.transform;
    }
    public Transform GetPumpkinPos()
    {
        return enemyTarget.transform;
    }
    public void IsGamePaused(bool value)
    {
        if (value)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    private void CheckConditions()
    {
        if (dayNightCycleManager.setNight && dayNightCycleManager.levelClean)
        {
            spawnManager.canSpawn = true;
            dayNightCycleManager.setNight = false;
        }
        if (allEnemyDead)
        {
            //dayNightCycleManager.levelClean = true;
            dayNightCycleManager.SetDay();
            dayNightCycleManager.levelClean = false;
            allEnemyDead = false;
            AddMoney(moneyReward);
            pumpkin.GrowthPumkin();
            pumpkin.health = 100;
            player.health = 100;
            moneyReward = Mathf.RoundToInt(moneyReward * 1.2f);
        }
    }
    public bool ExchangeEssence()
    {
        if (essence >= requiredEssenceToExchange)
        {
            essence -= requiredEssenceToExchange;
            return true;
        }
        return false;
    }
    public bool BuyFence()
    {
        if (money >= requiredMoneyToBuyFence)
        {
            money -= requiredMoneyToBuyFence;
            return true;
        }
        return false;
    }
    public void FillWateringCan()
    {
        water = 10f;
    }
    public void AddMoney(int money)
    {
        this.money += money;
    }
}
