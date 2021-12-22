using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombinationData
{
    public string title;
    public string chineseTitle;
    public List<FoodType> combination = new List<FoodType>();
    public int price;
    [Range(0, 100)]
    public int popularity;
    public int stock;
    public int sold;
}
