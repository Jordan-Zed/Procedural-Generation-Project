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
                checkOverlap(gameManager.prefabsL, gameManager, old, 0);
                j++;
                GameManager.tilesCovered += gameManager.tileSize;
            }
            else if (chunks[j].name.Contains("BT") || chunks[j].name.Contains("LT"))
            {
                checkOverlap(gameManager.prefabsB, gameManager, old, 1);
                j++;
                GameManager.tilesCovered += gameManager.tileSize;
             }
            else
            {
                checkOverlap(gameManager.prefabsT, gameManager, old, 2);
                j++;
                GameManager.tilesCovered += gameManager.tileSize;
             }
            if (j == 3)
            {
                branch = true;
            }
            old = chunks[j].transform;
        }
        if (chunks[j].name.Contains("BT") || chunks[j].name.Contains("LT") && GameManager.tilesCovered<=gameManager.width)
        {
            checkOverlap(gameManager.prefabsBR, gameManager, old, 1);
            j++;
            GameManager.tilesCovered += gameManager.tileSize;
        }
        else if (chunks[j].name.Contains("TB") || chunks[j].name.Contains("LB") && GameManager.tilesCovered<=gameManager.width)
        {
            checkOverlap(gameManager.prefabsTR, gameManager, old, 2);
            j++;
            GameManager.tilesCovered += gameManager.tileSize;
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
                GameObject end = Instantiate(gameManager.endTop, new Vector3(old.position.x, old.position.y + gameManager.tileSize, 0), Quaternion.identity);
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
                GameObject end = Instantiate(gameManager.endBottom, new Vector3(old.position.x, old.position.y - gameManager.tileSize, 0), Quaternion.identity);
                float endX = chunks[j].transform.Find("End").position.x;
                float startX = end.transform.Find("Start").position.x;
                float diff = endX - startX;
                //adjust position of next chunk
                end.transform.Translate(new Vector3(diff, 0));
            }
        }
    }

    void checkOverlap(GameObject[] prefabsList, GameManager gameManager, Transform old, int pos)
    {
        bool created = false;
        int loopLimit = 0;
        int i = 0;
        while( i < GameManager.allChunks.Count && loopLimit < 30)
        {
            loopLimit++;
            if (i == 0 && !created)
            {
                if (pos == 0)
                {
                    chunks.Add(Instantiate(prefabsList[Random.Range(0, prefabsList.Length)], new Vector3(old.position.x + gameManager.tileSize, old.position.y, 0), Quaternion.identity));
                    GameManager.allChunks.Add(chunks[j+1]);
                    float endY = chunks[j].transform.Find("End").position.y;
                    float startY = chunks[j + 1].transform.Find("Start").position.y;
                    float diff = endY - startY;
                    //adjust position of next chunk
                    chunks[j + 1].transform.Translate(new Vector3(0, diff));
                }
                else if (pos == 1)
                {
                    chunks.Add(Instantiate(prefabsList[Random.Range(0, prefabsList.Length)], new Vector3(old.position.x, old.position.y + gameManager.tileSize, 0), Quaternion.identity));
                    GameManager.allChunks.Add(chunks[j + 1]);
                    float endX = chunks[j].transform.Find("End").position.x;
                    float startX = chunks[j + 1].transform.Find("Start").position.x;
                    float diff = endX - startX;
                    //adjust position of next chunk
                    chunks[j + 1].transform.Translate(new Vector3(diff, 0));
                }
                else
                {
                    chunks.Add(Instantiate(prefabsList[Random.Range(0, prefabsList.Length)], new Vector3(old.position.x, old.position.y - gameManager.tileSize, 0), Quaternion.identity));
                    GameManager.allChunks.Add(chunks[j + 1]);
                    float endX = chunks[j].transform.Find("End").position.x;
                    float startX = chunks[j + 1].transform.Find("Start").position.x;
                    float diff = endX - startX;
                    //adjust position of next chunk
                    chunks[j + 1].transform.Translate(new Vector3(diff, 0));
                }
            }

            if (((chunks[j + 1].transform.position.x - GameManager.allChunks[i].transform.position.x) < 2) && ((chunks[j + 1].transform.position.y - GameManager.allChunks[i].transform.position.y) < 2))
            {
                GameObject destroyObject = chunks[j + 1];
                chunks.Remove(destroyObject);
                GameManager.allChunks.Remove(destroyObject);
                Destroy(destroyObject);
                i = 0;
                if (pos == 0)
                {
                    chunks.Add(Instantiate(prefabsList[Random.Range(0, prefabsList.Length)], new Vector3(old.position.x + gameManager.tileSize, old.position.y, 0), Quaternion.identity));
                    GameManager.allChunks.Add(chunks[j + 1]);
                    float endY = chunks[j].transform.Find("End").position.y;
                    float startY = chunks[j + 1].transform.Find("Start").position.y;
                    float diff = endY - startY;
                    //adjust position of next chunk
                    chunks[j + 1].transform.Translate(new Vector3(0, diff));
                }
                else if (pos == 1)
                {
                    chunks.Add(Instantiate(prefabsList[Random.Range(0, prefabsList.Length)], new Vector3(old.position.x, old.position.y + gameManager.tileSize, 0), Quaternion.identity));
                    GameManager.allChunks.Add(chunks[j + 1]);
                    float endX = chunks[j].transform.Find("End").position.x;
                    float startX = chunks[j + 1].transform.Find("Start").position.x;
                    float diff = endX - startX;
                    //adjust position of next chunk
                    chunks[j + 1].transform.Translate(new Vector3(diff, 0));
                }
                else
                {
                    chunks.Add(Instantiate(prefabsList[Random.Range(0, prefabsList.Length)], new Vector3(old.position.x, old.position.y - gameManager.tileSize, 0), Quaternion.identity));
                    GameManager.allChunks.Add(chunks[j + 1]);
                    float endX = chunks[j].transform.Find("End").position.x;
                    float startX = chunks[j + 1].transform.Find("Start").position.x;
                    float diff = endX - startX;
                    //adjust position of next chunk
                    chunks[j + 1].transform.Translate(new Vector3(diff, 0));
                }
                created = true;
            }
            else
            {
                i++;
            }
        }
    }
}
