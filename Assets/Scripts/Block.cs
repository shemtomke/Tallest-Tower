using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float speed;
    public GameObject block;

    bool isStop = false;
    public bool isFall = false;
    bool isStart = false;
    bool isMove = true;

    Vector3 initialPosition;

    Points points;
    BlockManager blockManager;
    private void Start()
    {
        points = FindObjectOfType<Points>();
        blockManager = FindObjectOfType<BlockManager>();
        initialPosition = transform.position;
    }
    private void Update()
    {
        Move();

        if (isFall)
        {
            Fall();
        }
    }
    void OnMouseDown()
    {
        if (!isStop)
        {
            isFall = true;
        }
    }
    // you need to click on the screen, then the block will fall.
    // Fall when isFall is true
    private void Fall()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
    // starts moving left and right
    void Move()
    {
        if(!isFall)
        {
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Base") || collision.gameObject.CompareTag("Block"))
        {
            Debug.Log("Stop Block");
            isStop = true;
            isFall = false;
            points.FallenBlockPoints();
            blockManager.selectBlock.gameObject.SetActive(true);
        }
    }
}