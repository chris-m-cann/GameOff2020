using System;
using UnityEditor;
using UnityEngine;
using Util.Events;
using Util.Variable;

namespace Util
{
    public class ResetAllBehaviour : MonoBehaviour
    {
        [SerializeField] private ResetableRuntimeSet resetables;

        private void OnDestroy()
        {
           resetables.ForEach(it => it.Reset(ResetScenario.OnSceneUnload));
        }
    }
}