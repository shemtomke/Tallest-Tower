using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Block/New Block")]
public class BlockProperties : ScriptableObject
{
    public Sprite currentBlock;
    public List<Sprite> matchingBlocks;
}
