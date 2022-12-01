using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //get reference to chunk game objects
    public GameObject block1;
    public GameObject block2;
    public GameObject block3;

    //int for width of chunks to be spawned
    public int width = 10;
    //size of a tile (used to prevent overlap)
    public int tileSize;

    // Start is called before the first frame update
    void Start()
    {
        //List containing all the prefab chunks from unity
        List<GameObject> prefabs = new List<GameObject>();
        prefabs.Add(block1);
        prefabs.Add(block2);
        prefabs.Add(block3);
        
        //list of chunks to be created
        List<GameObject> chunks = new List<GameObject>();
        //loop to create chunks
            for (int x = 0; x < width; x=x+tileSize)
            {
            chunks.Add(Instantiate(prefabs[Random.Range(0, prefabs.Count)], new Vector3(x, 0, 0), Quaternion.identity));
            }
        //oldDiff holds the offset of chunks
        float oldDiff =0;
        //loop to move chunks
        for (int i = 0; i < chunks.Count; i++) 
        {
            //if statement to prevent index error on last chunk
            if (i+1 < chunks.Count) 
            {
                //get the coordinates of the end from the current chunk
                Transform end = chunks[i].transform.Find("End");
                //get coordinates of the start of the next chunk
                Transform start = chunks[i + 1].transform.Find("Start");
                //store y coordinate of end, and add offset of oldDiff
                float endY = end.position.y + oldDiff;
                //find difference of end y position and start y position
                float diff = endY - start.position.y - oldDiff;
                //update oldDiff
                oldDiff = diff;

                //adjust position of next chunk
                chunks[i+1].transform.Translate(new Vector3(0, diff));
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
