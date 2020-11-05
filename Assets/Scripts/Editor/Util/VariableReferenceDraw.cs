using UnityEditor;
using UnityEngine;
using Util.Events;
using Util.Variable;

namespace Util.Editor
{
    [CustomPropertyDrawer(typeof(VariableReference<>))]
    public class VariableReferenceDraw : OneOfOptionListDraw
    {
        protected override string[] GetOptions()
        {
            return new[]{"variable", "observable", "constant"};
        }

        protected override string[] GetOptionLabels()
        {
            return  new[]{ "Use Variable", "Use Observable", "Use Constant" };
        }

        protected override string GetDiscriminatName()
        {
            return "option";
        }
    }
}