/* Snowboarding1 - Logan Shehane
 * This script spawns a rock or other obstacle on the slope at a random X value.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsacleSpawner : MonoBehaviour
{
    [Header("Bounds")]
    public float xBound = 0f;

    private GameObject GameMaster;
    // Start is called before the first frame update
    void Start()
    {
        GameMaster = GameObject.Find("GameMaster");
        GenerateObstacle(xBound);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.name + " + " + GameMaster.GetComponent<GameMaster>().Obstacle[0].transform.name);

    }
    void GenerateObstacle(float LocalxBound)
    {
        float xPos = 0f;
        xPos = Random.Range(-LocalxBound, LocalxBound);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        Instantiate(GameMaster.GetComponent<GameMaster>().Obstacle[Random.Range(0, GameMaster.GetComponent<GameMaster>().Obstacle.Count)], new Vector3(xPos, transform.position.y, transform.position.z), transform.rotation, transform);
    }
}
