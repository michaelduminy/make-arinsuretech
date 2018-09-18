using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    private static bool isGameOver = false;

	void Start () {
        GameState.Initialise();
	}
	
	void FixedUpdate () {
        if (!isGameOver && GameState.Tick()){
            // game is over
            GameOver();
        } else {
            var timeObject = GameObject.Find("txt_score");
            var timeTxt = timeObject.GetComponent<Text>();
            if (timeTxt != null)
            {
                timeTxt.text = "Score: " + GameState.GetScore();
            }

            var moneyObject = GameObject.Find("txt_money");
            var moneyTxt = moneyObject.GetComponent<Text>();
            if (moneyTxt != null)
            {
                moneyTxt.text = "Money: " + GameState.GetMoney();
            }
        }
	}

    void GameOver() {
        isGameOver = true;
        var go = GameObject.Find("txt_score");
        var txt = go.GetComponent<Text>();
        if (txt != null)
        {
            txt.text = "Final Score: " + GameState.GetScore();
        }
    }
}
