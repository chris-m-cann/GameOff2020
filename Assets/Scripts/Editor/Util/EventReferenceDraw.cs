using UnityEditor;
using Util.Events;

namespace Editor.Util
{
    [CustomPropertyDrawer(typeof(EventReference<>))]
    public class EventReferenceDraw : OneOfOptionListDraw
    {
        protected override string[] GetOptions()
        {
            return new[]{"gameEvent", "observable"};
        }

        protected override string[] GetOptionLabels()
        {
            return new[]{"Use GameEvent", "Use Observable"};
        }

        protected override string GetDiscriminatName()
        {
            return "option";
        }
    }
}