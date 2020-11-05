using UnityEngine;

namespace Util
{
    public class InstantiateThing : MonoBehaviour
    {
        public void InstantiateAtPos(GameObject go)
        {
            var transform1 = transform;
            Instantiate(go, transform1.position, transform1.rotation);
        }
    }
}