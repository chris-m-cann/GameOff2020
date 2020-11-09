using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Luna
{
    public class Player : TurnTaker
    {
        [SerializeField] private GridVariable grid;
        [SerializeField] private MouseController mouse;
        [SerializeField] private Pathfinding pathfinding;

        private List<Grid.Node> _path;
        private bool _turnComplete = false;
        private bool _awaitingDestination = false;

        private ITurnAction _currentAction;
        private TurnActionController _actionController;
        private void Awake()
        {
            _actionController = new TurnActionController(gameObject);
            _actionController.OnTurnCompleted += actionComplete =>
            {
                if (actionComplete) _currentAction = null;
                _turnComplete = true;
            };
        }

        public override void StartTurn()
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
            // build path
            // var newPos = new Vector3(Mathf.FloorToInt(position.x) +.5f, Mathf.FloorToInt(worldPoint.y) + .5f, worldMouse.position.z);
            _path = pathfinding.CalculatePath(transform.position, position);

            return new MoveAlongPathAction(_path);
        }


        public override bool IsTurnFinished()
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