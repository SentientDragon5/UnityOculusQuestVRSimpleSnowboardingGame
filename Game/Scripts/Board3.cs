/* Snowboarding1 - Logan Shehane
 * 
 * This script is for the board, it changes it's rotation to the ground's normal.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board3 : MonoBehaviour
{
    public Transform rayOrgin = null;
    public float raycastDownDistance = 1f;
    public LayerMask enviromentLayer;
    public Vector3 boardPosition = Vector3.zero;
    public Quaternion boardRotation;
    public GameObject Board = null;
    // Start is called before the first frame update
    void Start()
    {
        Board  = GameObject.Find("Board");
    }

    // Update is called once per frame
    void Update()
    {
        cast();
        Board.transform.position = boardPosition;
        Board.transform.rotation = boardRotation;

    }
    void cast()
    {
        RaycastHit Hit;
        
        if (Physics.Raycast(rayOrgin.position, Vector3.down, out Hit, raycastDownDistance, enviromentLayer))
        {
            boardPosition = rayOrgin.position;
            boardPosition.y = Hit.point.y;
            boardRotation = Quaternion.FromToRotation(Vector3.up, Hit.normal) * transform.rotation;
            return;
        }
        boardPosition = Vector3.zero;
    }
}
