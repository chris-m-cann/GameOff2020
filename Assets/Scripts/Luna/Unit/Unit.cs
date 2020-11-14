using System.Collections.Generic;
using UnityEngine;

namespace Luna.Unit
{
    public abstract class Unit : MonoBehaviour
    {
        public abstract List<IUnitAction> StartTurn();
        public abstract List<IUnitAction> Tick();

        // this this breaks single responsibility principle
        // consider moving to a different role based interface to be used from units
        public abstract void AddAction(IUnitAction action);
        public abstract void AddActions(List<IUnitAction> actions);
    }
}