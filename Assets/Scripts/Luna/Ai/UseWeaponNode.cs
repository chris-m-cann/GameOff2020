using System.Linq;
using Ai;
using Luna.Weapons;
using UnityEngine;
using Util;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class UseWeaponNode : BtNode
    {
        [SerializeField] private BlackboardKey weaponKey;
        [SerializeField] private BlackboardKey directionKey;


        protected override State OnExecute(AgentContext context)
        {
            var weapon = context.AgentBlackboard.RetrieveData<Weapon>(weaponKey);
            if (weapon == null) return State.Failed;

            if (!context.AgentBlackboard.Contains(directionKey)) return State.Failed;
            var direction = context.AgentBlackboard.RetrieveData<Vector2Int>(directionKey);

            var agentNode = context.Occupant.CurrentNode;
            if (agentNode == null) return State.Failed;

            var actions = weapon.Use(context.Occupant.Occupant, direction, context.Occupant.Grid);
            context.Unit.QueueRange(actions);

            return State.Succeeded;
        }
    }
}