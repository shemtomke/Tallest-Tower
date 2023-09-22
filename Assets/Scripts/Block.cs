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

    private Vector3 touchStartPos;
    private Vector3 objectStartPos;
    private bool isDragging = false;

    Points points;
    BlockManager blockManager;
    private void Start()
    {
        points = FindObjectOfType<Points>();
        blockManager = FindObjectOfType<BlockManager>();
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
        if (!isStop && !isDragging)
        {
            isFall = true;
        }
    }
    // you need to click on the screen, then the block will fall.
    // Fall when isFall is true
    private void Fall()
    {
        if (isFall && !isStop)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }
    // starts moving left and right
    void Move()
    {
        if (!isFall)
        {
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Vector3 inputPos = Input.GetMouseButtonDown(0)
                    ? Input.mousePosition
                    : (Vector3)Input.GetTouch(0).position;

                Ray ray = Camera.main.ScreenPointToRay(inputPos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    StartDragging(inputPos);
                }
            }

            if (isDragging)
            {
                Vector3 inputPos = Input.GetMouseButton(0)
                    ? Input.mousePosition
                    : (Vector3)Input.GetTouch(0).position;

                Vector3 touchPos = Camera.main.ScreenToWorldPoint(inputPos);
                touchPos.y = objectStartPos.y; // Maintain the same Y position
                touchPos.z = objectStartPos.z; // Maintain the same Z position
                transform.position = new Vector3(touchPos.x, transform.position.y, transform.position.z);

                if (!Input.GetMouseButton(0) && (Input.touchCount == 0 || Input.GetTouch(0).phase == TouchPhase.Ended))
                {
                    StopDragging();
                }
            }
        }
    }
    private void StartDragging(Vector3 inputPos)
    {
        isDragging = true;
        touchStartPos = Camera.main.ScreenToWorldPoint(inputPos);
        touchStartPos.z = transform.position.z;
        objectStartPos = transform.position;
    }
    private void StopDragging()
    {
        isDragging = false;
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