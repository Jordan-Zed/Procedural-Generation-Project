using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //get reference to chunk game objects
    public GameObject brBlock1;
    public GameObject brBlock2;
    public GameObject brBlock3;
    public GameObject btBlock1;
    public GameObject btBlock2;
    public GameObject btBlock3;
    public GameObject lrBlock1;
    public GameObject lrBlock2;
    public GameObject lrBlock3;
    public GameObject ltBlock1;
    public GameObject lbBlock1;
    public GameObject trBlock1;

    //int for width of chunks to be spawned
    public int width = 10;
    //size of a tile (used to prevent overlap)
    public int tileSize;

    // Start is called before the first frame update
    void Start()
    {
        //List containing all the prefab chunks from unity
        List<GameObject> prefabs = new List<GameObject>();
        List<GameObject> prefabsBR = new List<GameObject>();
        List<GameObject> prefabsLR = new List<GameObject>();
        List<GameObject> prefabsTR = new List<GameObject>();
        List<GameObject> prefabsBT = new List<GameObject>();
        List<GameObject> prefabsLT = new List<GameObject>();
        List<GameObject> prefabsLB = new List<GameObject>();
        List<GameObject> prefabsTB = new List<GameObject>();
        
        List<GameObject> prefabsB = new List<GameObject>();
        List<GameObject> prefabsT = new List<GameObject>();
        List<GameObject> prefabsL = new List<GameObject>();

        //contain all prefabs
        prefabs.Add(brBlock1);
        prefabs.Add(brBlock2);
        prefabs.Add(brBlock3);
        prefabs.Add(btBlock1);
        prefabs.Add(btBlock2);
        prefabs.Add(btBlock3);
        prefabs.Add(lrBlock1);
        prefabs.Add(lrBlock2);
        prefabs.Add(lrBlock3);
        prefabs.Add(ltBlock1);
        prefabs.Add(lbBlock1);
        prefabs.Add(trBlock1);


        //contains seperated prefabs
        prefabsBR.Add(brBlock1);
        prefabsBR.Add(brBlock2);
        prefabsBR.Add(brBlock3);
        prefabsBT.Add(btBlock1);
        prefabsBT.Add(btBlock2);
        prefabsBT.Add(btBlock3);
        prefabsLR.Add(lrBlock1);
        prefabsLR.Add(lrBlock2);
        prefabsLR.Add(lrBlock3);
        prefabsLT.Add(ltBlock1);
        prefabsLB.Add(lbBlock1);
        prefabsTR.Add(trBlock1);


        //contains prefabs filtered by "START"
        prefabsB.Add(brBlock1);
        prefabsB.Add(brBlock2);
        prefabsB.Add(brBlock3);
        prefabsB.Add(btBlock1);
        prefabsB.Add(btBlock2);
        prefabsB.Add(btBlock3);
        prefabsL.Add(lrBlock1);
        prefabsL.Add(lrBlock2);
        prefabsL.Add(lrBlock3);
        prefabsL.Add(ltBlock1);
        prefabsL.Add(lbBlock1);
        prefabsT.Add(trBlock1);
        
        //list of chunks to be created
        List<GameObject> chunks = new List<GameObject>();
        
        int j = 0;
        for (int x = 0; x < width; x = x + tileSize)
        {
            
            if (x == 0)
            {
                chunks.Add(Instantiate(prefabs[Random.Range(0, prefabs.Count)], new Vector3(x, 0, 0), Quaternion.identity));
            }
            else
            {
                Transform old = chunks[j].transform;
                if (chunks[j].name.Contains("BR") || chunks[j].name.Contains("LR") || chunks[j].name.Contains("TR"))
                {
                    chunks.Add(Instantiate(prefabsL[Random.Range(0, prefabsL.Count)], new Vector3(old.position.x + tileSize, old.position.y, 0), Quaternion.identity));
                    j++;

                    float endY = chunks[j-1].transform.Find("End").position.y;
                    float startY = chunks[j].transform.Find("Start").position.y;

                    float diff = endY - startY;
                    //adjust position of next chunk
                    chunks[j].transform.Translate(new Vector3(0, diff));
                }
                else if (chunks[j].name.Contains("BT") || chunks[j].name.Contains("LT"))
                {
                    chunks.Add(Instantiate(prefabsB[Random.Range(0, prefabsB.Count)], new Vector3(old.position.x, old.position.y + tileSize, 0), Quaternion.identity));
                    j++;

                    float endX = chunks[j - 1].transform.Find("End").position.x;
                    float startX = chunks[j].transform.Find("Start").position.x;

                    float diff = endX - startX;
                    //adjust position of next chunk
                    chunks[j].transform.Translate(new Vector3(diff, 0));
                }
                else
                {
                    chunks.Add(Instantiate(prefabsT[Random.Range(0, prefabsT.Count)], new Vector3(old.position.x, old.position.y - tileSize, 0), Quaternion.identity));
                    j++;

                    float endX = chunks[j - 1].transform.Find("End").position.x;
                    float startX = chunks[j].transform.Find("Start").position.x;

                    float diff = endX - startX;
                    //adjust position of next chunk
                    chunks[j].transform.Translate(new Vector3(diff, 0));
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
