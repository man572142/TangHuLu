using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    bool startLoading = false;
    [SerializeField] float loadDelay = 5.5f;
    

    void Update()
    {
        if(Input.anyKey && !startLoading)
        {
            StartCoroutine(StartToLoad());
        }
    }

    IEnumerator StartToLoad()
    {
        GetComponent<Animator>().SetBool("Fade", true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(1);
    }

}
