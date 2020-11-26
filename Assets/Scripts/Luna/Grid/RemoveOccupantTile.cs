using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Luna.Grid
{
    [RequireComponent(typeof(Tilemap))]
    public class RemoveOccupantTile : MonoBehaviour
    {
        private Tilemap _tilemap;

        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
        }

        public void RemoveTileNextFrame(GridOccupantBehaviour occupant)
        {
            StartCoroutine(CoRemoveTile(occupant.CurrentNodeIdx));
        }

        private IEnumerator CoRemoveTile(Vector2Int occupantIdx)
        {
            yield return null;
            _tilemap.SetTile((Vector3Int)occupantIdx, null);
        }
    }
}