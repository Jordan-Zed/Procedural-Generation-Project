using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Transform old = gameObject.transform;
        float endTop = gameObject.transform.Find("End (1)").position.x;
        float endBottom = gameObject.transform.Find("End").position.x;

        if (GameManager.tilesCovered <= gameManager.width)
        {
            GameObject chunkTop = Instantiate(gameManager.prefabsB[Random.Range(0, gameManager.prefabsB.Length)], new Vector3(old.position.x, old.position.y + gameManager.tileSize, 0), Quaternion.identity) as GameObject;
            chunkTop.AddComponent<PrefabScript>();
            GameManager.tilesCovered += gameManager.tileSize;
            float startTopX = chunkTop.transform.Find("Start").position.x;

            float diffTop = endTop - startTopX;
            //adjust position of next chunk
            chunkTop.transform.Translate(new Vector3(diffTop, 0));

            GameObject chunkBottom = Instantiate(gameManager.prefabsT[Random.Range(0, gameManager.prefabsT.Length)], new Vector3(old.position.x, old.position.y - gameManager.tileSize, 0), Quaternion.identity) as GameObject;
            chunkBottom.AddComponent<PrefabScript>();
            float startBottomX = chunkBottom.transform.Find("Start").position.x;

            float diffBottom = endBottom - startBottomX;
            //adjust position of next chunk
            chunkBottom.transform.Translate(new Vector3(diffBottom, 0));
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
