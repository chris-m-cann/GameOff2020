using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Util.Events;

namespace Util.Editor
{

    [CustomEditor(typeof(IntGameEvent))]
    public class IntEventButtonEditor : EventButtonEditor<int>
    {
    }
}