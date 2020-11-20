using UnityEditor;
using Util.Events;

namespace Editor.Util
{

    [CustomEditor(typeof(IntGameEvent))]
    public class IntEventButtonEditor : EventButtonEditor<int>
    {
    }
}