using System;
using System.Collections.Generic;
using System.Linq;
using Ai;
using UnityEngine;
using Util;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class StubTreeProviderBehaviour : MonoBehaviour, IProvider<BtNode>
    {
        [SerializeField] private BehaviourTree tree;

        public BtNode Get()
        {
            var r = tree.Root;

            Debug.Log($"Root node = {r}");
            return r;
        }
    }
}