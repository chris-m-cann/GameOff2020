using UnityEngine;
using Util;

namespace Luna
{
    [RequireComponent(typeof(ITurnTaker))]
    public class AddTurnTakerToSet : AddComponentToRuntimeSet<ITurnTaker>
    {

    }
}