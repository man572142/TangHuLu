using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUI : MonoBehaviour
{
    Text text;
    [SerializeField] Skewers skewers;

    private void Start()
    {
        GetComponent<Text>().text = "x" + skewers.Combo.ToString();
    }

    public void SetComboUI(int newCombo)
    {
        GetComponent<Text>().text = "x" + newCombo.ToString();
    }

}
