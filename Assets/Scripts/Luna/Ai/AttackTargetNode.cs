using System;
using System.Linq;
using Ai;
using Luna.Grid;
using Luna.Weapons;
using UnityEngine;
using Util;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    [Serializable]
    public class AttackTargetNode : BtNode
    {
        [SerializeField] private BlackboardKey weaponKey;
        [SerializeField] private BlackboardKey targetNodeKey;


        protected override State OnExecute(AgentContext context)
        {
            if (context.Target == null) return State.Failed;

            var weapon = context.AgentBlackboard.RetrieveData<Weapon>(weaponKey);
            if (weapon == null) return State.Failed;

            var targetNode = context.AgentBlackboard.RetrieveData<Grid.Grid.Node?>(targetNodeKey);
            if (targetNode == null) return State.Failed;


            var agentNode = context.Occupant.CurrentNode;
            if (agentNode == null) return State.Failed;

            var targetPosition = new Vector2Int(targetNode.Value.X, targetNode.Value.Y);
            var direction = targetPosition - context.Occupant.Occupant.Position;
            var targets = weapon.FindTargets(context.Occupant.Occupant, direction, context.Occupant.Grid);

            if (targets.All(it => it.Position != targetPosition)) return State.Failed;


            var actions = weapon.Use(context.Occupant.Occupant, direction, context.Occupant.Grid);
            context.Unit.QueueRange(actions);

            return State.Succeeded;
        }
    }
}