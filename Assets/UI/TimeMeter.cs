using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMeter : MonoBehaviour
{
    [SerializeField] float timeLimited = 120f;
    Slider slider;
    [SerializeField] Sales sales;
    [SerializeField] GameObject whiteBoard;
    [SerializeField] Money money;
    [SerializeField] GameObject player;
    [SerializeField] GameObject system;
    [SerializeField] GameObject endMusic;
    bool endGame = false;
    float timer = 0;
    [SerializeField] LevelSetting levelSetting;
    [SerializeField] Hint hint;

    bool quarter = false;
    bool half = false;
    bool threeQuarter = false;
    bool ten = false;
    bool isClosing = false;


    private void Start()
    {
        slider = GetComponent<Slider>();
        money = GetComponent<Money>();
        slider.value = 0f;
    }

    private void Update()
    {
        if (endGame) return;

        timer += Time.deltaTime;
        if(timer < timeLimited && timer > 0)
        {
            slider.value = timer / timeLimited;
        }
        else if(timer >= timeLimited)
        {
            money.FinalSettlement();
            whiteBoard.SetActive(true);
            player.gameObject.SetActive(false);
            system.gameObject.SetActive(false);
            endMusic.gameObject.SetActive(true);
            endGame = true;

        }

        if (timer >= timeLimited * 4 / 5 && !isClosing) { hint.Message("即將打烊"); isClosing = true; }
        else if (timer >= timeLimited * 3 / 4 && !threeQuarter) { sales.SalesRanking(); threeQuarter = true; }
        else if (timer >= timeLimited / 2 && !half) { hint.Message("營業時間已過了一半"); half = true; }
        else if (timer >= timeLimited / 4 && !quarter) { sales.SalesRanking(); quarter = true; }
        else if (timer >= timeLimited / 10 && !ten) 
        { 
            hint.Message("我想這裡的人應該會比較喜歡 " + levelSetting.LevelRespond() + " 或 " + levelSetting.LevelRespond());
            ten = true;
        }
    }

}
