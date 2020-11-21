using System;
using Ai;
using UnityEngine;

namespace Util
{
    [CreateAssetMenu(menuName = "Custom/BlackboardKey")]
    public class BlackboardKey : ScriptableObject
    {
        [HideInInspector]
        public Blackboard.ElementKey Key;

        private void OnEnable()
        {
            Refresh();
        }

        private void OnValidate()
        {
            Refresh();
        }

        private void Refresh()
        {
            Key = Blackboard.StringToKey(name);
        }
}
}