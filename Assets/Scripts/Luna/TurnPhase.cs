namespace Luna
{
    public enum TurnPhase
    {
        TurnStart,
        ChoosingAction,
        PerformingAction,
        ResolvingAction
    }
}

/*

    if (currentPhase != null) {
        var allDone = true;
        foreach (action in currentPhase) {
            if (action.complete == false) allDone = false;
        }

        if (all done) {
            currentPhase = phases.pop() ?? return
            foreach (action in currentPhase)
                action.Start();
        }
    } else {
        foreach(turntaker in turns.next())
            actions += taker.start()

        currentPhase = actions.phases.pop()
    }
*/