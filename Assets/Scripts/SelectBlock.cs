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
        if (currentBlock < (blockManager.blocks.Count - 1) && !isSelect) { currentBlock++; }
    }
    public void PreviousBlock()
    {
        if (currentBlock > 0 && !isSelect) { currentBlock--; }
    }
    public void ResetBlock()
    {
        selectedBlock = null;
        currentBlock = 0;
        isSelect = false;
    }
}
