using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Partisan
{

	
	public class MapController : MonoBehaviour
	{
		public static MapController Instance;

		[SerializeField] private GameObject buildableNode;
		[SerializeField] private GameObject pathNode;

		private int _width;
		private int _height;	
		private float _startX, _startZ, _endX, _endZ;
		private int _nodeIndex;
		
		private Node _currentNode;
		private NodeUI _nodeUI;
		private Vector3 _offsetPosition;

		private readonly Dictionary<int, Vector3> _nodeDictionary = new Dictionary<int, Vector3>();
		private readonly List<int> _pathNodes = new List<int>();
		

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
			GetPositionOffsets(_width, _height);
		}
		

		private void CreateEmptyGird()
		{
			for (var z = _startZ; z < _endZ; z++)
			{
				for (var x = _startX; x < _endX; x++)
				{
					_offsetPosition = new  Vector3(x, 0f, z);
					//InstantiateNode(_offsetPosition);
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
			/*Debug.Log("InstantiateNode", this);
			var n = Instantiate(emptyNode, position, Quaternion.identity);
			var currentNode = n.GetComponent<Node>();
			currentNode.index = _nodeIndex;*/
		}

		public void AddNodeToPathNodeList(int index)
		{
			if (!_pathNodes.Contains(index))
			{
				_pathNodes.Add(index);
			}
		}

		public void RemoveNodeFromPathNodeList(int index)
		{
			if (!_pathNodes.Contains(index)) return;
			var thisIndex = _pathNodes.IndexOf(index);
			_pathNodes.RemoveAt(thisIndex);
		}


		private void AddPathNodes()
		{
			for (var index = 0; index < _nodeDictionary.Count; index++)
			{
				Instantiate(_pathNodes.Contains(index) ? pathNode : buildableNode, _nodeDictionary[index],
					Quaternion.identity);
			}
		}
	}
}
