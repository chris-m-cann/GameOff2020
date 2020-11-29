using Luna.Grid;
using Luna.Weapons;
using UnityEngine;

namespace Luna.Actions
{
    public class LaunchProjectileAction : IUnitAction
    {
        private readonly GridOccupant _wielder;
        private readonly Grid.Grid.Node _endPoint;
        private readonly ProjectileBehaviour _projectile;

        private bool _isComplete = false;

        public LaunchProjectileAction(GridOccupant wielder, Grid.Grid.Node endPoint, ProjectileBehaviour projectile)
        {
            _wielder = wielder;
            _endPoint = endPoint;
            _projectile = projectile;
        }

        public void StartAction(Unit.Unit unit)
        {
            var projectile = Object.Instantiate(_projectile, unit.transform.position, Quaternion.identity).GetComponent<ProjectileBehaviour>();
            projectile.Fire(unit, _endPoint, () => _isComplete = true);
        }

        public bool Tick(Unit.Unit actor)
        {
            return _isComplete;
        }

        public int Priority { get; }
    }
}