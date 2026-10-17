using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Partisan
{
	public class Node : MonoBehaviour
	{
		
		[SerializeField] private GameObject nodeUIPrefab;
		private int _index;
		[ShowInInspector] public int Index { get; set; }
		public bool ShowUI { get; set; } = false;

		public event Action<int> IndexAssigned;
		
		private void Start()
		{
			_index = Index;
			if (!ShowUI) return;
			
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
