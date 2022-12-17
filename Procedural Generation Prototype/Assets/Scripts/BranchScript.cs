using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchScript : MonoBehaviour
{
    public List<GameObject> chunks;
    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Transform old = gameObject.transform;
        float endTop = gameObject.transform.Find("End (1)").position.x;
        float endBottom = gameObject.transform.Find("End").position.x;
        
        //int j = chunks.Count-1;

        if (GameManager.tilesCovered <= gameManager.width)
        {
            GameObject chunkTop = Instantiate(gameManager.prefabsB[Random.Range(0, gameManager.prefabsB.Length)], new Vector3(old.position.x, old.position.y + gameManager.tileSize, 0), Quaternion.identity) as GameObject;
            float startTopX = chunkTop.transform.Find("Start").position.x;

            float diffTop = endTop - startTopX;
            //adjust position of next chunk
            chunkTop.transform.Translate(new Vector3(diffTop, 0));
            int i = 0;
            int limitLoop = 0;
            while (i < GameManager.allChunks.Count && limitLoop <30)
            {
                limitLoop++;
                if (((chunkTop.transform.position.x - GameManager.allChunks[i].transform.position.x) < 2) && ((chunkTop.transform.position.y - GameManager.allChunks[i].transform.position.y) < 2))
                {
                    Destroy(chunkTop);
                    i = 0;
                    chunkTop = Instantiate(gameManager.prefabsB[Random.Range(0, gameManager.prefabsB.Length)], new Vector3(old.position.x, old.position.y + gameManager.tileSize, 0), Quaternion.identity) as GameObject;
                    startTopX = chunkTop.transform.Find("Start").position.x;

                    diffTop = endTop - startTopX;
                    //adjust position of next chunk
                    chunkTop.transform.Translate(new Vector3(diffTop, 0));
                }
                else 
                {
                    i++;
                }
            }
            chunkTop.AddComponent<PrefabScript>();
            GameManager.tilesCovered += gameManager.tileSize;
            

            GameObject chunkBottom = Instantiate(gameManager.prefabsT[Random.Range(0, gameManager.prefabsT.Length)], new Vector3(old.position.x, old.position.y - gameManager.tileSize, 0), Quaternion.identity) as GameObject;
            chunkBottom.AddComponent<PrefabScript>();
            float startBottomX = chunkBottom.transform.Find("Start").position.x;

            float diffBottom = endBottom - startBottomX;
            //adjust position of next chunk
            chunkBottom.transform.Translate(new Vector3(diffBottom, 0));

            i = 0;
            limitLoop = 0;
            while (i < GameManager.allChunks.Count && limitLoop < 30)
            {
                limitLoop++;
                if (((chunkBottom.transform.position.x - GameManager.allChunks[i].transform.position.x) < 2) && ((chunkBottom.transform.position.y - GameManager.allChunks[i].transform.position.y) < 2))
                {
                    Destroy(chunkBottom);
                    i = 0;
                    chunkBottom = Instantiate(gameManager.prefabsT[Random.Range(0, gameManager.prefabsT.Length)], new Vector3(old.position.x, old.position.y - gameManager.tileSize, 0), Quaternion.identity) as GameObject;
                    startBottomX = chunkBottom.transform.Find("Start").position.x;

                    diffBottom = endBottom - startBottomX;
                    //adjust position of next chunk
                    chunkBottom.transform.Translate(new Vector3(diffBottom, 0));
                }
                else
                {
                    i++;
                }
            }
        }
        else 
        {

                GameObject endTopChunk = Instantiate(gameManager.endTop, new Vector3(old.position.x, old.position.y - gameManager.tileSize, 0), Quaternion.identity);

                float startTopX = endTopChunk.transform.Find("Start").position.x;

                float diffTop = endTop - startTopX;
                //adjust position of next chunk
                endTopChunk.transform.Translate(new Vector3(diffTop, 0));

                GameObject endBottomChunk = Instantiate(gameManager.endBottom, new Vector3(old.position.x, old.position.y + gameManager.tileSize, 0), Quaternion.identity);

                float startBottomX = endBottomChunk.transform.Find("Start").position.x;

                float diffBottom = endBottom - startBottomX;
                //adjust position of next chunk
                endBottomChunk.transform.Translate(new Vector3(diffBottom, 0));
        }
    }
}
