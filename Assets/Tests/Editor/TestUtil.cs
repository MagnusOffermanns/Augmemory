using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class TestUtil
{


    public static GameObject GenerateBlockPrefab(int rows, int columns)
    {
        int items = (rows * columns) / 2;
        GameObject[] blockList = new GameObject[items];
        string[] blockNames = new string[items];
        Vector3[] customRotations = new Vector3[items];
        for (int i = 0; i < items; i++)
        {
            blockList[i] = new GameObject();
            blockNames[i] = "x";
        }


        GameObject block = new GameObject();
        var rd = new GameObject().AddComponent<Renderer>();
        
        var mBlock = block.AddComponent<MemoryBlock>();
        mBlock.objectList = blockList;
        mBlock.memoryObject = mBlock.gameObject;
        mBlock.customRotation = customRotations;
        mBlock.nameList = blockNames;
        mBlock.nameTag = new GameObject().AddComponent<TMPro.TextMeshProUGUI>();

        mBlock.active = true;

        return block;
    }
}

