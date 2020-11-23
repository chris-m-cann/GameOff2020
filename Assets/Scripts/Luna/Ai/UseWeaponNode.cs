using System.Linq;
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


        protected override State OnExecute(AgentContext context)
        {
            var weapon = context.AgentBlackboard.RetrieveData<Weapon>(weaponKey);
            if (weapon == null) return State.Failed;

            var agentNode = context.Occupant.CurrentNode;
            if (agentNode == null) return State.Failed;

            var targets = weapon.FindTargets(agentNode.Value, context.Occupant.Grid);


            foreach (var target in targets)
            {
                var actions = weapon.Apply(target, context.Agent);
                context.Unit.QueueRange(actions);
            }

            return State.Succeeded;
        }
    }
}