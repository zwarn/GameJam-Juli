using UnityEngine;
using System.Collections.Generic;

public enum Direction
{
	UP,
	DOWN, 
	LEFT, 
	RIGHT,
	FRONT,
	BACK
}

public enum Tile
{
	CUBE,
	TUBE,
	ROOM,
	GATE,
	OPEN_GATE
}

public struct Point
{
	public int x;
	public int y;
	public int z;			
	
	public Point (int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}
	
	public Vector3 ToVector ()
	{
		return new Vector3 (x * 3f, y * 3f, z * 3f);
	}
	
	public override string ToString ()
	{
		return string.Format ("Point {0}:{1}:{2}", x, y, z);
	}
}

public struct LevelElement
{
	public Direction dir;
	public Tile tile;
	public Point pos;
}

[ExecuteInEditMode]
public class LevelGenerator : MonoBehaviour
{
	public float RoomProb = 0.2f;
	public float GateProb = 0.6f;
	
	public GameObject Tube;
	public GameObject Cube;
	public GameObject Room;
	public GameObject ClosedGate;
	public GameObject OpenGate;

	private List<LevelElement> elements = new List<LevelElement> ();
	private List<LevelElement> openEnds = new List<LevelElement> ();
	private HashSet<Point> occupied = new HashSet<Point> ();
	
	private Tile RandomTile ()
	{	
		if (Random.value < GateProb) {
			return Tile.GATE;
		} else if (Random.value < RoomProb) {
			return Tile.ROOM;
		} else {
			return Tile.CUBE;
		}
	}
	
	public Point GoInDir (Point p, Direction dir, int steps = 1)
	{
		switch (dir) {
		case Direction.BACK:
			p.y -= steps;
			break;
		case Direction.FRONT:
			p.y += steps;
			break;			
		case Direction.UP:
			p.x += steps;
			break;
		case Direction.DOWN:
			p.x -= steps;
			break;			
		case Direction.LEFT:
			p.z -= steps;
			break;
		case Direction.RIGHT:
			p.z += steps;
			break;
		}			
		return p;
	}
	
	public T RandomElement<T> (List<T> coll)
	{
		return coll [Random.Range (0, coll.Count)];		
	}
	
	public List<Point> OccupiedBy (LevelElement elt)
	{
		List<Point> ret = new List<Point> ();
		switch (elt.tile) {
		case Tile.GATE:			
			break;
		case Tile.TUBE:
			ret.Add (elt.pos);
			ret.Add (GoInDir (elt.pos, elt.dir));
			ret.Add (GoInDir (elt.pos, elt.dir, 2));
			break;
		case Tile.CUBE:
			ret.Add (elt.pos);			
			break;
		case Tile.ROOM:
			var center = GoInDir (elt.pos, elt.dir);
			for (int x = center.x - 1; x < center.x+2; x++) {
				for (int y = center.y - 1; y < center.y+2; y++) {
					for (int z = center.z - 1; z < center.z+2; z++) {
						ret.Add (new Point (x, y, z));
					}	
				}
			}
			break;
		}
		return ret;
	}
	
	public bool Fits (LevelElement elt)
	{
		foreach (var p in OccupiedBy(elt)) {
			if (occupied.Contains (p)) {
				return false;
			}				
		}
		return true;
	}
	
	private GameObject GetPrefab (Tile tile)
	{
		switch (tile) {
		case Tile.CUBE:
			return Cube;
		case Tile.TUBE:
			return Tube;
		case Tile.ROOM:
			return Room;
		case Tile.GATE:
			return ClosedGate;
		case Tile.OPEN_GATE:
			return OpenGate;
		default:
			return null;
		}
	}
	
	
	public Vector3 Center (LevelElement elt)
	{
		switch (elt.tile) {
		case Tile.CUBE:
			return elt.pos.ToVector ();			
		case Tile.TUBE:
		case Tile.ROOM:
			return GoInDir (elt.pos, elt.dir).ToVector ();
		case Tile.GATE:
		case Tile.OPEN_GATE:
			return (elt.pos.ToVector () + GoInDir (elt.pos, elt.dir, -1).ToVector ()) / 2;
		default:
			return new Vector3 ();
		}
	}
	
	public Quaternion Rotation (Direction dir)
	{
		Vector3 from = new Vector3 (0, 1, 0);
		Vector3 to = new Vector3 ();
		switch (dir) {
		case Direction.BACK:
			to = new Vector3 (0, -1, 0);
			break;
		case Direction.FRONT:
			to = new Vector3 (0, 1, 0);
			break;			
		case Direction.UP:
			to = new Vector3 (1, 0, 0);
			break;
		case Direction.DOWN:
			to = new Vector3 (-1, 0, 0);
			break;			
		case Direction.LEFT:
			to = new Vector3 (0, 0, -1);
			break;
		case Direction.RIGHT:
			to = new Vector3 (0, 0, 1);
			break;
		}			
		return Quaternion.FromToRotation (from, to);
	}
	
	public void AddReally (LevelElement elt)
	{
		GameObject obj = GetPrefab (elt.tile);
		Vector3 pos = Center (elt);
		Quaternion rot = Rotation (elt.dir);
		GameObject o = (GameObject)(Instantiate (obj, pos, rot));
		o.transform.parent = GameObject.Find ("Level").transform;
	}
	
	public void AddElement (LevelElement elt)
	{
		print ("Add: " + elt.tile + " " + elt.pos + " " + elt.dir);
		this.elements.Add (elt);
		foreach (var p in OccupiedBy(elt)) {
			occupied.Add (p);
		}
		AddReally (elt);
	}
	
	public Point TubeExit (LevelElement elt)
	{
		if (elt.tile != Tile.TUBE) {
			throw new UnityException ("must be tube");
		}
		return GoInDir (elt.pos, elt.dir, 3);
	}
	
	public List<LevelElement> Exits (LevelElement elt)
	{
		Point ignoreStart = GoInDir (elt.pos, elt.dir, -1);
		Point center = (elt.tile == Tile.CUBE) ? elt.pos : GoInDir (elt.pos, elt.dir, 1);
		int steps = (elt.tile == Tile.CUBE) ? 1 : 2;
		List<LevelElement> ret = new List<LevelElement> ();
		Direction[] allDirs = {
			Direction.BACK,
			Direction.DOWN,
			Direction.FRONT,
			Direction.LEFT,
			Direction.RIGHT,
			Direction.UP
		};
		foreach (var dir in allDirs) {
			LevelElement open = new LevelElement ();
			open.pos = GoInDir (center, dir, steps);
			open.dir = dir;
			if (!ignoreStart.Equals (open.pos)) {
				ret.Add (open);
			}
		}
		return ret;
	}
	
	public void Step ()
	{
		LevelElement nextOpenEnd = RandomElement<LevelElement> (openEnds); 
		openEnds.Remove (nextOpenEnd);
		Tile nextTile = RandomTile ();		
		LevelElement nextElt = nextOpenEnd;
		if (nextTile == Tile.GATE) {
			nextElt.tile = nextTile;
		} else {
			nextElt.tile = Tile.TUBE;
			LevelElement nextRoom = new LevelElement ();
			nextRoom.pos = TubeExit (nextElt);
			nextRoom.dir = nextElt.dir;
			nextRoom.tile = nextTile;
			if (Fits (nextElt) && Fits (nextRoom)) {
				
				AddElement (nextRoom);
				foreach (var exit in Exits(nextRoom)) {
					openEnds.Add (exit);
				}
			} else {
				nextElt.tile = Tile.GATE;
			}
		}
		AddElement (nextElt);
	}

	void Clear ()
	{
		GameObject level = GameObject.Find ("Level");
		foreach (Transform child in level.transform) {			
			GameObject.DestroyImmediate (child.gameObject);
		}
		
		this.elements.Clear ();
		this.openEnds.Clear ();
		this.occupied.Clear ();
	}

	void Gen ()
	{
		Clear ();
		LevelElement start = new LevelElement ();
		start.dir = Direction.FRONT;		
		openEnds.Add (start);			
		var p = GateProb;
		GateProb = 0f;
		for (int i = 0; i < 4; i++) {
			Step ();
		}	
		GateProb = p;
		for (int i = 0; i < 30; i++) {
			Step ();
		}	
		GateProb = 1;
		while (openEnds.Count > 0) {
			Step ();
		}
	}
	
	public bool Generate = false;
	public bool ClearAll = false;

	
	// Update is called once per frame
	void Update ()
	{
		if (Generate) {
			Generate = false;
			Gen ();
		}
		if (ClearAll) {
			ClearAll = false;
			Clear ();
		}
	}
}
