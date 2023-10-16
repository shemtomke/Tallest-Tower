using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public List<BlockProperties> blocks;
    public BlockProperties baseBlock;
    public Vector2 blockPos;

    public SelectBlock selectBlock;
    public GameObject currentActiveBlock;

    public void DisplayBlock()
    {
        blockPrefab.GetComponent<SpriteRenderer>().sprite = selectBlock.selectedBlock;
        currentActiveBlock = Instantiate(blockPrefab, blockPos, Quaternion.identity);
        selectBlock.ResetBlock();
    }
}