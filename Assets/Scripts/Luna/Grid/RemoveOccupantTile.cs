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
            // we need to wait a frame to remove it so we dont get 2 instances of Destroy called on the same game object
            // on the same frame
            yield return null;
            _tilemap.SetTile((Vector3Int)occupantIdx, null);
        }
    }
}