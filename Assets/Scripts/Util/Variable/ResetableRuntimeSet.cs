using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Util
{
    [CreateAssetMenu(menuName = "Custom/RuntimeSets/ResetableObjects")]
    public class ResetableRuntimeSet : RuntimeSet<ResetableObject>
    {
        void OnEnable()
        {
            string[] guids1 = AssetDatabase.FindAssets("t:ResetableObject");
            foreach (var guid in guids1)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);

                ResetableObject so = AssetDatabase.LoadAssetAtPath<ResetableObject>(path);
                items.Add(so);
            }
        }

        private void OnDisable()
        {
            Clear();
        }
    }
}