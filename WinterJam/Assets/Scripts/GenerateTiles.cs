using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GenerateTiles : MonoBehaviour {

    [SerializeField] private Tilemap treeMap, baseMap;
    [SerializeField] private TileBase tree;
    [SerializeField] private float density; // Between 0 and 10

    private List<Vector3Int> openTiles = new List<Vector3Int>();

    private void Start() {
        foreach (Vector3Int position in baseMap.cellBounds.allPositionsWithin) {
            openTiles.Add(position);
        }

        GenerateTrees();
    }

    private void GenerateTrees() {
        foreach (Vector3Int pos in openTiles) {
            if (Random.Range(0f, 10f) < density) {
                treeMap.SetTile(pos, tree);
                openTiles.Remove(pos); // Tile is no longer open
            }
        }
    }

}
