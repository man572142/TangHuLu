using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class Combinations : MonoBehaviour
{
    /// <summary>CSV檔的食物資料庫</summary>
    [SerializeField] TextAsset textAsset;
    [SerializeField] CombinationData[] combinationData;
    public CombinationData[] CombinationDatas { get { return combinationData; } }
    [SerializeField] Stock stock;
    [SerializeField] ForSale forSale;

    private void Awake()
    {
        ReadCSV();
    }
    private void Start()
    {
        
    }

    private void ReadCSV()
    {

        string[] data = textAsset.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.None);
        int tableSize = data.Length / 10 - 1;    //10是在csv檔裡的欄位數,tableSize就是列數(-1是扣掉第一列)
        combinationData = new CombinationData[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            combinationData[i] = new CombinationData();
            combinationData[i].title = data[10 * (i + 1)];  //從data第10格開始(第一列不算)
            combinationData[i].chineseTitle = data[10 * (i + 1) + 1];
            for (int f = 0; f < 6; f++)  //一串最多6顆水果
            {
                string foodString = data[10 * (i + 1) + 2 + f];
                if (foodString.Length != 0)
                {
                    FoodType food = (FoodType)System.Enum.Parse(typeof(FoodType), foodString);  //把csv的string轉為FoodType的enum
                    combinationData[i].combination.Add(food);
                }

            }
            combinationData[i].price = int.Parse(data[10 * (i + 1) + 8]);
            combinationData[i].popularity = int.Parse(data[10 * (i + 1) + 9]);
        }
    }

    public bool CompareWithData(string[] skewer)  //陣列的每一個值都是一個水果名
    {
        foreach (CombinationData target in combinationData)
        {          
            int currentCombo = skewer.Length;
            if (currentCombo != target.combination.Count) { continue; }
            bool straightMatch = false;
            bool reverseMatch = false;

            //配方不分方向，如:[草莓草莓番茄番茄] 與 [番茄番茄草莓草莓] 視為同一種,都可以成一串)

            //正向
            for (int i = 0; i < currentCombo; i++)  
            {
                if (target.combination[i].ToString() == skewer[i]) straightMatch = true;
                else { straightMatch = false; break; }   //任何一次出現False就結束迴圈             
            }

            //反向
            for (int r = 0; r < currentCombo; r++)  
            {
                if (target.combination[r].ToString() == skewer[currentCombo - 1 - r]) reverseMatch = true;
                else { reverseMatch = false; break; }   //任何一次出現False就結束迴圈      
            }

            if (straightMatch || reverseMatch)   //上述正反向迴圈,只要其中一個有全部都true,即成功成串
            {
                target.stock++;
                forSale.StartSelling();
                stock.ShowStock(target);
                return true;
            }
        }
        return false;   //直到所有foreach都跑完還是沒有return,出來到這裡就回傳false

    }

}
