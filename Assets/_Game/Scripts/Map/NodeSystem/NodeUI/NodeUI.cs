using UnityEngine;
using UnityEngine.UIElements;

namespace Partisan
{
    [RequireComponent(typeof(PanelRenderer))]
    public class NodeUI : MonoBehaviour
    {
        [SerializeField] private float uiOffset;
        private PanelRenderer _panelRenderer;
        private Node _node;
        private Button _nodeIndexButton;
        private Camera _camera;
        private int _uiVersion = -1;
        private int _nodeIndex;
        
        private void Awake()
        {
            _camera = Camera.main;
            _panelRenderer = GetComponentInChildren<PanelRenderer>();
            _panelRenderer.RegisterUIReloadCallback(OnUIReload);
        }

        private void Start()
        {
            transform.localPosition = new Vector3(0f, uiOffset, 0f);
        }

        private void LateUpdate()
        {
            if (!_camera) return;
            transform.forward = _camera.transform.forward;
        }

        private void OnDestroy()
        {
            _panelRenderer.UnregisterUIReloadCallback(OnUIReload);
            if (_node != null)
            {
                _node.IndexAssigned -= OnIndexAssigned;
            }
        }

        public void Bind(Node node)
        {
            if (_node != null)
            {
                _node.IndexAssigned -= OnIndexAssigned;
            }
            
            _node = node;
            if (_node != null)
            {
                _node.IndexAssigned += OnIndexAssigned;
            }
        }

        private void OnIndexAssigned(int index)
        {
            if (_node == null) return;
            _nodeIndex = index;
            
            SetIndexDisplay(index);
        }

        private void OnUIReload(PanelRenderer panelRenderer, VisualElement root, int version)
        {
            if (_uiVersion == version) return;
            _uiVersion = version;
            
            _nodeIndexButton = root.Q<Button>("NodeIndexButton");
            _nodeIndexButton.clicked += AddIndexToList;
            _nodeIndexButton?.RegisterCallback<PointerDownEvent>(OnButtonPointerDown);
            SetIndexDisplay();
        }

        private void OnButtonPointerDown(PointerDownEvent evt)
        {
            if (evt.button == 1) 
            {
                RemoveIndexFromList();
            
                // Optional: Prevent the event from bubbling up to parent containers
                evt.StopPropagation(); 
            }
            
            if (evt.button == 0)
            {
                Debug.Log("Left-clicked the UI Toolkit Button!");
            }
        }

        private void AddIndexToList() // todo and color change to indicate
        {
            MapDataBuilder.Instance.AddNodeToPathNodeList(_nodeIndex);
        }

        private void RemoveIndexFromList()
        {
            MapDataBuilder.Instance.RemoveNodeFromPathNodeList(_nodeIndex);
        }

        private void SetIndexDisplay()
        {
            SetIndexDisplay(_nodeIndex);
        }
        

        private void SetIndexDisplay(int index)
        {
            if (index < 0 || _nodeIndexButton == null) return;
            _nodeIndexButton.text = index.ToString();
        }

    }
    

}