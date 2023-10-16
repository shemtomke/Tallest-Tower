using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float speed;
    public GameObject collidingBlock, currentBlock;

    private bool hasCollided = false;
    public bool isDragging = false;
    bool isStop = false;
    public bool isFall = false;

    private Vector3 touchStartPos;
    private Vector3 objectStartPos;
    
    Points points;
    BlockManager blockManager;
    BlockPropertiesStatus blockPropertiesStatus;

    private void Start()
    {
        points = FindObjectOfType<Points>();
        blockManager = FindObjectOfType<BlockManager>();
        blockPropertiesStatus = FindObjectOfType<BlockPropertiesStatus>();

        currentBlock = this.gameObject;
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

        if (collidingBlock != null)
        {
            var colllidingSprite = collidingBlock.GetComponent<Sprite>();

            if (colllidingSprite == blockManager.baseBlock.currentBlock)
            {
                blockPropertiesStatus.collidingBlockProperty = blockManager.baseBlock;
            }
            else
            {
                for (int i = 0; i < blockManager.blocks.Count; i++)
                {
                    if (colllidingSprite == blockManager.blocks[i].currentBlock)
                    {
                        blockPropertiesStatus.collidingBlockProperty = blockManager.blocks[i];
                    }
                }
            }
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
        float distance = 0;

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
        if (Input.GetMouseButton(0))
        {
            Vector3 touchCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Calculate the new position of the GameObject based on the X-axis movement
            Vector3 newPosition = new Vector3(touchCurrentPos.x - touchStartPos.x + objectStartPos.x, transform.position.y, transform.position.z);

            // Move the GameObject to the new position
            transform.position = newPosition;
            distance = Mathf.Abs(touchCurrentPos.x - touchStartPos.x);

            // Check if the distance is greater than 0.1 (threshold)
            if (distance >= 0.01f) { isDragging = true; }
            else { isDragging = false; }
        }
        // Release the GameObject when the touch/mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                // Handle drag end
                isDragging = false;
            }
            else
            {
                // Handle tap
                if (!isStop && !isDragging)
                {
                    isFall = true; // Set the flag when done moving
                }
            }
        }

        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

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
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector3 touchCurrentPos = Camera.main.ScreenToWorldPoint(touch.position);
                // Calculate the new position of the GameObject based on the X-axis movement
                Vector3 newPosition = new Vector3(touchCurrentPos.x - touchStartPos.x + objectStartPos.x, transform.position.y, transform.position.z);

                // Move the GameObject to the new position
                transform.position = newPosition;
                distance = Mathf.Abs(touchCurrentPos.x - touchStartPos.x);

                // Check if the distance is greater than 0.1 (threshold)
                if (distance >= 0.01f) { isDragging = true; }
                else { isDragging = false; }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (isDragging)
                {
                    // Handle drag end
                    isDragging = false;
                }
                else
                {
                    // Handle tap
                    if (!isStop && !isDragging)
                    {
                        isFall = true; // Set the flag when done moving
                    }
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided && (collision.gameObject.CompareTag("Base") || collision.gameObject.CompareTag("Block")))
        {
            collidingBlock = collision.gameObject;
            hasCollided = true;
            isStop = true;
            isFall = false;
            points.FallenBlockPoints();
            CheckMatchingBlock();
        }
    }
    void CheckMatchingBlock()
    {
        if(blockPropertiesStatus.collidingBlockProperty != null)
        {
            var thisSprite = GetComponent<SpriteRenderer>().sprite;
            for (int i = 0; i < blockPropertiesStatus.collidingBlockProperty.matchingBlocks.Count; i++)
            {
                if (thisSprite == blockPropertiesStatus.collidingBlockProperty.matchingBlocks[i])
                {
                    Debug.Log("Matching!");
                    blockManager.selectBlock.gameObject.SetActive(true);
                    this.GetComponent<Block>().enabled = false;
                }
                else
                {
                    Debug.Log("Not Matching!");
                    blockManager.selectBlock.gameObject.SetActive(false);
                }
            }
        }
    }
    IEnumerator StartCheckingMatch()
    {
        yield return new WaitForSeconds(1);

        CheckMatchingBlock();

        yield return new WaitForSeconds(1);

        blockManager.selectBlock.gameObject.SetActive(true);
        this.GetComponent<Block>().enabled = false;
    }
}