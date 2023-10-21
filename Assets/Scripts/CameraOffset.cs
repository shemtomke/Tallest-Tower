using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraOffset : MonoBehaviour
{
    public Camera mainCamera;
    public float camOffset, selectionBlockOffset, centerY;
    public Vector3 target, selectionBlockTarget;
    public List<GameObject> blocks = new List<GameObject>();
    public GameObject centreObj;

    SelectBlock selectBlock;
    BlockManager blockManager;
    private void Start()
    {
        selectBlock = FindObjectOfType<SelectBlock>();
        blockManager = FindObjectOfType<BlockManager>();
        target = mainCamera.ScreenToViewportPoint(transform.position);
    }
    private void FixedUpdate()
    {
        centerY = mainCamera.transform.position.y; // Calculate the center Y coordinate in pixels
        // Get the Y position of the GameObject.
        float highestYPosition = float.MinValue; // Initialize to the smallest possible value

        for (int i = 0; i < blocks.Count; i++)
        {
            float currentY = blocks[i].transform.position.y;

            if (currentY > highestYPosition)
            {
                highestYPosition = currentY;
                centreObj = blocks[i];
            }
        }

        // Smoothly move the camera towards the target position using Lerp
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 3f);
    }
    public void CheckBlockPosition()
    {
        if (centreObj != null)
        {
            float gameObjectY = centreObj.transform.position.y;

            if (gameObjectY >= centerY)
            {
                target.y = centerY + camOffset;

                AdjustCamera();
            }
        }
    }
    void AdjustCamera()
    {
        blockManager.blockPos.y += selectionBlockOffset;
    }
}