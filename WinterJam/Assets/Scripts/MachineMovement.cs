using UnityEngine;
using UnityEngine.Tilemaps;

public class MachineMovement : MonoBehaviour {

    [SerializeField] private float moveSpeed = 2;

    [SerializeField] private Tilemap map;
    [SerializeField] private TileBase[] tiles = new TileBase[4];

    private Vector3Int position = new Vector3Int(0, 0, 0);
    private int direction = 0; // Direction the machine is facing: 0 = frontLeft, 1 = backLeft, 2 = frontRight, 3 = backRight
    private Vector3Int movement;

    public void Start() {
        map.SetTile(position, tiles[direction]);
        movement = new Vector3Int(0, 1, 0); // Default starting movement

        InvokeRepeating("Move", 1/moveSpeed, 1/moveSpeed);
    }

    public void Update() {
        
        if (Input.GetKey(KeyCode.UpArrow)) {
            direction = 0;
            movement = new Vector3Int(0, 1, 0);
        }
        
        if (Input.GetKey(KeyCode.RightArrow)) {
            direction = 2;
            movement = new Vector3Int(1, 0, 0);
        }
        
        if (Input.GetKey(KeyCode.DownArrow)) {
            direction = 3;
            movement = new Vector3Int(0, -1, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            direction = 1;
            movement = new Vector3Int(-1, 0, 0);
        }

    }

    private void Move() {
        position += movement;
        map.ClearAllTiles();
        map.SetTile(position, tiles[direction]);
    }

}