using System;
using System.Collections.Generic;
using Luna.Grid;
using Luna.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using Util;

namespace Luna
{
    public class MouseController : MonoBehaviour
    {
        public enum MouseMode
        {
            Moving,
            RangedWeaponMode,
            MelleWeapon
        }
        [SerializeField] private Transform worldMouse;
        [SerializeField] private GridVariable grid;
        [SerializeField] private Tilemap uiTilemap;
        [SerializeField] private Tilemap highlightedUiTilemap;
        [SerializeField] private Tile up;
        [SerializeField] private Tile highlighted;
        [SerializeField] private Tile attackTile;
        [SerializeField] private RangedWeapon ranged;
        [SerializeField] private MeleeWeapon melee;


        private Grid.Grid.Node? _node;
        private MouseMode _mode = MouseMode.Moving;

        private Camera _camera;
        private Pathfinding _pathfinding;
        private GridOccupantBehaviour _player;
        private List<Grid.Grid.Node> _path;

        private void Awake()
        {
            _pathfinding = GetComponent<Pathfinding>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (worldMouse == null && worldMouse.gameObject.activeSelf) return;
            if (grid == null || grid.Value == null) return;
            if (uiTilemap == null) return;

            var worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
            var node = new Grid.Grid.Node();
            if (grid.Value.TryGetNodeAtWorldPosition(worldPoint, ref node))
            {
                if (_node?.Equals(node) != true)
                {
                    _node = node;
                    OnNewNode(node);
                }

            }

        }

        private void OnNewNode(Grid.Grid.Node node)
        {

            switch (_mode)
            {
                case MouseMode.Moving:
                    highlightedUiTilemap.ClearAllTiles();

                    _path = _pathfinding.CalculatePath(_player.transform.position, node.WorldPosition);

                    if (_path != null && _path.Count > 0)
                    {
                        foreach (var n in _path)
                        {
                            var pos = grid.Value.Position00.ToVector2Int() + n.Position;
                            highlightedUiTilemap.SetTile((Vector3Int)pos, up);
                        }
                    }
                    else
                    {
                        var pos = grid.Value.Position00.ToVector2Int() + node.Position;
                        if (uiTilemap.HasTile((Vector3Int) pos))
                        {
                            highlightedUiTilemap.SetTile((Vector3Int)pos, attackTile);
                        }
                    }

                    break;
                case MouseMode.RangedWeaponMode:
                    var diff = node.Position - _player.CurrentNodeIdx;

                    highlightedUiTilemap.ClearAllTiles();
                    if (diff.IsCardinal())
                    {
                        HighlightCardinal(diff, highlightedUiTilemap);
                    }
                    break;
                default:
                    worldMouse.position = node.WorldPosition;
                    break;
            }
        }

        public void SetIndicator(bool b, GridOccupantBehaviour player, MeleeWeapon defaultWeapon)
        {
            melee = defaultWeapon;
            _player = player;
            worldMouse.gameObject.SetActive(b);

            if (b)
            {
                SetMode(MouseMode.Moving);
                OnNewMode(MouseMode.Moving);
                if (_node != null)
                {
                    OnNewNode(_node.Value);
                }
            }
            else
            {
                uiTilemap.ClearAllTiles();
                highlightedUiTilemap.ClearAllTiles();
            }

        }

        private void SetMode(MouseMode mode)
        {
            if (mode != _mode && _node != null)
            {
                _mode = mode;
                OnNewMode(mode);
                OnNewNode(_node.Value);
            }
            else
            {
                _mode = mode;
            }
        }

        private void OnNewMode(MouseMode mode)
        {
            highlightedUiTilemap.ClearAllTiles();
            uiTilemap.ClearAllTiles();

            switch (mode)
            {
                case MouseMode.RangedWeaponMode:
                    HighlightCardinal(Vector2Int.down, uiTilemap);
                    HighlightCardinal(Vector2Int.left, uiTilemap);
                    HighlightCardinal(Vector2Int.right, uiTilemap);
                    HighlightCardinal(Vector2Int.up, uiTilemap);

                    break;
                default:
                    var targets = melee?.FindTargets(_player.Occupant, Vector2Int.up, grid.Value);
                    if (targets != null)
                    {
                        foreach (var target in targets)
                        {
                            var pos = grid.Value.Position00.ToVector2Int() + target.Position;
                            uiTilemap.SetTile((Vector3Int) pos, attackTile);
                        }
                    }

                    break;
            }

        }

        private void HighlightCardinal(Vector2Int direction, Tilemap tilemap)
        {
            var nodes = ranged.CalculatePath(_player.Occupant, direction, grid.Value);
            for (int i = 0; i < nodes.Travelled.Count; i++)
            {
                if (nodes.IsLastNodeTarget && i == nodes.Travelled.Count - 1)
                {
                    var pos = grid.Value.Position00.ToVector2Int() + nodes.Travelled[i].Position;
                    tilemap.SetTile((Vector3Int) pos, attackTile);
                }
                else
                {
                    var pos = grid.Value.Position00.ToVector2Int() + nodes.Travelled[i].Position;
                    tilemap.SetTile((Vector3Int) pos, highlighted);
                }
            }
        }

        public void EnterRangedMode(RangedWeapon weapon)
        {
            ranged = weapon;
            SetMode(MouseMode.RangedWeaponMode);
        }

        public void EnterDefaultMode(MeleeWeapon defaultWeapon)
        {
            melee = defaultWeapon;
            SetMode(MouseMode.Moving);
        }

        public void EnterMeleeOnlyMode(MeleeWeapon weapon)
        {
            melee = weapon;
            SetMode(MouseMode.MelleWeapon);
        }
    }
}