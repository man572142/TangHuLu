using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skewers : MonoBehaviour
{
    [SerializeField] int combo = 3;
    public int Combo { get { return combo; } }
    List<GameObject> foodList = new List<GameObject>();
    GameObject[,] set;
    int combAmount;
    [SerializeField] Combinations Combination;
    [SerializeField] Money money;
    [SerializeField] ComboUI comboUI;


    public IEnumerator MakeSkewers()
    {
        yield return new WaitForEndOfFrame();
        if (transform.childCount >= combo)
        {
            combAmount = transform.childCount - combo + 1;
            set = new GameObject[combAmount, combo];
            CreateFoodList();  //**實測發現CreateFoodList會比Destory第11個食物還要早執行,暫時先讓他往後一個frame
            FindEveryCombination();           
        }
    }

    private void CreateFoodList()
    {
        foodList.Clear();  //創建一個新list前先把舊的清除
        foreach (Transform follower in transform)
        {
            GameObject food = follower.GetComponent<Follower>().GetFood();
            foodList.Add(food);
        }
    }

    private void FindEveryCombination()
    {
        for (int i = 0; i < combAmount; i++)    //先取第i串
        {
            for (int num = 0; num < combo; num++)  //以i為第一顆,再往後取combo所需的數量,foodIndex代表foodList中的編號,num為該[串]當中的編號
            {
                int foodIndex = num + i;
                set[i, num] = foodList[foodIndex];
            }
            MakeSkewer(i);
        }
    }

    private void MakeSkewer(int i)
    {
        bool skewerMatch = false;
        string[] setToCompare = new string[combo];

        for (int a = 0; a < combo; a++)
        {
            setToCompare[a] = set[i, a].tag;
        }
        skewerMatch = Combination.GetComponent<Combinations>().CompareWithData(setToCompare);
        if (skewerMatch)    
        {
            for (int b = 0; b < combo; b++)
            {
                set[i, b].GetComponent<Follower>().PickFood();
                BroadcastSpeedChange(2f);
            }

        }
    }


    public void BroadcastSpeedChange(float speed)
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Follower>().SetSpeed(speed);
        }
    }

    public void SetCombo(int newCombo)
    {
        combo = newCombo;
        comboUI.SetComboUI(newCombo);
    }


}
