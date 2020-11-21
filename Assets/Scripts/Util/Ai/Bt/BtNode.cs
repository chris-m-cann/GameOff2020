using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Util.Ai.Bt
{
    [Serializable]
    public abstract class BtNode : Node
    {
        public enum State
        {
            Succeeded,
            Failed,
            Running
        }

        #if UNITY_EDITOR
        public string Description;
        #endif

        [Input] public BtNode Parent;

        public State CurrentState { get; protected set; }

        public abstract State Execute(AgentContext context);

        public override void OnCreateConnection(NodePort @from, NodePort to)
        {
            base.OnCreateConnection(@from, to);

            RefreshParent();
        }

        public override void OnRemoveConnection(NodePort port)
        {
            base.OnRemoveConnection(port);
            RefreshParent();
        }

        private void RefreshParent()
        {
            NodePort parentPort = GetPort(nameof(Parent));

            if (parentPort != null && parentPort.ConnectionCount > 0)
            {
                var parentConnection = parentPort.GetConnection(0);
                if (parentConnection != null)
                {
                    Parent = (BtNode) parentConnection.node;
                    return;
                }
            }

            Parent = null;
        }
    }
}