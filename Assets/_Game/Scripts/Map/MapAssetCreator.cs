#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Partisan
{
    public static class MapAssetCreator
    {
        public static void CreateLevelAsset(string assetName, List<int> numbers, int width,int height, int spawn, int target) 
        {
            // Create the instance in memory first
            var asset = ScriptableObject.CreateInstance(typeof(MapDataSO));

            foreach (var num in numbers)
            {
                ((MapDataSO)asset).PathNodeList.Add(num);
            }

            ((MapDataSO)asset).Width = width;
            ((MapDataSO)asset).Height = height;
            
            ((MapDataSO)asset).Spawn = spawn;
            ((MapDataSO)asset).Target = height;

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