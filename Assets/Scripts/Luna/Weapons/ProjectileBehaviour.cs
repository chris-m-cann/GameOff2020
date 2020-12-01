using System;
using Luna.Actions;
using Luna.Grid;
using UnityEngine;
using Util;
using Util.Inventory;
using Object = System.Object;

namespace Luna.Weapons
{
    [RequireComponent(typeof(MoveAlongPath), typeof(Unit.Unit))]
    public class ProjectileBehaviour : MonoBehaviour
    {
        public int Range = 3;

        // todo(chris) add back in when doing penitration
        // // how many targets it can hit
        // public int PenitrationDepth;
        // // what types of target can it penitrate
        // public GridOccupantType[] PenitrationTags;
        [SerializeField] private PickUpBehaviour pickupPrefab;
        [SerializeField] private InventoryItem item;


        private MoveAlongPath _move;
        private Unit.Unit _unit;

        private void Awake()
        {
            _move = GetComponent<MoveAlongPath>();
            _unit = GetComponent<Unit.Unit>();
        }

        public void Fire(Unit.Unit unit, Grid.Grid.Node destination, Action onComplete)
        {
            _move.Move(unit, destination, () =>
            {
                // spawn item??
                // destroy
                if (pickupPrefab)
                {
                    unit.QueueAction(new SpawnPickupAction(pickupPrefab, transform.position, item));
                    unit.QueueAction(new DestroyGameObjectAction(gameObject));
                }
                else
                {
                    _unit.Occupant.UpdateGrid(transform.position);
                }

                onComplete();
            });
        }
    }

    public class DestroyGameObjectAction : IUnitAction
    {
        private readonly GameObject _gameObject;
        public DestroyGameObjectAction(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public void StartAction(Unit.Unit unit)
        {
            // if about to destroy ourselves and more actions to perform then push this onto the back of the actions list
            if (unit.gameObject == _gameObject && unit.ActionsLeft > 1)
            {
                unit.QueueAction(this);
            }
            else
            {
                UnityEngine.Object.Destroy(_gameObject);
            }
        }

        public bool Tick(Unit.Unit actor)
        {
            return true;
        }

        public int Priority => 999;
    }

    public class SpawnPickupAction : IUnitAction
    {
        private readonly PickUpBehaviour _pickupPrefab;
        private readonly InventoryItem _item;
        private readonly Vector3 _spawnPoint;

        public SpawnPickupAction(PickUpBehaviour pickupPrefab, Vector2 where, InventoryItem item)
        {
            _pickupPrefab = pickupPrefab;
            _spawnPoint = where;
            _item = item;
        }

        public void StartAction(Unit.Unit unit)
        {
            var pickup = UnityEngine.Object.Instantiate(_pickupPrefab, _spawnPoint, Quaternion.identity).GetComponent<PickUpBehaviour>();

            pickup.SetItem(_item);
        }

        public bool Tick(Unit.Unit actor)
        {
            return true;
        }

        public int Priority => 2;
    }
}