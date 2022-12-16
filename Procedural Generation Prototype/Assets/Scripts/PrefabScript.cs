using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabScript : MonoBehaviour
{
    
    List<GameObject> chunks = new List<GameObject>();
    bool branch = false;
    int j = 0;


    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        chunks.Add(gameObject);
        Transform old = gameObject.transform;

        while (branch == false && GameManager.tilesCovered<=gameManager.width) 
        {
            
            if (chunks[j].name.Contains("BR") || chunks[j].name.Contains("LR") || chunks[j].name.Contains("TR"))
            {
                chunks.Add(Instantiate(gameManager.prefabsL[Random.Range(0, gameManager.prefabsL.Length)], new Vector3(old.position.x + gameManager.tileSize, old.position.y, 0), Quaternion.identity));
                j++;
                GameManager.tilesCovered += gameManager.tileSize;
                float endY = chunks[j - 1].transform.Find("End").position.y;
                float startY = chunks[j].transform.Find("Start").position.y;
                

                float diff = endY - startY;
                //adjust position of next chunk
                chunks[j].transform.Translate(new Vector3(0, diff));
            }
            else if (chunks[j].name.Contains("BT") || chunks[j].name.Contains("LT"))
            {
                chunks.Add(Instantiate(gameManager.prefabsB[Random.Range(0, gameManager.prefabsB.Length)], new Vector3(old.position.x, old.position.y + gameManager.tileSize, 0), Quaternion.identity));
                j++;
                GameManager.tilesCovered += gameManager.tileSize;
                float endX = chunks[j - 1].transform.Find("End").position.x;
                float startX = chunks[j].transform.Find("Start").position.x;

                float diff = endX - startX;
                //adjust position of next chunk
                chunks[j].transform.Translate(new Vector3(diff, 0));
            }
            else
            {
                chunks.Add(Instantiate(gameManager.prefabsT[Random.Range(0, gameManager.prefabsT.Length)], new Vector3(old.position.x, old.position.y - gameManager.tileSize, 0), Quaternion.identity));
                j++;
                GameManager.tilesCovered += gameManager.tileSize;
                float endX = chunks[j - 1].transform.Find("End").position.x;
                float startX = chunks[j].transform.Find("Start").position.x;

                float diff = endX - startX;
                //adjust position of next chunk
                chunks[j].transform.Translate(new Vector3(diff, 0));
            }
            if (j == 3)
            {
                branch = true;
            }
            old = chunks[j].transform;
        }
        if (chunks[j].name.Contains("BT") || chunks[j].name.Contains("LT") && GameManager.tilesCovered<=gameManager.width)
        {
            chunks.Add(Instantiate(gameManager.prefabsBR[Random.Range(0, gameManager.prefabsBR.Length)], new Vector3(old.position.x, old.position.y + gameManager.tileSize, 0), Quaternion.identity));
            j++;
            GameManager.tilesCovered += gameManager.tileSize;
            float endX = chunks[j - 1].transform.Find("End").position.x;
            float startX = chunks[j].transform.Find("Start").position.x;
            
            float diff = endX - startX;
            //adjust position of next chunk
            chunks[j].transform.Translate(new Vector3(diff, 0));
        }
        else if (chunks[j].name.Contains("TB") || chunks[j].name.Contains("LB") && GameManager.tilesCovered<=gameManager.width)
        {
            chunks.Add(Instantiate(gameManager.prefabsTR[Random.Range(0, gameManager.prefabsTR.Length)], new Vector3(old.position.x, old.position.y - gameManager.tileSize, 0), Quaternion.identity));
            j++;
            GameManager.tilesCovered += gameManager.tileSize;
            float endX = chunks[j - 1].transform.Find("End").position.x;
            float startX = chunks[j].transform.Find("Start").position.x;
            
            float diff = endX - startX;
            //adjust position of next chunk
            chunks[j].transform.Translate(new Vector3(diff, 0));
        }

        old = chunks[j].transform;

        if (GameManager.tilesCovered <= gameManager.width)
        {
            GameObject branch = Instantiate(gameManager.branchPrefab[Random.Range(0, gameManager.branchPrefab.Length)], new Vector3(old.position.x + gameManager.tileSize, old.position.y, 0), Quaternion.identity);
            branch.AddComponent<BranchScript>();
            GameManager.tilesCovered += gameManager.tileSize;
            float endY = chunks[j].transform.Find("End").position.y;
            float startY = branch.transform.Find("Start").position.y;
            float diff = endY - startY;
            branch.transform.Translate(new Vector3(0, diff));
        }
        else
        {
            if (chunks[j].name.Contains("BT") || chunks[j].name.Contains("LT") && GameManager.tilesCovered <= gameManager.width)
            {
                GameObject end = Instantiate(gameManager.endTop, new Vector3(old.position.x, old.position.y - gameManager.tileSize, 0), Quaternion.identity);

                float endX = chunks[j].transform.Find("End").position.x;
                float startX = end.transform.Find("Start").position.x;

                float diff = endX - startX;
                //adjust position of next chunk
                end.transform.Translate(new Vector3(diff, 0));
            }
            else if (chunks[j].name.Contains("BR") || chunks[j].name.Contains("LR") || chunks[j].name.Contains("TR"))
            {
                GameObject end = Instantiate(gameManager.endLeft, new Vector3(old.position.x + gameManager.tileSize, old.position.y, 0), Quaternion.identity);

                float endY = chunks[j].transform.Find("End").position.y;
                float startY = end.transform.Find("Start").position.y;


                float diff = endY - startY;
                //adjust position of next chunk
                end.transform.Translate(new Vector3(0, diff));
            }
            else 
            {
                GameObject end = Instantiate(gameManager.endBottom, new Vector3(old.position.x, old.position.y + gameManager.tileSize, 0), Quaternion.identity);

                float endX = chunks[j].transform.Find("End").position.x;
                float startX = end.transform.Find("Start").position.x;

                float diff = endX - startX;
                //adjust position of next chunk
                end.transform.Translate(new Vector3(diff, 0));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
