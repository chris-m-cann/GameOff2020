using System;
using System.Collections.Generic;
using Ai;
using UnityEngine;
using Util;
using Util.Ai;
using Util.Ai.Bt;

namespace Luna.Ai
{
    public class StubTreeProviderBehaviour : MonoBehaviour, IProvider<BtNode>
    {
        [SerializeField] private string weaponKey;
        [SerializeField] private string targetNodeKey;
        private BtNode _root;

        private void Awake()
        {
            _root = BuildTree();
        }

        private BtNode BuildTree()
        {
            var move = new MoveToMoveTarget();
            var pickMoveTarget = new CreateMoveTarget();
            var attack = new AttackTarget(Blackboard.StringToKey(weaponKey), Blackboard.StringToKey(targetNodeKey));

            var idle =  new SequenceBtNode(new List<BtNode>{pickMoveTarget, move});

            return new SelectorBtNode(new List<BtNode>{attack, idle});
        }

        public BtNode Get()
        {
            return _root;
        }
    }
}