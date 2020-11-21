using System;
using Ai;
using UnityEngine;

namespace Util
{
    [CreateAssetMenu(menuName = "Custom/BlackboardKey")]
    public class BlackboardKey : ScriptableObject
    {
        [SerializeField] private string key;

        [HideInInspector]
        public Blackboard.ElementKey Key;

        private void OnEnable()
        {
            Refresh();
        }

        private void OnValidate()
        {
            Debug.Log("OnValidate called");
            Refresh();
        }

        private void Refresh()
        {
            if (string.IsNullOrEmpty(key)) return;
            Key = Blackboard.StringToKey(key);
        }
}
}