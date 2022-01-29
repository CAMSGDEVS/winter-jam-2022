using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MachineMovement : MonoBehaviour {

    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private GenerateTiles generateTiles;
    [SerializeField] private Tilemap map, treeMap, baseMap;
    [SerializeField] private TileBase[] tiles = new TileBase[4];
    [SerializeField] private Animator animator;
    [SerializeField] private Text legalText;

    private Vector3Int position = new Vector3Int(0, 0, 0);
    private int direction = 0; // Direction the machine is facing: 0 = frontLeft, 1 = backLeft, 2 = frontRight, 3 = backRight
    public int treeCount = 0;
    private Vector3Int movement;
    private bool isMoving;

    public void Start() {
        map.SetTile(position, tiles[direction]);
        movement = new Vector3Int(0, 1, 0); // Default starting movement

        isMoving = true;
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

    public void Restart() {
        animator.SetTrigger("Close");
        position = new Vector3Int(0, 0, 0);
        movement = new Vector3Int(0, 1, 0); // Default starting movement
        direction = 0;
        map.SetTile(position, tiles[direction]);
        generateTiles.Reset();
        isMoving = true;
    }

    private void Move() {
        if (isMoving) {
            position += movement;
            map.ClearAllTiles();
            map.SetTile(position, tiles[direction]);

            TileBase tree = treeMap.GetTile(position);
            if (tree != null && tree != generateTiles.protester) { // Collision with tree
                treeMap.SetTile(position, null);
                GameManager.treesDestroyed++;
                treeCount--;
                if (treeCount <= 0) {
                    GameManager.year++;
                    GameManager.Instance.ChangeYear(GameManager.year);
                }

            } else if (tree == generateTiles.protester) {
                legalText.text = " - Legal Notice -\n\nYou have been charged with first degree murder. Use money to cover it up?";
                GameManager.protestersKilled++;
                animator.SetTrigger("Open");
                treeMap.SetTile(position, null);
                isMoving = false;

            } else if (baseMap.GetTile(position) == null) {
                legalText.text = " - Legal Notice -\n\nAn executive blames you for damaging company machines in a crash. Fire him to cover it up?";
                animator.SetTrigger("Open");
                isMoving = false;
            }
        }
    }

}
