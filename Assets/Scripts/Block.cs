using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float speed;
    public GameObject block;

    private bool hasCollided = false;
    public bool isDragging = false;
    bool isStop = false;
    public bool isFall = false;

    private Vector3 touchStartPos;
    private Vector3 objectStartPos;
    
    Points points;
    BlockManager blockManager;
    private void Start()
    {
        points = FindObjectOfType<Points>();
        blockManager = FindObjectOfType<BlockManager>();
    }
    private void Update()
    {
        if (isFall)
        {
            Fall();
        }
        else
        {
            Move();
        }
    }
    void OnMouseDown()
    {
        if (!isStop && !isDragging)
        {
            isFall = true; // Set the flag when done moving
        }
    }
    // you need to click on the screen, then the block will fall.
    // Fall when isFall is true
    private void Fall()
    {
        if (!isStop)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }
    // starts moving left and right
    void Move()
    {
        // Check for touch input
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                // Store the initial touch position and the object's current position
                touchStartPos = Camera.main.ScreenToWorldPoint(touch.position);
                objectStartPos = transform.position;

                // Check if the touch started on this GameObject
                RaycastHit2D hit = Physics2D.Raycast(touchStartPos, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                // Calculate the new position of the GameObject based on touch movement
                Vector3 touchCurrentPos = Camera.main.ScreenToWorldPoint(touch.position);
                Vector3 newPosition = new Vector3(touchCurrentPos.x - touchStartPos.x + objectStartPos.x, transform.position.y, transform.position.z);

                // Move the GameObject to the new position
                transform.position = newPosition;
            }
            else if (touch.phase == TouchPhase.Ended && isDragging)
            {
                isDragging = false;
            }
        }
        // Check for touch or mouse input
        if (Input.GetMouseButtonDown(0))
        {
            // Store the initial touch/mouse position and the object's current position
            touchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            objectStartPos = transform.position;

            // Check if the touch/mouse click is on this GameObject
            RaycastHit2D hit = Physics2D.Raycast(touchStartPos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
            }
        }
        // Check if the player is holding down the touch/mouse button and dragging
        if (isDragging && (Input.GetMouseButton(0) || Input.touchCount > 0))
        {
            Vector3 touchCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calculate the new position of the GameObject based on the X-axis movement
            Vector3 newPosition = new Vector3(touchCurrentPos.x - touchStartPos.x + objectStartPos.x, transform.position.y, transform.position.z);

            // Move the GameObject to the new position
            transform.position = newPosition;
        }
        // Release the GameObject when the touch/mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided && (collision.gameObject.CompareTag("Base") || collision.gameObject.CompareTag("Block")))
        {
            hasCollided = true;
            isStop = true;
            isFall = false;
            points.FallenBlockPoints();
            blockManager.selectBlock.gameObject.SetActive(true);
        }
    }
}