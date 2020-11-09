using System.Collections.Generic;

namespace Luna
{
    public class MoveAlongPathAction : ITurnAction
    {
        private List<Grid.Node> _path;

        public MoveAlongPathAction(List<Grid.Node> path)
        {
            _path = path;
        }

        public void Run(TurnActionController controller)
        {
            if (_path != null && _path.Count > 0)
            {
                var next = _path[0];
                _path.RemoveAt(0);

                controller.Actor.GetComponent<MoveAlongPath>()?.Move(next, () =>
                {
                    controller.OnTurnComplete(_path.Count == 0);
                });
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