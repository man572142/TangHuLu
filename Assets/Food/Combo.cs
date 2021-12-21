using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    [SerializeField] int combo = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            FindObjectOfType<Skewers>().SetCombo(combo);
            Destroy(gameObject);
        }
    }
}
