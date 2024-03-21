using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject[] smooths, clingys, stickys, walls;

    private GridMaker grid;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        grid = GameObject.Find("Grid").GetComponent<GridMaker>();
        smooths = GameObject.FindGameObjectsWithTag("Smooth");
        clingys = GameObject.FindGameObjectsWithTag("Clingy");
        stickys = GameObject.FindGameObjectsWithTag("Sticky");
        walls = GameObject.FindGameObjectsWithTag("Wall");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool checkGrid(Vector3 target)
    {
        bool result = true;
        if (target.x < grid.TopLeft.x || target.y > grid.TopLeft.y || target.x > grid.BottomRight.x || target.y < grid.BottomRight.y)
        {
            result = false;
        }
        return result;
    }

    public bool checkWall(Vector3 target)
    {
        bool result = true;
        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i].transform.position == target)
            {
                result = false;
                break;
            }
        }
        return result;
    }

    public bool checkSmooth(Vector3 target, Vector3 input)
    {
        bool result = true;
        //if smooth ahead
        for (int i = 0; i < smooths.Length; i++)
        {
            if (smooths[i].transform.position == target)
            {
                if (!smooths[i].GetComponent<Smooth>().move(target-input,input))
                {
                    result = false;
                }
                break;
            }
        }
        return result;
    }

    public bool checkClingy(Vector3 pos, Vector3 input)
    {
        bool result = true;
        Vector3 target = pos + input;
        Vector3 back = pos - input;
        //if clingy ahead
        for (int i = 0; i < clingys.Length; i++)
        {
            if (clingys[i].transform.position == target)
            {
                result = false;
                break;
            }
        }
        //if clingy back
        for (int i = 0; i < clingys.Length; i++)
        {
            if (clingys[i].transform.position == back)
            {
                checkClingy(clingys[i].transform.position, input);
                checkSticky(pos, clingys[i].transform.position, input);
                clingys[i].transform.position += input;
                break;
            }
        }
        return result;
    }

    public bool checkSticky(Vector3 avoid, Vector3 pos, Vector3 input)
    {
        bool result = true;
        Vector3 up = pos + Vector3.up;
        Vector3 down = pos + Vector3.down;
        Vector3 left = pos + Vector3.left;
        Vector3 right = pos + Vector3.right;
        Vector3 target = pos + input;
        //if sticky ahead
        for (int i = 0; i < stickys.Length; i++)
        {
            if (stickys[i].transform.position == up
                || stickys[i].transform.position == down
                || stickys[i].transform.position == left
                || stickys[i].transform.position == right)
            {
                if (stickys[i].transform.position != avoid && !stickys[i].GetComponent<Sticky>().move(pos, input) && stickys[i].transform.position == target)
                {
                    result = false;
                }
                break;
            }
        }
        return result;
    }
}
