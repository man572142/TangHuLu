using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{



    [SerializeField] FoodType foodType;
    [SerializeField] int cost = 5;
     Money money;

    private void Awake()
    {
        money = FindObjectOfType<Money>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() == null || money.CurrentMoney() <= 0) { return; }  //如果碰到的不是Player,或者沒有錢了,就return

        collision.GetComponent<Player>().CreateFollower(foodType.ToString());
        money.Expense(cost);
        Destroy(gameObject);
    }



    public string GetFoodType()
    {
        Destroy(gameObject);
        return foodType.ToString();
        
    }

}
