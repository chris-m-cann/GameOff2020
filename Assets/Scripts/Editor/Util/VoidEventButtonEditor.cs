using UnityEditor;
using Util;
using Util.Events;

namespace Editor.Util
{
    
    [CustomEditor(typeof(VoidGameEvent))]
    public class VoidEventButtonEditor : EventButtonEditor<Void>
    {
    }

}