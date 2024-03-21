using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Vector3 input;
    private Vector3 targetPos;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            input = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            input = Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            input = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            input = Vector3.right;
        } else
        {
            input = Vector3.zero;
        }

        targetPos = transform.position + input;

        

        //move if in grid and no wall ahead
        if (GameManager.instance.checkGrid(targetPos) 
            && GameManager.instance.checkWall(targetPos) 
            && GameManager.instance.checkSmooth(targetPos, input) 
            && GameManager.instance.checkClingy(transform.position, input)
            && GameManager.instance.checkSticky(Vector3.zero,transform.position, input))
        {
            transform.position = targetPos;
        }
    }


}