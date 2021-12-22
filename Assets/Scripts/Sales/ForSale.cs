using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForSale : MonoBehaviour
{
    [SerializeField] float maxSellTime = 30f;
    [SerializeField] float minSellTime = 5f;
    [SerializeField] Money money;
    [SerializeField] Stock stock;
    [SerializeField] Sales sales;
    bool isSelling = false;
    List<CombinationData> popularity = new List<CombinationData>();
    [SerializeField] int popularityBouns = 15;

    [SerializeField] int[] soldQuantity = { 45, 35, 13, 5, 2 };
    [SerializeField] LevelSetting levelSetting;

    public void StartSelling()
    {
        if(!isSelling)
        {
            StartCoroutine(Selling());
        }
    }



    IEnumerator Selling()
    {
        while(true)
        {
            isSelling = true;
            if (stock.Combinations.Count <= 0) { isSelling = false; break; }
            yield return new WaitForSeconds(Random.Range(minSellTime, maxSellTime));


            popularity = stock.Combinations;   //將現有的存貨依流行度排行
            popularity.Sort((x, y) => { return -x.popularity.CompareTo(y.popularity); });

            int sell = SellQuantity();   //決定賣出數量
            
            for(int p = 0; p < sell; p++)
            {
                int pick = PickByPopularity();
                if (pick < 0) continue;  //如果這裡沒有選到會少賣一支,但沒關係 

                CombinationData pickSkewer = popularity[pick];  //依流行度選取
                StartCoroutine(money.Income(pickSkewer.price)); ;
                pickSkewer.stock--;
                if (pickSkewer.stock <= 0) popularity.Remove(popularity[pick]);   //如果賣完了,就將其從popularity清單中移除,下次迴圈pick的時候就不會又選到

                pickSkewer.sold++;
                stock.SoldStock();
                sales.Sale(pickSkewer);
                Debug.Log("SOLD : " + pickSkewer.title);
            }   
        }
    }

    private int SellQuantity()
    {
        int randomIndex = Random.Range(0, 100);
        int soldCount = 0;
        for (int i = 0; i < 5; i++)
        {
            if (randomIndex > soldQuantity[i] + soldCount) { soldCount += soldQuantity[i]; }
            else return i+1; //實際上要return的是數量,而不是index,故+1
        }
        Debug.LogError("Sold Quantity have wrong Probability");
        return 0;
    }

    private int PickByPopularity()
    {        
        int popBouns = 0;
        

        if (popularity.Count < 1) return -1; //如果都沒有商品了,就直接retrun,這樣在selling的for迴圈就會一直被略過
        else if(popularity.Count < 4 )  //如果種類不到4支,直接提升各項的受歡迎度
        {
            popBouns = popularityBouns;
            Debug.Log("popBouns");
        }

        int randomIndex = Random.Range(0, 100);
        int popCount = 0;

        for (int i = 0; i < popularity.Count; i++)
        {
            if (randomIndex > popularity[i].popularity + popCount + popBouns + levelSetting.LevelBouns(popularity[i]))
            {
                popCount += popularity[i].popularity + popBouns;  //超過100會導致最後幾名都選不到,但因為Popularity已由高排到低,那幾名本來就比較冷門,可解釋為排擠效應
            }
            else
            {
                randomIndex = i;
                return randomIndex;
            }         
        }
        //如果迴圈都跑完了,還是沒有return,代表現存商品的流行度總合一定不到100,也就是都不夠受歡迎
        if (popularity.Count < 4) FindObjectOfType<Hint>().Message("只有這幾樣啊? 能不能再做" + levelSetting.LevelRespond() + " 或 " + levelSetting.LevelRespond());
        else FindObjectOfType<Hint>().Message("都沒有我喜歡的口味,能不能多做點..." + levelSetting.LevelRespond());

        return -1;  //沒有任何糖葫蘆會被賣出
    }
}
