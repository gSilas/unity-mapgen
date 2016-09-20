using UnityEngine;
using System;
using System.Collections;

public class MapGenerator : MonoBehaviour
{

	public int mapWidth;
	public int mapHeight;
	public float noiseScale;

	public int octaves;
	[Range (0, 1)]
	public float persistence;
	public float lacunarity;

	public int seed;
	public Vector2 offset;

	public bool autoUpdate;

	public void GenerateMap ()
	{
		DestroyImmediate (GameObject.Find ("Map"));
		float[,] noiseMap = Noise.GenerateNoiseMap (mapWidth, mapHeight, seed, noiseScale, octaves, persistence, lacunarity, offset);

		MapDisplay display = FindObjectOfType<MapDisplay> ();
		display.DrawTerrainMap (noiseMap);
	}

	public void SeedChanged (string sseed)
	{
		seed = Convert.ToInt32 (sseed);
	}

	//Initializes the game for each level.
	void Start ()
	{
		GenerateMap ();
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.G))
			GenerateMap ();
		if (Input.GetKeyDown (KeyCode.W)) {
			offset.y+=0.01f;
			GenerateMap ();
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			offset.x-=0.01f;
			GenerateMap ();
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			offset.y-=0.01f;
			GenerateMap ();
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			offset.x+=0.01f;
			GenerateMap ();
		}
	}

	void OnValidate ()
	{
		if (mapWidth < 1)
			mapWidth = 1;

		if (mapHeight < 1)
			mapHeight = 1;

		if (lacunarity < 1)
			lacunarity = 1;

		if (octaves < 0)
			octaves = 0;
	}
}