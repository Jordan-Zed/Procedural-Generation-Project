using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //List containing all the prefab chunks from unity
    public GameObject[] prefabs;
    public GameObject[] prefabsBR;
    public GameObject[] prefabsLR;
    public GameObject[] prefabsTR;
    public GameObject[] prefabsBT;
    public GameObject[] prefabsLT;
    public GameObject[] prefabsLB;
    public GameObject[] prefabsTB;

    public GameObject[] prefabsB;
    public GameObject[] prefabsT;
    public GameObject[] prefabsL;

    public GameObject endTop;
    public GameObject endLeft;
    public GameObject endBottom;

    public GameObject[] branchPrefab;


    //int for width of chunks to be spawned
    public int width;
    //size of a tile (used to prevent overlap)
    public int tileSize;
    public static int tilesCovered = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject chunk = Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        chunk.AddComponent<PrefabScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
