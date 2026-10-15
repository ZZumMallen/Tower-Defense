using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Properties;
using UnityEngine;

namespace Partisan
{
    [CreateAssetMenu(fileName = "MapDataSO", menuName = "Scriptable Objects/MapDataSO")]
    [UsedImplicitly]
    public class MapDataSO : ScriptableObject
    {
        [SerializeField, DontCreateProperty] private int width;
        [SerializeField, DontCreateProperty] private int height;
        [SerializeField] private List<int> pathNodeList = new List<int>();

        public int Width
        {
            get => width;
            set => width = value;
        }

        public int Height
        {
            get => height;
            set => height = value;
        }

        public List<int> PathNodeList => pathNodeList;
        
    }
    

}
