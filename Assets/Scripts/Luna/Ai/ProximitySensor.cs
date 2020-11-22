using System;
using Ai;
using Luna.Grid;
using UnityEngine;
using Util;
using Util.Ai;

namespace Luna.Ai
{

    [RequireComponent(typeof(IProvider<GridVariable>))]
    public class ProximitySensor : BaseSensor
    {
        [SerializeField] private int range = 2;
        [SerializeField] private LayerMask targetLayers;
        [SerializeField] private BlackboardKey targetNodeKey;
        [SerializeField] private bool removeTargetOnExit = true;


        private Vector2 _size;

        private IProvider<GridVariable> _grid;
        private bool _hasTarget;

        protected override void Awake()
        {
            base.Awake();
            _grid = GetComponent<IProvider<GridVariable>>();
        }

        private void Start() => Init();

        private void OnValidate() => Init();

        public override void Check(Blackboard agentBoard)
        {
            var col = Physics2D.OverlapBox(transform.position, _size, 0, targetLayers);



            if (col?.transform != null)
            {
                var node = new Grid.Grid.Node();

                if (_grid.Get().Value.TryGetNodeAtWorldPosition(col.transform.position, ref node))
                {
                    _hasTarget = true;
                    _agent.Target = col.transform;
                    agentBoard.Add<Grid.Grid.Node?>(targetNodeKey, node);
                    return;
                }
            }

            if (removeTargetOnExit)
            {
                _hasTarget = false;
                _agent.Target = null;
                agentBoard.Remove(targetNodeKey);
            }
        }

        private void Init()
        {
            _size = new Vector2(range, range);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _hasTarget ? Color.red : Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(_size.x, _size.y, 1));
        }
    }
}