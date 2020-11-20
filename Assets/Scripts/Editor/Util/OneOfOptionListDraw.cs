using UnityEditor;
using UnityEngine;

namespace Editor.Util
{
    public abstract class OneOfOptionListDraw : PropertyDrawer
    {
        protected abstract string[] GetOptions();
        protected abstract string[] GetOptionLabels();
        protected abstract string GetDiscriminatName();

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

            var options = GetOptions();
            var labelText = GetOptionLabels();
            var discriminatName  = GetDiscriminatName();

            SerializedProperty discriminat = property.FindPropertyRelative(discriminatName);
            SerializedProperty[] properties = new SerializedProperty[options.Length];

            for (int i = 0; i < options.Length; i++)
            {
                properties[i] = property.FindPropertyRelative(options[i]);
            }

            // Calculate rect for configuration button
            Rect buttonRect = new Rect(position);
            buttonRect.yMin += _popupStyle.margin.top;
            buttonRect.width = _popupStyle.fixedWidth + _popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            int result = EditorGUI.Popup(buttonRect, discriminat.intValue, labelText, _popupStyle);

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