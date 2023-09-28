using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectBlock : MonoBehaviour
{
    public int currentBlock = 0;
    public BlockManager blockManager;
    public bool isSelect = false;
    public Sprite selectedBlock;

    private void OnMouseDown()
    {
        isSelect = true;
        UpdateBlock();
        selectedBlock = this.GetComponent<SpriteRenderer>().sprite;
    }
    private void OnMouseUp()
    {
        blockManager.DisplayBlock();
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        blockManager.currentActiveBlock = null;
    }
    private void Start()
    {
        UpdateBlock();
    }
    private void Update()
    {
        TouchInput();
        UpdateBlock();
    }
    void TouchInput()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch (you can handle multiple touches if needed)

            if (touch.phase == TouchPhase.Began)
            {
                // Touch began, similar to OnMouseDown
                isSelect = true;
                UpdateBlock();
                selectedBlock = GetComponent<SpriteRenderer>().sprite;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Touch ended, similar to OnMouseUp
                blockManager.DisplayBlock();
                gameObject.SetActive(false);
            }
        }
    }
    void UpdateBlock()
    {
        this.GetComponent<SpriteRenderer>().sprite = blockManager.blocks[currentBlock];
    }
    public void NextBlock()
    {
        if (!isSelect)
        {
            currentBlock = (currentBlock + 1) % blockManager.blocks.Count;
        }
    }
    public void PreviousBlock()
    {
        if (!isSelect)
        {
            // Add blockManager.blocks.Count to handle negative index cases and loop around
            currentBlock = (currentBlock - 1 + blockManager.blocks.Count) % blockManager.blocks.Count;
        }
    }
    public void ResetBlock()
    {
        selectedBlock = null;
        currentBlock = 0;
        isSelect = false;
    }
}