using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [Header("Food")]
    [SerializeField] Sprite strawberry;
    [SerializeField] Sprite tomato;
    [SerializeField] Sprite plum;
    PlayerMovement playerMovement;
    Player player;
    int foodIndex = 0;  
    [SerializeField] float speedUpLevel = 1f;
    Vector2 followTarget;
    

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            gameObject.GetComponentInParent<Skewers>().BroadcastSpeedChange(1f);
        }
    }



    public int GetSiblingIndex()
    {
        return transform.GetSiblingIndex();
    }

    private void Update()
    {
        foodIndex = transform.GetSiblingIndex();
        float movingSpeed = playerMovement.PlayerMovingSpeed * Time.deltaTime * speedUpLevel;  //跟Player同速度
        followTarget = player.GetQueue(foodIndex);   //告訴queue自己是幾號，並跟隨該座標
        transform.position = Vector2.MoveTowards(transform.position, followTarget, movingSpeed);
    }

    public void CheckChildAmount(int maxAmount)
    {
        if (transform.GetSiblingIndex() >= maxAmount)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {     
        speedUpLevel = speed;
        if(speed >1) GetComponent<SpriteRenderer>().color = Color.red;
        else if (speed == 1) GetComponent<SpriteRenderer>().color = Color.white; 

    }

    public void TagFood(string food)
    {
        if(food == "Strawberry")
        {
            GetComponent<SpriteRenderer>().sprite = strawberry;
            gameObject.tag = food;
            gameObject.name = food;
            
        }
        else if (food == "Tomato")
        {
            GetComponent<SpriteRenderer>().sprite = tomato;
            gameObject.tag = food;
            gameObject.name = food;
        }
        else if (food == "Plum")
        {
            GetComponent<SpriteRenderer>().sprite = plum;
            gameObject.tag = food;
            gameObject.name = food;
        }
    }

    public GameObject GetFood()
    {
        return gameObject;
    }

    public void PickFood()
    {
        Destroy(gameObject);
    }
}
