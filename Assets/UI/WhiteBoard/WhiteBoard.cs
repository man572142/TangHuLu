using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBoard : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ActiveObjects());
    }

    IEnumerator ActiveObjects()
    {
        yield return new WaitForSeconds(1);
        transform.GetChild(0).gameObject.SetActive(true);  //Closed
        yield return new WaitForSeconds(2);
        transform.GetChild(1).gameObject.SetActive(true); //Sold
        yield return new WaitForSeconds(3);
        transform.GetChild(2).gameObject.SetActive(true); //Expense
        yield return new WaitForSeconds(1);
        transform.GetChild(3).gameObject.SetActive(true); //Income
        yield return new WaitForSeconds(1);
        transform.GetChild(4).gameObject.SetActive(true); //RealIncome
        yield return new WaitForSeconds(2);
        transform.GetChild(5).gameObject.SetActive(true); //Star
        yield return new WaitForSeconds(1);
        transform.GetChild(6).gameObject.SetActive(true); //Button
    }
}
