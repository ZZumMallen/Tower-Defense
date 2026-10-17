using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Partisan
{

	[RequireComponent(typeof(NavMesh))]
	public class MapManager : MonoBehaviour
	{
		public static MapManager Instance;
		
		[SerializeField] private MapDataSO data;
		[SerializeField] private GameObject buildableNode;
		[SerializeField] private GameObject pathNode;

		private int _width, _height, _nodeIndex, _enemySpawn, _enemyTarget;
		private float _startX, _startZ, _endX, _endZ;

		private const float YOffset = 1f;

		private Node _currentNode;
		private Vector3 _offsetPosition;
		private NavMeshSurface _navMesh;

		private readonly Dictionary<int, Vector3> _nodeDictionary = new Dictionary<int, Vector3>();
		private List<int> _pathNodeList = new List<int>(); 
		
		
		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(gameObject);
			}

			Instance = this;
			_navMesh = GetComponent<NavMeshSurface>();
			_width = data.Width;
			_height = data.Height;
			_enemySpawn = data.Spawn;
			_enemyTarget = data.Target;
			_pathNodeList = data.PathNodeList;
		}

		private void Start() // todo this will be awaitable at some point
		{
			GetPositionOffsets(_width, _height);
		}
		
		private void GetPositionOffsets(int newWidth, int newHeight)
		{
			_startX = -(float)newWidth / 2;
			_startZ = -(float)newHeight / 2;
			_endX = (float)newWidth / 2;
			_endZ = (float)newHeight / 2;
		}
		
		[Button]
		private void CreateEmptyGird()
		{
			for (var z = _startZ; z < _endZ; z++)
			{
				for (var x = _startX; x < _endX; x++)
				{
					_offsetPosition = new  Vector3(x, 0f, z);
					_nodeDictionary.Add(_nodeIndex, _offsetPosition);
					_nodeIndex++;
				}
			}
		}

		[Button]
		private void AddPathNodes()
		{
			for (var index = 0; index < _nodeDictionary.Count; index++)
			{
				if (_pathNodeList.Contains(index))
				{
					NodeFactory(index, pathNode, false);
				}
				else
				{
					NodeFactory(index, buildableNode, false);
				}
			}
		}

		private void NodeFactory(int index, GameObject nodeType, bool showUI)
		{
			var currentNode = Instantiate(nodeType, _nodeDictionary[index], Quaternion.identity, transform);
			var thisNode = currentNode.GetComponent<Node>();
			thisNode.Index = index;
			thisNode.ShowUI = showUI;
		}

		[Button]
		private void BakeNavMesh()
		{
			_navMesh.BuildNavMesh();
		}

		[Button]
		private void CreateEnemySpawnPoint()
		{
			var p = _nodeDictionary[_enemySpawn];
			var enemySpawn = new Vector3(p.x, p.y + YOffset, p.z);

			var cube = GameObject.CreatePrimitive(PrimitiveType.Cube); // debugging only
			cube.transform.position = enemySpawn; // debugging only
			
			GameEvents.Instance.EnemySpawnPointCreated(enemySpawn);
		}

		[Button]
		private void CreateEnemyTargetPoint() //todo set start and end points int the builder
		{
			var p = _nodeDictionary[_enemyTarget];
			var enemyTarget = new Vector3(p.x, p.y + YOffset, p.z);
			
			var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);// debugging only
			cube.transform.position = enemyTarget;// debugging only
			
			GameEvents.Instance.EnemyTargetPointCreated(enemyTarget);
		}
		
		
	}
}
