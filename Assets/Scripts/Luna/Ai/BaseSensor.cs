using System;
using Ai;
using UnityEngine;
using Util.Ai;

namespace Luna.Ai
{
    [RequireComponent(typeof(Agent))]
    public abstract class BaseSensor : MonoBehaviour, ISensor
    {
        public abstract void Check(Blackboard agentBoard);

        protected Agent _agent;


        protected virtual void Awake()
        {
            _agent = GetComponent<Agent>();
        }


        protected virtual void OnEnable()
        {
            _agent.RegisterSensor(this);
        }

        protected virtual void OnDisable()
        {
            _agent.UnregisterSensor(this);
        }
    }
}