using System;
using System.Collections.ObjectModel;

namespace Luna.Actions
{
    public interface IUnitAction
    {
        void StartAction(Unit.Unit unit);

        bool Tick(Unit.Unit actor);

        int Priority { get; }
    }
}