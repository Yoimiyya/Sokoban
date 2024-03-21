using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static Unity.Collections.AllocatorManager;
using static UnityEditor.PlayerSettings;

public class Sticky : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public bool move(Vector3 avoid, Vector3 input)
    {
        bool result = false;
        Vector3 target = transform.position + input;
        //check grid and check wall
        if (GameManager.instance.checkGrid(target)
            && GameManager.instance.checkWall(target)
            && GameManager.instance.checkSmooth(target, input)
            && GameManager.instance.checkClingy(transform.position, input)
            && GameManager.instance.checkSticky(avoid, transform.position, input))
        {
            transform.position += input;
            result = true;
        }
        return result;
    }
}
