using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sales : MonoBehaviour
{
    List<CombinationData> soldRanking = new List<CombinationData>();
    public List<CombinationData> SoldRanking { get { return soldRanking; } }
    [SerializeField] Sold sold;

    public void Sale(CombinationData skewer)
    {
        bool adding = false;

        foreach(CombinationData item in soldRanking)
        {
            if (skewer == item)
            {
                adding = false;  //只要有一個一樣,就變成false並跳出foreach
                break;
            }
            else adding = true;
        }

        if (soldRanking.Count < 1) soldRanking.Add(skewer);
        else if (adding) soldRanking.Add(skewer);

        if (skewer.sold > 2 && skewer.stock < 3 )
        {
            FindObjectOfType<Hint>().Message(skewer.chineseTitle + "很好吃!你要不要再多做一點");
        }
        else if(skewer.sold > 4)
        {
            FindObjectOfType<Hint>().Message(skewer.chineseTitle + "真的太好吃了!我還會再來!");
        }
        else if(skewer.sold > 9)
        {
            FindObjectOfType<Hint>().Message("大家都在討論 " + skewer.chineseTitle + " 真的賣翻了!!");
        }
        SalesRanking();

    }

    public void SalesRanking()
    {
        if (soldRanking.Count < 2) 
        {
            return;
        }
        else
        {
            soldRanking.Sort((x, y) => { return -x.sold.CompareTo(y.sold); });
        }
        

        if(soldRanking.Count < 4)
        {
            FindObjectOfType<Hint>().Message("我肯定還會想再吃 " + soldRanking[0].chineseTitle);
        }
        else if(soldRanking.Count > 3)
        {
            FindObjectOfType<Hint>().Message(soldRanking[0].chineseTitle + " 或 " + soldRanking[1].chineseTitle + " 我都滿有興趣的!");
        }
        
    }


}
