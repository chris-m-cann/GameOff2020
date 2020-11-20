using System;
using UnityEngine;
using XNode;

namespace Util.Ai.Bt
{
    [CreateAssetMenu(menuName = "Custom/BehaviourTree/Graph")]
    public class BehaviourTree : NodeGraph
    {
        public BtNode Root
        {
            get
            {
                foreach (var node in nodes)
                {
                    var btNode = (BtNode) node;
                    if (btNode.Parent == null) return btNode;
                }
                Debug.LogError($"No root node found for {name}");
                return null;
            }
        }

    }
}
