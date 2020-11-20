using UnityEngine;
using XNode;

namespace Util.Ai.Bt
{
    public abstract class BtDecoratorNode : BtNode
    {
        [Output] [SerializeField] protected BtNode child;

        public override void OnCreateConnection(NodePort @from, NodePort to)
        {
            base.OnCreateConnection(@from, to);

            RefreshChild();
        }

        public override void OnRemoveConnection(NodePort port)
        {
            base.OnRemoveConnection(port);

            RefreshChild();
        }

        private void RefreshChild()
        {
            NodePort childPort = GetPort(nameof(child));

            if (childPort != null && childPort.ConnectionCount > 0)
            {
                var connection = childPort.GetConnection(0);
                if (connection != null)
                {
                    child = (BtNode) connection.node;
                    return;
                }
            }

            child = null;
        }
    }
}