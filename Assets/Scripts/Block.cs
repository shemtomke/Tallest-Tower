using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float speed;
    public GameObject block;

    bool isStop = false;
    public bool isFall = false;
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
        Fall();
    }
    // you need to click on the screen, then the block will fall.
    void Fall()
    {
        if(Input.GetMouseButtonDown(0) && !isStop)
        {
            isFall = true;
        }

        if(isFall && !isStop)
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
            points.FallenBlockPoints();
            blockManager.selectBlock.gameObject.SetActive(true);
        }
    }
}