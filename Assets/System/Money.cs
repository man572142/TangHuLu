using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [SerializeField] int startingMoney = 500;
    int currentMoney;
    TextMeshProUGUI text;
    [SerializeField] RectTransform[] cash;
    [SerializeField] Vector2 cashPosition = new Vector2(-100, 0);
    [SerializeField] float timeBeforeCashDestroy = 2f;
    int totalIncome = 0;
    public int TotalIncome { get { return totalIncome; } }
    int totalExpense = 0;
    public int TotalExpense { get { return totalExpense; } }

    [SerializeField] Text incomeNumber;
    [SerializeField] Text expenseNumber;
    [SerializeField] Text realIncomeNumber;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {    
        currentMoney = startingMoney;
        text.text = currentMoney.ToString();
    }

    public int CurrentMoney()
    {
        return currentMoney;
    }

    public IEnumerator Income(int earn)
    {
        
        RectTransform newCash = Instantiate(cash[Random.Range(0, cash.Length)], transform, false);
        newCash.anchoredPosition = cashPosition;
        Destroy(newCash.gameObject, timeBeforeCashDestroy);
        currentMoney += earn;
        totalIncome += earn;
        yield return new WaitForSeconds(timeBeforeCashDestroy);        
        text.text = currentMoney.ToString();
    }

    public void Expense(int cost)
    {
        currentMoney -= cost;
        totalExpense += cost;
        text.text = currentMoney.ToString();
    }

    public void FinalSettlement()
    {
        incomeNumber.text = totalIncome.ToString();
        expenseNumber.text = totalExpense.ToString();
        realIncomeNumber.text = (totalIncome - totalExpense).ToString();
        if(totalIncome-totalExpense <= 0)
        {
            realIncomeNumber.color = Color.red;
        }
    }
}
