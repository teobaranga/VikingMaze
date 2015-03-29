using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {
	[System.Serializable]
	public class Cell {
		public bool visited;
		public GameObject north, south, west, east;
	}

	public GameObject wall;
	public GameObject vik;
	public float wallLength = 1.0f;
	public int xSize = 5;
	public int ySize = 5;
	public List<Vector3> cellCentres;

	private Vector3 initialPos;
	private GameObject wallHolder;
	private Cell[] cells;
	private int visitedCells = 0;
	private bool startedBuilding = false;
	private int currentNeighbor;
	private List<int> lastCells;
	private int backingUp = 0;
	private int wallToBreak = 0;
	public int currentCell = 0;

	// Use this for initialization
	void Start () {
		CreateWalls ();
		Instantiate(vik, new Vector3 (-5.5f, 2f, -5.0f), new Quaternion(0, 1, 0, 1));

	}

	void CreateWalls() {
		wallHolder = new GameObject ();
		wallHolder.name = "Maze";

		initialPos = new Vector3 ((-xSize / 2) + wallLength / 2, 0.0f, (-ySize / 2) + wallLength / 2);
		Vector3 myPos = initialPos;
		GameObject tempWall;

		// For x axis
		for (int i = 0; i < ySize; i++) {
			for (int j = 0; j <= xSize; j++) {
				myPos = new Vector3(initialPos.x + (j*wallLength)-wallLength/2, 0.0f, initialPos.z +(i*wallLength)-wallLength/2);
				tempWall = Instantiate(wall, myPos, Quaternion.identity) as GameObject;
				tempWall.transform.parent = wallHolder.transform;
			}
		}

		// For y axis
		for (int i = 0; i <= ySize; i++) {
			for (int j = 0; j < xSize; j++) {
				myPos = new Vector3(initialPos.x + (j*wallLength), 0.0f, initialPos.z +(i*wallLength)-wallLength);
				tempWall = Instantiate(wall, myPos, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
				tempWall.transform.parent = wallHolder.transform;
			}
		}
		Vector3 v1 = new Vector3 (-6, 0, -6);
		Vector3 v2 = new Vector3 (-6,0,-4);
		Vector3 v3 = new Vector3 (-7,0,-5);

		tempWall = Instantiate(wall, v3, Quaternion.identity) as GameObject;
		tempWall = Instantiate(wall, v1, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
		tempWall = Instantiate(wall, v2, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;

		CreateCells ();
	}

	void CreateCells(){
		lastCells = new List<int>();
		lastCells.Clear();
		GameObject[] allWalls;
		int children = wallHolder.transform.childCount;
		allWalls = new GameObject[children];
		cells = new Cell[xSize * ySize];
		int eastWestProcess = 0;
		int childProcess = 0;
		int termCount = 0;

		// Gets all the childre
		for (int i = 0; i < children; i++) {
			allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
		}
		for (int cellprocess = 0; cellprocess < cells.Length; cellprocess++) {

			if (termCount == xSize) {
				eastWestProcess ++;
				termCount = 0;
			}

			cells[cellprocess] = new Cell();
			cells[cellprocess].east = allWalls[eastWestProcess];
			cells[cellprocess].south = allWalls[childProcess + (xSize+1)*ySize];

				eastWestProcess++;

			termCount++;
			childProcess++;
			
			cells[cellprocess].west = allWalls[eastWestProcess];
			cells[cellprocess].north = allWalls[(childProcess + (xSize+1)*ySize)+xSize-1];
		}
		for (int i=0; i<cells.Length; i++) {
			double y_coord = cells[i].east.transform.position.x;
			double x_coord = cells[i].north.transform.position.z;
			cellCentres.Add (new Vector3 ((float) x_coord, 0f, (float) y_coord));
		}
		CreateMaze ();
	}

	void CreateMaze(){
		while (visitedCells < xSize * ySize) {
			if (startedBuilding) {
				GetNeighbor();
				if (cells[currentNeighbor].visited == false && cells[currentCell].visited == true) {
					BreakWall();
					cells[currentNeighbor].visited = true;
					visitedCells++;
					lastCells.Add(currentCell);
					currentCell = currentNeighbor;
					if (lastCells.Count > 0) {
						backingUp = lastCells.Count - 1;
					}
				}
			}
			else{
				currentCell = Random.Range(0, xSize*ySize);
				cells[currentCell].visited = true;
				visitedCells++;
				startedBuilding = true;
			}
		}
		Destroy (wallHolder.transform.GetChild(0).gameObject);
		Destroy (wallHolder.transform.GetChild(wallHolder.transform.childCount - 1).gameObject);
	}

	void GetNeighbor(){
		int length = 0;
		int[] neighbors = new int[4];
		int[] connectingWalls = new int[4];
		int check = (currentCell +1) / xSize;
		check -= 1;
		check *= xSize;
		check += xSize;

		// west
		if (currentCell + 1 < xSize * ySize && ((currentCell + 1) != check)){
			if (cells [currentCell + 1].visited == false) {
				neighbors [length] = currentCell + 1;
				connectingWalls[length] = 3;
				length++;
			}
		}

		// east
		if (currentCell - 1 >= 0 && currentCell != check){
			if (cells [currentCell - 1].visited == false) {
				neighbors [length] = currentCell - 1;
				connectingWalls[length] = 2;
				length++;
			}
		}

		// north
		if (currentCell + xSize < xSize * ySize){
			if (cells [currentCell + xSize].visited == false) {
				neighbors [length] = currentCell + xSize;
				connectingWalls[length] = 1;
				length++;
			}
		}

		// south
		if (currentCell - xSize >= 0){
			if (cells [currentCell - xSize].visited == false) {
				neighbors [length] = currentCell - xSize;
				connectingWalls[length] = 4;
				length++;
			}
		}

		if (length != 0) {
			int result = Random.Range (0, length);
			currentNeighbor = neighbors [result];
			wallToBreak = connectingWalls[result];
		} else {
			if (backingUp > 0) {
				currentCell = lastCells[backingUp];
				backingUp--;
			}
		}

	}

	void BreakWall(){
		switch (wallToBreak) {
		case 1:
			Destroy(cells[currentCell].north);break;
		case 2:
			Destroy(cells[currentCell].east);break;
		case 3:
			Destroy(cells[currentCell].west);break;
		case 4:
			Destroy(cells[currentCell].south);break;
		default:
			break;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
