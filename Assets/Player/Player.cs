using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject followerPrefab;
    Queue<Vector2> locationQueues = new Queue<Vector2>();
    public Queue<Vector2> LocationQueues { get { return locationQueues; } }
    [SerializeField] float recordRateInSeconds = 1f;
    [SerializeField] int followerQueueLength = 10;
    //public int FollowerQueueLength { get { return followerQueueLength; } }
    Skewers skewers;
    [SerializeField] AudioClip[] stab;
    AudioSource audioSource;

    private void Awake()
    {
        skewers = FindObjectOfType<Skewers>();
        audioSource = GetComponent<AudioSource>();
    }


    public void CreateFollower(string food)
    {
        skewers.BroadcastMessage("CheckChildAmount",followerQueueLength, SendMessageOptions.DontRequireReceiver);
        GameObject follower = Instantiate(followerPrefab, transform.position, Quaternion.identity);        
        follower.GetComponent<Follower>().TagFood(food);
        follower.transform.parent = skewers.transform;   //使生成的follower以Skewers為parent
        follower.transform.SetAsFirstSibling();     //排在最前頭   
        StartCoroutine(skewers.GetComponent<Skewers>().MakeSkewers());
        int soundIndex = UnityEngine.Random.Range(0, stab.Length);
        audioSource.PlayOneShot(stab[soundIndex]);
    }

    private void Start()
    {
        locationQueues.Enqueue(transform.position);
        StartCoroutine(RecordLocation());
    }

    IEnumerator RecordLocation()
    {
        while(true)
        {
            if(locationQueues.Count <= followerQueueLength)
            {
                locationQueues.Enqueue(transform.position);
                yield return new WaitForSeconds(recordRateInSeconds);
            }
            else
            {
                locationQueues.Enqueue(transform.position);
                locationQueues.Dequeue();
                yield return new WaitForSeconds(recordRateInSeconds);
            }
        }
    }

    public Vector2 GetQueue(int foodIndex)
    {
        Vector2[] currentQueue = locationQueues.ToArray();
        
        return currentQueue[locationQueues.Count - 1 -foodIndex];    //取最新加入queue紀錄的座標，將follower擺在該位置        
    }

}
