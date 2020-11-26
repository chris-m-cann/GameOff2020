using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;

namespace Luna.Grid
{
    [CreateAssetMenu(menuName = "Custom/RoomTemplate")]
    public class RoomTemplate : ScriptableObject
    {
        [Multiline(lines:12)]
        [SerializeField] private string template;

        public string Template => template.Replace("\n", "");
    }
}