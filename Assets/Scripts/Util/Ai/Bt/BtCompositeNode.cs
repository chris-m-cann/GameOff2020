using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Util.Ai.Bt
{
    public abstract class BtCompositeNode : BtNode
    {
        [Output] [SerializeField] protected List<BtNode> children = new List<BtNode>();

        public override void OnCreateConnection(NodePort @from, NodePort to)
        {
            base.OnCreateConnection(@from, to);

            RefreshChildren();
        }

        public override void OnRemoveConnection(NodePort port)
        {
            base.OnRemoveConnection(port);

            RefreshChildren();

        }

        private void RefreshChildren()
        {
            children.Clear();
            NodePort port = GetPort(nameof(children));
            if (port == null) return;

            List<NodePort> connections = port.GetConnections();


            foreach (var connection in connections)
            {
                children.Add((BtNode)connection.node);
            }
        }
    }
}