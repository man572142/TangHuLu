using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sold : MonoBehaviour
{
    [SerializeField] float showRate = 0.5f;
    [SerializeField] Sales sales;
    List<CombinationData> soldRanking = new List<CombinationData>();
    [SerializeField] Text textPrefab;

    private void OnEnable()
    {
        StartCoroutine(ShowRanking());
    }

    IEnumerator ShowRanking()
    {
        soldRanking = sales.SoldRanking;
        int count = 0;
        if(soldRanking.Count > 5) { count = 5; } 
        else { count = soldRanking.Count; }
        for (int i = 0; i < count ;i++)
        {
            yield return new WaitForSeconds(showRate);
            Text leader = Instantiate(textPrefab,transform,false);
            leader.text = soldRanking[i].title + " : " + soldRanking[i].sold;
        }    
        
    }
}
