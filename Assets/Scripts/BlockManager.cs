using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public List<BlockProperties> blocks;
    public Vector2 blockPos;

    public SelectBlock selectBlock;
    public GameObject currentActiveBlock;

    CameraOffset cameraOffset;
    GameManager gameManager;
    private void Start()
    {
        cameraOffset = FindObjectOfType<CameraOffset>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        DestroyBlocks();
    }
    public void DisplayBlock()
    {
        blockPrefab.GetComponent<SpriteRenderer>().sprite = selectBlock.selectedBlock;
        currentActiveBlock = Instantiate(blockPrefab, blockPos, Quaternion.identity);
        selectBlock.ResetBlock();
    }
    public void DestroyBlocks()
    {
        if(gameManager.isGameOver)
        {
            for (int i = 0; i < cameraOffset.blocks.Count; i++)
            {
                Destroy(cameraOffset.blocks[i]);
            }
            cameraOffset.blocks.Clear();
        }
    }
}