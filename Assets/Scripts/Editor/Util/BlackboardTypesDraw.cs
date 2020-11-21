using UnityEditor;
using Util.Ai;

namespace Editor.Util
{
    [CustomPropertyDrawer(typeof(BlackboardTypes))]
    public class BlackboardTypesDraw : OneOfOptionListReflectionDraw<BlackboardTypes>
    {

    }
}