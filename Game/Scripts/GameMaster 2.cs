/* Snowboarding1 - Logan Shehane
 * This script controlls my dubbed GameMaster, it is the spawner of the slopes for the mountain. 
 * in the inspector you include all of the options, then this randomizes the size, then the shape of the slope.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [Header("Player")]
    public GameObject Player;

    [Header("Render Settings")]
    public float renderDistanceFront = 100;
    public float renderDistanceBack = 100;
    [Range(20f, 120f)] public float Scale = 100;

    [Header("Slope Prefabs")]
    public List<GameObject> WhiteBiome0 = new List<GameObject>();
    public List<GameObject> WhiteBiome1 = new List<GameObject>();
    public List<GameObject> WhiteBiome2 = new List<GameObject>();

    [Header("Obstacle Prefabs")]
    public List<GameObject> Obstacle = new List<GameObject>();

    private int heightOfNewChunk;
    private int heightOfLastChunk;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += Vector3.up * 10 * Scale;
        Instantiate(WhiteBiome1[0], new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        transform.position -= Vector3.up * 10 * Scale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.z > transform.position.z - renderDistanceFront)
        {
            heightOfNewChunk = Random.Range(1, 3);
            if ( heightOfNewChunk == 0)
            {
                transform.position += (new Vector3(0f, 0f - (10f * (float)heightOfLastChunk), 160) * Scale);
                Instantiate(WhiteBiome0[Random.Range(0, WhiteBiome0.Count)], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            else if (heightOfNewChunk == 1)
            {
                transform.position += (new Vector3(0f, -10f - (10f * (float)heightOfLastChunk), 160) * Scale);
                Instantiate(WhiteBiome1[Random.Range(0, WhiteBiome1.Count)], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            else if (heightOfNewChunk == 2)
            {
                transform.position += (new Vector3(0f, -20f - (10f * (float)heightOfLastChunk), 160) * Scale);
                Instantiate(WhiteBiome2[Random.Range(0, WhiteBiome2.Count)], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            heightOfLastChunk = heightOfNewChunk;
        }
    }
}