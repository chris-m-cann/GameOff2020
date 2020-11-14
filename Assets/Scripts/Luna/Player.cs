using System.Collections.Generic;
using System.Linq;
using Luna.Grid;
using Luna.Weapons;
using UnityEngine;
using Util;

namespace Luna
{
    public class Player : MonoBehaviour, ITurnTaker
    {
        [SerializeField] private GridVariable grid;
        [SerializeField] private MouseController mouse;
        [SerializeField] private Pathfinding pathfinding;
        [SerializeField] private Weapon mainWeapon;

        private List<Grid.Grid.Node> _path;
        private bool _turnComplete = false;
        private ITurnAction _currentAction;
        private TurnActionController _actionController;

        private void Awake()
        {
            _actionController = new TurnActionController(gameObject, grid);
            _actionController.OnTurnCompleted += actionComplete =>
            {
                if (actionComplete) _currentAction = null;
                _turnComplete = true;
            };
        }

        public void StartTurn()
        {
            _turnComplete = false;

            if (_currentAction == null)
            {
                GetNextAction();
            }
            else
            {
                _currentAction.Run(_actionController);
            }
        }

        public void OnPositionSelected(Vector3 position)
        {
            if (_currentAction != null)
            {
                _currentAction.Cancel(_actionController);
                _currentAction = null;
            }
            else
            {
                _currentAction = BuildAction(position);
                _currentAction.Run(_actionController);
            }
        }

        private void GetNextAction()
        {
            _path = null;
            // turn on the mouse input
            mouse.SetIndicator(true);

            // doenst really do anything is all handled by the OnPositionSelected call
        }

        private ITurnAction BuildAction(Vector3 position)
        {
            mouse.SetIndicator(false);


            Grid.Grid.Node clickedNode = new Grid.Grid.Node();
            var isClickedNodeValid = grid.Value.TryGetNodeAtWorldPosition(position, ref clickedNode);


            Grid.Grid.Node myNode = new Grid.Grid.Node();
            var isMyNodeValid = grid.Value.TryGetNodeAtWorldPosition(transform.position, ref myNode);

            if (isMyNodeValid && isClickedNodeValid)
            {
                var targets = mainWeapon.FindTargets(myNode, grid.Value);

                if (targets.Contains(clickedNode))
                {
                    mainWeapon.Apply(clickedNode, gameObject);
                }
            }

            _path = pathfinding.CalculatePath(transform.position, position);

            return new MoveAlongPathAction(_path);
        }


        public bool IsTurnFinished()
        {
            return _turnComplete;
        }

        private void OnDrawGizmos()
        {
            if (_path == null || grid == null || grid.Value == null) return;

            foreach (var node in _path)
            {
                var pos = grid.Value.GetWorldPos(node);

                Gizmos.color = Color.blue;
                Gizmos.DrawCube(pos, Vector3.one);
            }
        }
    }
}