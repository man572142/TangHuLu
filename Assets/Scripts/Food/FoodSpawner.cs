using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [Header("Map Size")]
    [SerializeField] float xLength;
    [SerializeField] float yLength;

    [SerializeField] float spawnRate = 2f;
    [SerializeField] GameObject[] foodPrefab;
    [SerializeField] GameObject[] funtionalFood;
    [SerializeField] float funRate = 15f;
    /// <summary>食物種類數 </summary>
    int foodLength;
    /// <summary>功能性食物種類數 </summary>
    int funLength;
    /// <summary>整個場上最多可容納多少食物 </summary>
    [SerializeField] int maxFoodAmount = 30;
    bool isSpawning = true;
    bool isSpawningFunFood = true;
    [SerializeField] Player player;
    [SerializeField] float spawnMinimumDistance = 2f;
    [SerializeField] Skewers skewers;
 
    void Start()
    {
        foodLength = foodPrefab.Length;
        funLength = funtionalFood.Length;
        StartCoroutine(Spawner());
        StartCoroutine(FuntionalSpawner());
    }

    private void Update()
    {
        if(transform.childCount <= maxFoodAmount && !isSpawning)   //food低於maxFoodAmount並且沒有Spawner正在進行的話
        {
            StartCoroutine(Spawner());
        }
        if(transform.childCount <= maxFoodAmount && !isSpawningFunFood)
        {
            StartCoroutine(FuntionalSpawner());
        }
    }


    IEnumerator Spawner()
    {
        while(transform.childCount <= maxFoodAmount)
        {
            isSpawning = true;
            int foodIndex = Random.Range(0, foodLength);  //random不包含最大值
            float newX = Random.Range(-xLength / 2, xLength / 2);
            float newY = Random.Range(-yLength / 2, yLength / 2);
            Vector2 position = new Vector2(newX, newY);
            if(Vector2.Distance(position,player.transform.position) < spawnMinimumDistance) { continue; }    //如果新生成的位置離player太近,則結束該輪迴圈並再重來一次

            GameObject newFood = Instantiate(foodPrefab[foodIndex], position, Quaternion.identity);
            newFood.transform.parent = transform;
            yield return new WaitForSeconds(spawnRate);

        }
        isSpawning = false;   //food超過maxFoodAmount停止整個while迴圈，交由Update繼續追蹤food數量
    }

    IEnumerator FuntionalSpawner()
    {

        while (transform.childCount <= maxFoodAmount)
        {
            isSpawningFunFood = true;
            yield return new WaitForSeconds(funRate);            
            
            int index = RandomWithException(funLength, skewers.GetComponent<Skewers>().Combo - 3);   //最小的Combo是3,其index是0
            if (index == -1) { Debug.LogError("FunFoodSpawner Exception"); break; }

            float newX = Random.Range(-xLength / 2, xLength / 2);
            float newY = Random.Range(-yLength / 2, yLength / 2);
            Vector2 position = new Vector2(newX, newY);
            if (Vector2.Distance(position, player.transform.position) < spawnMinimumDistance) { continue; }    //如果新生成的位置離player太近,則結束該輪迴圈並再重來一次

            GameObject funFood = Instantiate(funtionalFood[index], position, Quaternion.identity);
            funFood.transform.parent = transform;           
        }
        isSpawningFunFood = false;
    }

    int RandomWithException(int max,int except)  //隨機生成一個和現在combo不相同的數
    {
        if (except >= max || except < 0) return -1;

        while(true)
        {
            int index = Random.Range(0, max);
            if(index != except)
            {
                return index;
            }
        }
        
        
    }
}
