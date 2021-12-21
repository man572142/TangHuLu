using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float startDelay = 5f;
    [SerializeField] [Range(5f, 10f)] float movingSpeed = 7f;
    [SerializeField] [Range(2f, 5f)] float rotateSpeed = 3f;
    [SerializeField] float faintTime = 2f;
    [SerializeField] float bumpDrag = 2f;
    [SerializeField] AudioClip crashWall;
    public float PlayerMovingSpeed { get { return movingSpeed; } }

    Rigidbody2D myRigidbody;
    bool isTouchingBoundary = false;
    AudioSource audioSource;
    Sprite originalSprite;
    [SerializeField] Sprite crashSprite;
    bool toMove = false;


    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        originalSprite = GetComponent<SpriteRenderer>().sprite;
        Invoke("ReadyForMoving", startDelay);
    }

    void ReadyForMoving()
    {
        toMove = true;
    }

    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape))   //回到選單
        {
            SceneManager.LoadScene(1);
        }


    }
    private void FixedUpdate()
    {
        if (toMove)
        {
            Moving();
        }

        RotationControl();

    }

    private void Moving()
    {
        if(!isTouchingBoundary)
        {
            myRigidbody.velocity = transform.up * movingSpeed * 50 * Time.fixedDeltaTime; //永遠向上(Y軸)前進
            
        }
        
    }

    public void IsTouhingBoundary()
    {
        audioSource.PlayOneShot(crashWall);
        GetComponent<SpriteRenderer>().sprite = crashSprite;
        isTouchingBoundary = true;
        myRigidbody.drag = bumpDrag;
        Invoke("WakeAndGo", faintTime);
    }

    public void WakeAndGo()
    {
        isTouchingBoundary = false;
        myRigidbody.drag = 0;
        GetComponent<SpriteRenderer>().sprite = originalSprite;
    }

    private void RotationControl()
    {
        if (Input.GetKey(KeyCode.X))   //順時針
        {
            myRigidbody.rotation -= rotateSpeed * 50 * Time.fixedDeltaTime;
            Moving();    //修正方向後要重新指定velocity
        }
        else if (Input.GetKey(KeyCode.Z))  //逆時針
        {
            myRigidbody.rotation += rotateSpeed * 50 * Time.fixedDeltaTime;
            Moving();    //修正方向後要重新指定velocity
        }
        
    }


}
