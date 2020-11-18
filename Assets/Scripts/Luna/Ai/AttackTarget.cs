using System.Linq;
using Ai;
using Luna.Grid;
using Luna.Weapons;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class AttackTarget : BtNode
    {
        private Blackboard.ElementKey _weaponKey;
        private Blackboard.ElementKey _targetNodeKey;

        public AttackTarget(Blackboard.ElementKey weaponKey, Blackboard.ElementKey targetNodeKey)
        {
            _weaponKey = weaponKey;
            _targetNodeKey = targetNodeKey;
        }

        public override State Execute(AgentContext context)
        {
            if (context.Target == null) return State.Failed;

            var unit = context.Agent.GetComponent<Unit.Unit>();
            if (unit == null) return State.Failed;

            var weapon = context.AgentBlackboard.RetrieveData<Weapon>(_weaponKey);
            if (weapon == null) return State.Failed;

            var targetNode = context.AgentBlackboard.RetrieveData<Grid.Grid.Node?>(_targetNodeKey);
            if (targetNode == null) return State.Failed;

            var occupant = context.Agent.GetComponent<GridOccupantBehaviour>();
            if (occupant == null) return State.Failed;

            var agentNode = occupant.CurrentNode;
            if (agentNode == null) return State.Failed;

            var targets = weapon.FindTargets(agentNode.Value, occupant.Get().Value);

            if (!targets.Any(it => it.Equals(targetNode.Value))) return State.Failed;


            var actions = weapon.Apply(targetNode.Value, context.Agent);
            unit.AddActions(actions);

            return State.Succeeded;
        }
    }
}