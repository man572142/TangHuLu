using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetting : MonoBehaviour
{
    [SerializeField] SkewerList[] bonus;
    [SerializeField] int popularityToAdd = 20;
    List<string> levelChoice = new List<string>();
    [SerializeField] Combinations combination;
    CombinationData[] datas;

    private void Start()
    {
        datas = combination.CombinationDatas;

        if(bonus.Length <= 0 )
        {
            Debug.Log("Level Not Set!!");
        }

        foreach(SkewerList skewer in bonus)
        {
            foreach (CombinationData data in datas)
            {
                if(skewer.ToString() == data.title)
                {
                    levelChoice.Add(data.chineseTitle);  //把這關的Bonus記錄下來
                }
            }
        }
    }

    public int LevelBouns(CombinationData target)
    {
        foreach(SkewerList skewer in bonus)
        {
            if (skewer.ToString() == target.title)
            {
                
                Debug.Log("You choose the right combination");
                return popularityToAdd;   //如果目標有在這關的Bonus清單中,就可以獲得流行度加成
            }
        }
        return 0;
    }

    public string LevelRespond()
    {
        int ranIndex = Random.Range(0, levelChoice.Count);
        return levelChoice[ranIndex];
    }

}
