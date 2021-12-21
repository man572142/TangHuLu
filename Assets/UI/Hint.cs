using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField] float timeBeforeTurnOff = 5f;
    [SerializeField] GameObject characters;
    bool isCoroutine = false;
    bool stillCoroutine = false;
    bool secondCall = false;
    int previousIndex;


    public void Message(string message)
    {
        if(!isCoroutine) 
        StartCoroutine(CustomThought(message));

    }
    IEnumerator CustomThought(string message)
    {
        isCoroutine = true;
        if (stillCoroutine)
        {
            characters.transform.GetChild(previousIndex).gameObject.SetActive(false);
            secondCall = true;
        }
        
        int randomCharacter = Random.Range(0, characters.transform.childCount);
        previousIndex = randomCharacter;

        characters.transform.GetChild(randomCharacter).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponentInChildren<Text>().text = message;
        yield return new WaitForSeconds(timeBeforeTurnOff - 2);

        stillCoroutine = true;
        isCoroutine = false;

        yield return new WaitForSeconds(timeBeforeTurnOff - 3);
        stillCoroutine = false;
        if(!secondCall)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            characters.transform.GetChild(randomCharacter).gameObject.SetActive(false);
        }
        secondCall = false;
    }
}
