using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] Image image;
    [SerializeField] Text text;
    [SerializeField][TextArea] string information;


    public void SceneLoad()
    {
        SceneManager.LoadScene(sceneName);
    }


    public void ShowInformation()
    {
        image.sprite = Resources.Load<Sprite>("Menu/" + sceneName);
        text.text = information;
    }

    public void ShowTutorial()
    {
        image.sprite = Resources.Load<Sprite>("Menu/Tutorial");
        text.text = "";
    }

    public void ShowGuide()
    {
        image.sprite = Resources.Load<Sprite>("Menu/Guide");
        text.text = "";
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
