using System;
using System.Collections.Generic;
using System.Linq;
using Luna.Actions;
using Luna.Grid;
using UnityEngine;
using Util;
using Random = UnityEngine.Random;

namespace Luna
{
    public class MeteorSpawnerAction : ActionBehaviour
    {
        [SerializeField] private GridVariable grid;
        [SerializeField] private GridOccupantType[] cantSpawnOn;

        [SerializeField] private int minInitialDelay = 0;
        [SerializeField] private int maxInitialDelay = 3;

        [SerializeField] private int minCoolDown = 2;
        [SerializeField] private int maxCoolDown = 5;

        [SerializeField] private MeteorControllerAction meteorPrefab;


        private List<Grid.Grid.Node> _onGoing = new List<Grid.Grid.Node>();
        private bool _doneInitial = false;
        private int _turnsUntilNextSpwan = 0;

        private void Start()
        {
            _turnsUntilNextSpwan = Random.Range(minInitialDelay, maxInitialDelay + 1);
        }

        public override void StartAction(Unit.Unit unit)
        {
            --_turnsUntilNextSpwan;

            if (_turnsUntilNextSpwan < 0 && grid.Value != null)
            {
                Grid.Grid.Node node;

                int breakAfter = 10;
                do
                {
                    if (breakAfter < 0)
                    {
                        Debug.LogError("Couldnt find a valid node to spawn meteor");
                        return;
                    }

                    --breakAfter;

                    node = grid.Value.RandomNode();
                } while (!IsNodeValid(node));


                var meteor = Instantiate(meteorPrefab, node.WorldPosition, Quaternion.identity);

                meteor.OnMeteorDestroyed += it =>
                {
                    _onGoing.Remove(node);
                };

                _onGoing.Add(node);

                _turnsUntilNextSpwan = Random.Range(minCoolDown, maxCoolDown + 1);
            }
        }

        private bool IsNodeValid(Grid.Grid.Node node)
        {
            if (_onGoing.Contains(node)) return false;

            foreach (var occupant in node.Occupants)
            {
                if (cantSpawnOn.Contains(occupant.Type)) return false;
            }

            return true;
        }

        public override bool Tick(Unit.Unit actor)
        {
            return true;
        }
    }
}