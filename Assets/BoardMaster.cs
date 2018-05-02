using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMaster : MonoBehaviour {
	public GameObject StonePrefab;
	public GameObject Board;
	public List<GameObject> Stones;
	public int[] Map;
	const int OCT = 8;

	// Use this for initialization
	void Start () {
		Map = new int[]{
			1, 1, 1, 1, 1, 1 , 1, 1 ,
			1, 1, 1, 1, 1, 1 , 1, 1 , 
			1, 1, 1, 1, 1, 1 , 1, 1 , 
			1, 1, 1, 1, 1, 1 , 1, 1 , 
			1, 1, 1, 1, 1, 1 , 1, 1 , 
			1, 1, 1, 1, 1, 1 , 1, 1 , 
			1, 1, 1, 1, 1, 1 , 1, 1 , 
			1, 1, 1, 1, 1, 1 , 1, 1 , 
			};
		GenerateBoard ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateBoard(){
		float width = Board.GetComponent<SpriteRenderer> ().bounds.size.x;
		float cell = width / OCT; 
		float BaseX= Board.transform.position.x-(width/2)+(cell/2);
		float BaseY= Board.transform.position.y+(width/2)-(cell/2);
		DestroyStone ();
		for (int i = 0; i < Map.Length; i++) {
			if(Map[i]==0){
				continue;
			}
			float x=(BaseX) + (i % OCT) * cell;
			float y=(BaseY) - Mathf.Floor(i / OCT)*cell;
			GameObject stone = Instantiate (StonePrefab) as GameObject;
			var sc = stone.GetComponent<StoneController> ();
			sc.Number = Map [i];
			sc.Position = i;
			stone.transform.position = new Vector2 (x,y);
			Stones.Add (stone);
		}
	}

	/// <summary>
	/// 石を全部取り除く
	/// </summary>
	void DestroyStone(){
		foreach (var stone in Stones) {
			Destroy (stone);
		}
		Stones.Clear ();
	}

}
