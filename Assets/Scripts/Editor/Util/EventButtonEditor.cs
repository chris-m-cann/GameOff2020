using UnityEngine;
using Util.Events;

namespace Util.Editor
{
    public abstract class EventButtonEditor<T> : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GameEvent<T> gameEvent = (GameEvent<T>) target;

            GUI.enabled = Application.isPlaying;


            if (GUILayout.Button("Raise") && gameEvent.ValueToRaise != null)
            {
                gameEvent.Raise(gameEvent.ValueToRaise);
            }
        }
    }
}