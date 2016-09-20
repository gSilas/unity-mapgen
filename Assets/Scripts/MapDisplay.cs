using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour
{

	private Transform boardHolder;

	private int cityCounter = 0;

	public Tile[] terrainTiles;
	//Array of terrain prefabs.

	public void DrawTerrainMap (float[,] noiseMap)
	{
		boardHolder = new GameObject ("Map").transform;

		int mwidth = noiseMap.GetLength (0);
		int mheight = noiseMap.GetLength (1);


		for (int y = 0; y < mheight; y++) {
			for (int x = 0; x < mwidth; x++) {

				int index = getTileIndex (noiseMap [x, y]);
				if (terrainTiles [index].name == "grass" && cityCounter <= 200) {
					cityCounter++;
					GameObject toInstantiate = terrainTiles [index].gameObject;
					GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent (boardHolder);
				} else if (cityCounter > 200) {
					cityCounter = 0;
					GameObject toInstantiate = terrainTiles [8].gameObject;
					GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent (boardHolder);
					
				} else {					
					GameObject toInstantiate = terrainTiles [index].gameObject;
					GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent (boardHolder);
				}
			}
		}	
	}

	public int getTileIndex (float noiseValue)
	{
		for (int i = 0; i < terrainTiles.GetLength (0); i++) {
			if (terrainTiles [i].height > noiseValue) {
				return i;
			}
		}
		return 0;
	}
}