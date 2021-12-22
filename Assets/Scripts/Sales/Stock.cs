using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stock : MonoBehaviour
{
    [SerializeField] int maxLeaderBoardShowing = 5;
    /// <summary>現貨糖葫蘆</summary>
    List<CombinationData> combinations = new List<CombinationData>();
    public List<CombinationData> Combinations { get { return combinations; } }
    
    public void ShowStock(CombinationData newFlavor) 
    {
        //確認整個List是否已有相同口味,有的話需確認排名,更新顯示位置
        for (int i = 0; i < combinations.Count; i++) 
        {
            CombinationData inStock = combinations[i];
            if(inStock.title == newFlavor.title)  
            {
                CheckRanking();
                return;
            }
        }

        if(combinations.Count < 5 )  //如果現貨無此新口味，並且種類不到五個,直接加入清單並排入榜上
        {
            combinations.Add(newFlavor);
            SetPosition();
            return;
        }

        //榜單已滿,直接加入List(不需要SetPosition(阿就滿了阿)也不需要CheckRanking,因為新加入只會有1個存貨,不可能比得過榜上的)
        combinations.Add(newFlavor);

    }
    public void SoldStock()
    {
        for(int i=0;i<combinations.Count;i++)
        {
            if(combinations[i].stock <= 0)
            {
                combinations.Remove(combinations[i]);
                SetPosition();               
            }
            else CheckRanking();       
        }

    }

    private void CheckRanking()
    {
        combinations.Sort((x,y) => { return -x.stock.CompareTo(y.stock); });
        SetPosition();
    }

    private void SetPosition()
    {
        int onLeaderBoard = 0;
        if (combinations.Count < maxLeaderBoardShowing)
        {
            onLeaderBoard = combinations.Count;
            for(int c = maxLeaderBoardShowing-1; c > onLeaderBoard-1;c--)  //如果有貨的數量比榜位還少,把沒有用的榜關掉
            {
                transform.GetChild(c).gameObject.SetActive(false);
                transform.GetChild(c).GetComponent<LeaderBoard>().flavor = null;
            }
        }
        else onLeaderBoard = maxLeaderBoardShowing;

        for(int i = 0; i < onLeaderBoard; i++)
        {      
            LeaderBoard leader = transform.GetChild(i).GetComponent<LeaderBoard>();
            leader.gameObject.SetActive(true);
            leader.flavor = combinations[i];
            leader.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = leader.flavor.stock.ToString();
            leader.gameObject.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(leader.flavor.title);
        }
    }

}
