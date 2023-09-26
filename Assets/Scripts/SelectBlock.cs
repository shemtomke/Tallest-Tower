using System.Collections;
using System.Collections.Generic;
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
        this.gameObject.SetActive(false);

        blockManager.DisplayBlock();
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
        UpdateBlock();
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