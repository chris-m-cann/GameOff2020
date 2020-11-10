using System.Collections.Generic;
using Luna.Grid;

namespace Luna
{
    public class MoveAlongPathAction : ITurnAction
    {
        private List<Grid.Grid.Node> _path;

        public MoveAlongPathAction(List<Grid.Grid.Node> path)
        {
            _path = path;
        }

        public void Run(TurnActionController controller)
        {
            if (_path != null && _path.Count > 0)
            {
                var next = _path[0];
                _path.RemoveAt(0);

                var alongPath = controller.Actor.GetComponent<MoveAlongPath>();
                if (alongPath != null)
                {
                    controller.Actor.GetComponent<GridOccupantBehaviour>()?.UpdateGrid(next.WorldPosition);

                    alongPath.Move(next, () =>
                    {
                        controller.OnTurnComplete(_path.Count == 0);
                    });
                }

            }
            else
            {
                controller.OnTurnComplete(true);
            }
        }

        public void Cancel(TurnActionController controller)
        {
            _path = null;
        }
    }
}