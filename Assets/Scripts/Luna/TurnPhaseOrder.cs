using System;
using UnityEngine;
using Util;

namespace Luna
{
    [CreateAssetMenu(menuName = "Custom/PhaseOrder")]
    public class TurnPhaseOrder : ScriptableObject
    {
        [SerializeField] private TurnPhase[] phases;

        public int GetTurnPhaseOrder(TurnPhase phase)
        {
            if (phases == null) return 0;
            int index = 0;

            for (; index < phases.Length; index++)
            {
                if (phases[index] == phase) break;
            }

            return index;
        }

        public Pair<TurnPhase, bool> NextPhase(TurnPhase currentPhase)
        {
            var idx = Array.IndexOf(phases, currentPhase);

            idx++;

            if (idx > (phases.Length - 1))
            {
                return new Pair<TurnPhase, bool>
                {
                    First = phases[0],
                    Second = true
                };
            }
            else
            {
                return new Pair<TurnPhase, bool>
                {
                    First = phases[idx],
                    Second = false
                };
            }


        }
    }
}