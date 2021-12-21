using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class Combinations : MonoBehaviour
{
    [SerializeField] TextAsset textAsset;
    [SerializeField] CombinationData[] combinationData;
    public CombinationData[] CombinationDatas { get { return combinationData; } }
    Stock stock;
    [SerializeField] ForSale forSale;

    private void Awake()
    {
        stock = FindObjectOfType<Stock>();
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
            for (int f = 0; f < 6; f++)
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

    public bool CompareWithData(string[] skewer)
    {
        foreach (CombinationData target in combinationData)
        {          
            int currentCombo = skewer.Length;
            if (currentCombo != target.combination.Count) { continue; }
            bool straightMatch = false;
            bool reverseMatch = false;

            for (int i = 0; i < currentCombo; i++)  //正向
            {
                if (target.combination[i].ToString() == skewer[i]) straightMatch = true;
                else { straightMatch = false; break; }   //任何一次出現False就結束               
            }
            for (int r = 0; r < currentCombo; r++)  //反向
            {
                if (target.combination[r].ToString() == skewer[currentCombo - 1 - r]) reverseMatch = true;
                else { reverseMatch = false; break; }   //任何一次出現False就結束      
            }

            if (straightMatch || reverseMatch)   
            {
                target.stock++;
                forSale.StartSelling();
                stock.ShowStock(target);
                return true;   //如果有找到MATCH就回傳true,沒有則進行下一次Foreach迴圈
            }
        }
        return false;   //直到所有foreach都跑完還是沒有return,出來到這裡就回傳false

    }

}
