using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public List<Sprite> blocks;
    public Vector2 blockPos;

    public SelectBlock selectBlock;

    public void DisplayBlock()
    {
        blockPrefab.GetComponent<SpriteRenderer>().sprite = selectBlock.selectedBlock;
        Instantiate(blockPrefab, blockPos, Quaternion.identity);
        selectBlock.ResetBlock();
    }
}