using System;
using System.Collections.Generic;
using Ai;
using Luna.Ai;
using UnityEngine;
using Util.Ai.Bt;

namespace Util.Ai
{
    [RequireComponent(typeof(IProvider<BtNode>))]
    public class Agent : MonoBehaviour
    {
        [SerializeField] private Blackboard globalBlackboard;

        public Blackboard AgentBlackboard => _context.AgentBlackboard;

        public Transform Target
        {
            get => _context.Target;
            set => _context.Target = value;
        }

        private BtNode _treeRoot;
        private AgentContext _context;
        private List<ISensor> _sensors = new List<ISensor>();

        private void Awake()
        {
            var agentBlackboard = ScriptableObject.CreateInstance<Blackboard>();
            _context = new AgentContext
            {
                GlobalBlackboard = globalBlackboard,
                AgentBlackboard = agentBlackboard,
                Agent = gameObject,
                Target = null
            };
        }

        public void Execute()
        {
            if (_treeRoot == null)
            {
                _treeRoot = GetComponent<IProvider<BtNode>>().Get();
            }

            foreach (var sensor in _sensors)
            {
                sensor.Check(AgentBlackboard);
            }

            _treeRoot.Execute(_context);
        }

        public void RegisterSensor(ISensor sensor)
        {
            if (!_sensors.Contains(sensor)) _sensors.Add(sensor);
        }


        public void UnregisterSensor(ISensor sensor)
        {
            if (_sensors.Contains(sensor)) _sensors.Remove(sensor);
        }
    }
}