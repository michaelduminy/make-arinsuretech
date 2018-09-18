using System;
using UnityEngine;
using UnityEngine.UI;

public static class GameState {

    private static float score = 0;
    private static float money = 100;
    private static float decreaseRate = 1;

    public static void Initialise() {
        score = 0;
        money = 100;
    }

    public static bool Tick() {
        DecreaseMoney();
        IncreaseScore();

        return money <= 0;
    }

    public static void DecreaseMoney()
    {
        money -= Time.deltaTime * decreaseRate;
    }

    public static void IncreaseMoney(int amount) {
        money += amount;
    }

    public static int GetScore() {
        return (int)score;
    }

    public static int GetMoney()
    {
        return (int)money;
    }

    public static void IncreaseScore() {
        score += Time.deltaTime;
    }

    public static void DecreaseRate(float multiplyer) {
        decreaseRate *= multiplyer;
    }
}
