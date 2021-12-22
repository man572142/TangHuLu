using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField] float timeBeforeTurnOff = 5f;
    [SerializeField] GameObject characters;
    [SerializeField] Text textUI = null;
    bool isCoroutine = false;
    bool cutCoroutine = false;
    bool secondCall = false;
    int previousIndex;


    public void Message(string message)
    {
        if(!isCoroutine) 
        StartCoroutine(CustomerThought(message));

    }
    IEnumerator CustomerThought(string message)
    {
        isCoroutine = true;
        if (cutCoroutine)
        {
            characters.transform.GetChild(previousIndex).gameObject.SetActive(false);
            secondCall = true;
        }
        
        int randomCharacter = Random.Range(0, characters.transform.childCount);  //隨機顯示顧客頭像
        previousIndex = randomCharacter;

        characters.transform.GetChild(randomCharacter).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
        textUI.text = message;
        yield return new WaitForSeconds(timeBeforeTurnOff - 2);
        //這段時間開始將能允許下一個提示訊息插隊進來
        cutCoroutine = true;  
        isCoroutine = false;

        yield return new WaitForSeconds(timeBeforeTurnOff - 3);
        //這段時間後就不允許插隊
        cutCoroutine = false;

        if(!secondCall)  //假如剛剛沒有訊息被插隊進來
        {
            transform.GetChild(0).gameObject.SetActive(false);
            characters.transform.GetChild(randomCharacter).gameObject.SetActive(false);
        }
        secondCall = false;
    }
}
