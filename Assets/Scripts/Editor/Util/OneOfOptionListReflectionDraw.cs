using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Util;

namespace Editor.Util
{
    public abstract class OneOfOptionListReflectionDraw<T> : PropertyDrawer
    {
        protected virtual string GetDisciminantName() => "option";

        private GUIStyle _popupStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_popupStyle == null)
            {
                _popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                _popupStyle.imagePosition = ImagePosition.ImageOnly;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            var members = typeof(T).GetFields().Where(it => it.IsPublic).Select(it => it.Name).ToArray();


            SerializedProperty discriminat = property.FindPropertyRelative(GetDisciminantName());
            SerializedProperty[] properties = new SerializedProperty[members.Length];
            properties[0] = property;

            for (int i = 0; i < members.Length; i++)
            {
                properties[i] = property.FindPropertyRelative(members[i]);
            }


            // Calculate rect for configuration button
            Rect buttonRect = new Rect(position);
            buttonRect.yMin += _popupStyle.margin.top;
            buttonRect.width = _popupStyle.fixedWidth + _popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;


            int result = EditorGUI.Popup(buttonRect, discriminat.intValue, members, _popupStyle);

            discriminat.intValue = result;

            EditorGUI.PropertyField(position,
                properties[discriminat.intValue],
                GUIContent.none);

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}