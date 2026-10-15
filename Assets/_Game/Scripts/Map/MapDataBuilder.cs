using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Partisan
{

	
	public class MapDataBuilder : MonoBehaviour
	{
		public static MapDataBuilder Instance;
		
		[BoxGroup("Titles", ShowLabel = false)]
		[TitleGroup("Titles/First Title")]
		[SerializeField] private string mapName;
		[TitleGroup("Titles/First Title")]
		[SerializeField] private int width;
		[TitleGroup("Titles/First Title")]
		[SerializeField] private int height;
		[TitleGroup("Titles/Prefabs")]
		[SerializeField] private GameObject emptyNode;
		[TitleGroup("Titles/Prefabs")]
		[SerializeField] private GameObject buildableNode;
		[TitleGroup("Titles/Prefabs")]
		[SerializeField] private GameObject pathNode;
		[TitleGroup("Titles/Prefabs")]
			
		private Node _currentNode;
		private NodeUI _nodeUI;

		private float _startX, _startZ, _endX, _endZ;
		
		private readonly Dictionary<int, Vector3> _nodeDictionary = new Dictionary<int, Vector3>();
		
		[TitleGroup("Titles/Path Nodes")]
		[PropertySpace(SpaceAfter = 20)]
		[SerializeField] private List<int> pathNodes = new List<int>();
		private int _nodeIndex;
		private Vector3 _offsetPosition;

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(gameObject);
			}

			Instance = this;
		}

		private void Start()
		{
			GetPositionOffsets(width, height);
		}
		
		[TitleGroup("Titles/Horizontal Buttons")]
		[Button(ButtonSizes.Large)]
		[ButtonGroup("Titles/Horizontal Buttons/Buttons")]
		private void CreateEmptyGird()
		{
			for (var z = _startZ; z < _endZ; z++)
			{
				for (var x = _startX; x < _endX; x++)
				{
					_offsetPosition = new  Vector3(x, 0f, z);
					InstantiateNode(_offsetPosition);
					_nodeDictionary.Add(_nodeIndex, _offsetPosition);
					_nodeIndex++;
				}
			}
		}
		
		private void GetPositionOffsets(int newWidth, int newHeight)
		{
			_startX = -(float)newWidth / 2;
			_startZ = -(float)newHeight / 2;
			_endX = (float)newWidth / 2;
			_endZ = (float)newHeight / 2;
		}

		private void InstantiateNode(Vector3 position)
		{
			Debug.Log("InstantiateNode", this);
			var n = Instantiate(emptyNode, position, Quaternion.identity);
			var currentNode = n.GetComponent<Node>();
			currentNode.index = _nodeIndex;
		}

		public void AddNodeToPathNodeList(int index)
		{
			if (!pathNodes.Contains(index))
			{
				pathNodes.Add(index);
			}
		}

		public void RemoveNodeFromPathNodeList(int index)
		{
			if (!pathNodes.Contains(index)) return;
			var thisIndex = pathNodes.IndexOf(index);
			pathNodes.RemoveAt(thisIndex);
		}

		
		private void AddPathNodes()
		{
			for (var index = 0; index < _nodeDictionary.Count; index++)
			{
				Instantiate(pathNodes.Contains(index) ? pathNode : buildableNode, _nodeDictionary[index],
					Quaternion.identity);
			}
		}
		
		
		private void CreateLevel()
		{
			AssetCreator.CreateLevelAsset(mapName,pathNodes,width,height);
		}
	}
}
