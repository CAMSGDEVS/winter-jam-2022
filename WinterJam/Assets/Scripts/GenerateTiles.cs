using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GenerateTiles : MonoBehaviour {

    [SerializeField] private Tilemap treeMap, baseMap;
    [SerializeField] private TileBase[] trees;
    [SerializeField] private MachineMovement machineMovement;
    [SerializeField] private float treeDensity, protesterDensity; // Between 0 and 10

    public TileBase protester;

    private List<Vector3Int> allTiles = new List<Vector3Int>();

    private void Start() {
        Init();
        GenerateTrees();
        GenerateProtesters();
    }

    private void Init() {
        allTiles.Clear();
        foreach (Vector3Int position in baseMap.cellBounds.allPositionsWithin) { // Init allTiles
            allTiles.Add(position);
        }
    }

    public void Reset() {
        Init();
        treeMap.ClearAllTiles();
        GenerateTrees();
        GenerateProtesters();
    }

    private void GenerateProtesters() {
        for (int i = 0; i < allTiles.Count; i++) {
            if (Random.Range(0f, 10f) < protesterDensity) {
                treeMap.SetTile(allTiles[i], protester);
                allTiles.Remove(allTiles[i]);
            }
        }
    }

    private void GenerateTrees() {
        for (int i = 0; i < allTiles.Count; i++) {
            if (Random.Range(0f, 10f) < treeDensity) {
                treeMap.SetTile(allTiles[i], trees[Random.Range(0, trees.Length)]);
                allTiles.Remove(allTiles[i]);
                machineMovement.treeCount++;
            }
        }
    }

}
