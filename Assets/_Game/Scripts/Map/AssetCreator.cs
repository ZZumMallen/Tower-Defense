#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Partisan
{
    public static class AssetCreator
    {
        public static void CreateLevelAsset(string assetName, List<int> numbers, int width,int height) 
        {
            // Create the instance in memory first
            var asset = ScriptableObject.CreateInstance(typeof(MapDataSO));

            foreach (var num in numbers)
            {
                ((MapDataSO)asset).PathNodeList.Add(num);
            }

            ((MapDataSO)asset).Width = width;
            ((MapDataSO)asset).Height = width;

            // Define the saving path inside the Assets folder
            var path = $"Assets/_Game/{assetName}.asset";

            // Save it permanently to disk
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();

            Debug.Log($"Successfully created permanent asset at: {path}");
        }
    }
}
#endif