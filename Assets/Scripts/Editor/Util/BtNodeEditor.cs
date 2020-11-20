using UnityEditor;
using UnityEngine;
using Util.Ai.Bt;
using XNodeEditor;

namespace Editor.Util
{
    [CustomNodeEditorAttribute(typeof(BtNode))]
    public class BtNodeEditor : NodeEditor
    {
        public override void OnBodyGUI()
        {
            var node = (BtNode) target;
            var parent = node.GetPort(nameof(node.Parent));
            if (parent != null && parent.ConnectionCount == 1)
            {
                var parentPort = parent.GetConnection(0);
                var connections = parentPort.GetConnections();

                if (connections.Count > 1)
                {
                    for (int i = 0; i < connections.Count; i++)
                    {
                        if (connections[i].node == node)
                        {
                            EditorGUILayout.LabelField(i.ToString(), EditorStyles.boldLabel);
                        }
                    }
                }
            }

            base.OnBodyGUI();
        }
    }
}