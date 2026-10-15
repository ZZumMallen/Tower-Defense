using System;
using UnityEngine;

namespace Partisan
{
	public class Node : MonoBehaviour
	{
		[SerializeField] private GameObject nodeUIPrefab;
		[SerializeField] private float offsetY;
		private int _index;
		public int index;

		public event Action<int> IndexAssigned;

		private void Start()
		{
			_index = index;
			SetNodeUI();
		}

		private void SetNodeUI()
		{
			var n = Instantiate(nodeUIPrefab, transform);
			var nodeUI  = n.GetComponent<NodeUI>();
			nodeUI.Bind(this);
			IndexAssigned?.Invoke(_index);
		}
	}
}
