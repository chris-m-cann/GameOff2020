using UnityEngine;

namespace Util.Variable
{
    public class ResetAllBehaviour : MonoBehaviour
    {
        [SerializeField] private ResetableRuntimeSet resetables;
        private void OnDestroy()
        {
            if (resetables == null) return;
           resetables.ForEach(it => it.Reset(ResetScenario.OnSceneUnload));
        }
    }
}