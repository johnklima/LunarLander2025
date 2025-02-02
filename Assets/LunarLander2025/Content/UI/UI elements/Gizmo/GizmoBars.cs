using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoBars : MonoBehaviour
{
    //The lunar lander being tracked
    public Object lunarLander;
    public float landerSpeedZ = 0.0f;
    public float landerSpeedX = 0.0f;
    public float landerSpeedY = 0.0f;

    //Speeds at which directional bars appear
    public float bar1 = 1.0f;
    public float bar2 = 2.0f;
    public float bar3 = 3.0f;
    public float bar4 = 4.0f;

    //bars of the gizmo
    private GameObject westBar1;
    private GameObject westBar2;
    private GameObject westBar3;
    private GameObject westBar4;
    private GameObject eastBar1;
    private GameObject eastBar2;
    private GameObject eastBar3;
    private GameObject eastBar4;
    private GameObject northBar1;
    private GameObject northBar2;
    private GameObject northBar3;
    private GameObject northBar4;
    private GameObject southBar1;
    private GameObject southBar2;
    private GameObject southBar3;
    private GameObject southBar4;
    private GameObject upBar1;
    private GameObject upBar2;
    private GameObject upBar3;
    private GameObject upBar4;
    private GameObject downBar1;
    private GameObject downBar2;
    private GameObject downBar3;
    private GameObject downBar4;



    // Start is called before the first frame update
    void Start()
    {
        // getting all the gizmo bars GameObjects
        westBar1 = gameObject.transform.GetChild(2).gameObject;
        westBar2 = gameObject.transform.GetChild(3).gameObject;
        westBar3 = gameObject.transform.GetChild(4).gameObject;
        westBar4 = gameObject.transform.GetChild(5).gameObject;
        eastBar1 = gameObject.transform.GetChild(6).gameObject;
        eastBar2 = gameObject.transform.GetChild(7).gameObject;
        eastBar3 = gameObject.transform.GetChild(8).gameObject;
        eastBar4 = gameObject.transform.GetChild(9).gameObject;
        northBar1 = gameObject.transform.GetChild(10).gameObject;
        northBar2 = gameObject.transform.GetChild(11).gameObject;
        northBar3 = gameObject.transform.GetChild(12).gameObject;
        northBar4 = gameObject.transform.GetChild(13).gameObject;
        southBar1 = gameObject.transform.GetChild(14).gameObject;
        southBar2 = gameObject.transform.GetChild(15).gameObject;
        southBar3 = gameObject.transform.GetChild(16).gameObject;
        southBar4 = gameObject.transform.GetChild(17).gameObject;
        upBar1 = gameObject.transform.GetChild(18).gameObject;
        upBar2 = gameObject.transform.GetChild(19).gameObject;
        upBar3 = gameObject.transform.GetChild(20).gameObject;
        upBar4 = gameObject.transform.GetChild(21).gameObject;
        downBar1 = gameObject.transform.GetChild(22).gameObject;
        downBar2 = gameObject.transform.GetChild(23).gameObject;
        downBar3 = gameObject.transform.GetChild(24).gameObject;
        downBar4 = gameObject.transform.GetChild(25).gameObject;
}

// Update is called once per frame
void Update()
    {
        // lander's movement in Z axis 
        if (bar4 * -1 > landerSpeedZ)
        {
            Debug.Log("Flying South over 4");
            northBar1.SetActive(false);
            northBar2.SetActive(false);
            northBar3.SetActive(false);
            northBar4.SetActive(false);
            southBar1.SetActive(true);
            southBar2.SetActive(true);
            southBar3.SetActive(true);
            southBar4.SetActive(true);
        }
        else if (bar3 * -1 > landerSpeedZ)
        {
            Debug.Log("Flying South at 3");
            northBar1.SetActive(false);
            northBar2.SetActive(false);
            northBar3.SetActive(false);
            northBar4.SetActive(false);
            southBar1.SetActive(true);
            southBar2.SetActive(true);
            southBar3.SetActive(true);
            southBar4.SetActive(false);
        }
        else if (bar2 * -1 > landerSpeedZ)
        {
            Debug.Log("Flying South at 2");
            northBar1.SetActive(false);
            northBar2.SetActive(false);
            northBar3.SetActive(false);
            northBar4.SetActive(false);
            southBar1.SetActive(true);
            southBar2.SetActive(true);
            southBar3.SetActive(false);
            southBar4.SetActive(false);
        }
        else if (bar1 * -1 > landerSpeedZ)
        {
            Debug.Log("Flying South at 1");
            northBar1.SetActive(false);
            northBar2.SetActive(false);
            northBar3.SetActive(false);
            northBar4.SetActive(false);
            southBar1.SetActive(true);
            southBar2.SetActive(false);
            southBar3.SetActive(false);
            southBar4.SetActive(false);
        }
        else if (bar4 < landerSpeedZ)
        {
            Debug.Log("Flying North at 4");
            northBar1.SetActive(true);
            northBar2.SetActive(true);
            northBar3.SetActive(true);
            northBar4.SetActive(true);
            southBar1.SetActive(false);
            southBar2.SetActive(false);
            southBar3.SetActive(false);
            southBar4.SetActive(false);
        }
        else if (bar3 < landerSpeedZ)
        {
            Debug.Log("Flying North at 3");
            northBar1.SetActive(true);
            northBar2.SetActive(true);
            northBar3.SetActive(true);
            northBar4.SetActive(false);
            southBar1.SetActive(false);
            southBar2.SetActive(false);
            southBar3.SetActive(false);
            southBar4.SetActive(false);
        }
        else if (bar2 < landerSpeedZ)
        {
            Debug.Log("Flying North at 2");
            northBar1.SetActive(true);
            northBar2.SetActive(true);
            northBar3.SetActive(false);
            northBar4.SetActive(false);
            southBar1.SetActive(false);
            southBar2.SetActive(false);
            southBar3.SetActive(false);
            southBar4.SetActive(false);
        }
        else if (bar1 < landerSpeedZ)
        {
            Debug.Log("Flying North at 1");
            northBar1.SetActive(true);
            northBar2.SetActive(false);
            northBar3.SetActive(false);
            northBar4.SetActive(false);
            southBar1.SetActive(false);
            southBar2.SetActive(false);
            southBar3.SetActive(false);
            southBar4.SetActive(false);
        }
        else
        {
            Debug.Log("Basically not moving on Z axis");
            northBar1.SetActive(false);
            northBar2.SetActive(false);
            northBar3.SetActive(false);
            northBar4.SetActive(false);
            southBar1.SetActive(false);
            southBar2.SetActive(false);
            southBar3.SetActive(false);
            southBar4.SetActive(false);
        }

        // lander's movement in X axis 
        if (bar4 * -1 > landerSpeedX)
        {
            Debug.Log("Flying West over 4");
            eastBar1.SetActive(false);
            eastBar2.SetActive(false);
            eastBar3.SetActive(false);
            eastBar4.SetActive(false);
            westBar1.SetActive(true);
            westBar2.SetActive(true);
            westBar3.SetActive(true);
            westBar4.SetActive(true);
        }
        else if (bar3 * -1 > landerSpeedX)
        {
            Debug.Log("Flying West over 3");
            eastBar1.SetActive(false);
            eastBar2.SetActive(false);
            eastBar3.SetActive(false);
            eastBar4.SetActive(false);
            westBar1.SetActive(true);
            westBar2.SetActive(true);
            westBar3.SetActive(true);
            westBar4.SetActive(false);
        }
        else if (bar2 * -1 > landerSpeedX)
        {
            Debug.Log("Flying West over 2");
            eastBar1.SetActive(false);
            eastBar2.SetActive(false);
            eastBar3.SetActive(false);
            eastBar4.SetActive(false);
            westBar1.SetActive(true);
            westBar2.SetActive(true);
            westBar3.SetActive(false);
            westBar4.SetActive(false);
        }
        else if (bar1 * -1 > landerSpeedX)
        {
            Debug.Log("Flying West over 1");
            eastBar1.SetActive(false);
            eastBar2.SetActive(false);
            eastBar3.SetActive(false);
            eastBar4.SetActive(false);
            westBar1.SetActive(true);
            westBar2.SetActive(false);
            westBar3.SetActive(false);
            westBar4.SetActive(false);
        }
        else if (bar4 < landerSpeedX)
        {
            Debug.Log("Flying East over 4");
            eastBar1.SetActive(true);
            eastBar2.SetActive(true);
            eastBar3.SetActive(true);
            eastBar4.SetActive(true);
            westBar1.SetActive(false);
            westBar2.SetActive(false);
            westBar3.SetActive(false);
            westBar4.SetActive(false);
        }
        else if (bar3 < landerSpeedX)
        {
            Debug.Log("Flying East over 3");
            eastBar1.SetActive(true);
            eastBar2.SetActive(true);
            eastBar3.SetActive(true);
            eastBar4.SetActive(false);
            westBar1.SetActive(false);
            westBar2.SetActive(false);
            westBar3.SetActive(false);
            westBar4.SetActive(false);
        }
        else if (bar2 < landerSpeedX)
        {
            Debug.Log("Flying East over 2");
            eastBar1.SetActive(true);
            eastBar2.SetActive(true);
            eastBar3.SetActive(false);
            eastBar4.SetActive(false);
            westBar1.SetActive(false);
            westBar2.SetActive(false);
            westBar3.SetActive(false);
            westBar4.SetActive(false);
        }
        else if (bar1 < landerSpeedX)
        {
            Debug.Log("Flying East over 1");
            eastBar1.SetActive(true);
            eastBar2.SetActive(false);
            eastBar3.SetActive(false);
            eastBar4.SetActive(false);
            westBar1.SetActive(false);
            westBar2.SetActive(false);
            westBar3.SetActive(false);
            westBar4.SetActive(false);
        }
        else {
            Debug.Log("Basically not moving on X axis");
            eastBar1.SetActive(false);
            eastBar2.SetActive(false);
            eastBar3.SetActive(false);
            eastBar4.SetActive(false);
            westBar1.SetActive(false);
            westBar2.SetActive(false);
            westBar3.SetActive(false);
            westBar4.SetActive(false);
        }

        // lander's movement in Y axis 
        if (bar4 * -1 > landerSpeedY)
        {
            Debug.Log("Flying West over 4");
            upBar1.SetActive(false);
            upBar2.SetActive(false);
            upBar3.SetActive(false);
            upBar4.SetActive(false);
            downBar1.SetActive(true);
            downBar2.SetActive(true);
            downBar3.SetActive(true);
            downBar4.SetActive(true);
        }
        else if (bar3 * -1 > landerSpeedY)
        {
            Debug.Log("Flying West over 3");
            upBar1.SetActive(false);
            upBar2.SetActive(false);
            upBar3.SetActive(false);
            upBar4.SetActive(false);
            downBar1.SetActive(true);
            downBar2.SetActive(true);
            downBar3.SetActive(true);
            downBar4.SetActive(false);
        }
        else if (bar2 * -1 > landerSpeedY)
        {
            Debug.Log("Flying West over 2");
            upBar1.SetActive(false);
            upBar2.SetActive(false);
            upBar3.SetActive(false);
            upBar4.SetActive(false);
            downBar1.SetActive(true);
            downBar2.SetActive(true);
            downBar3.SetActive(false);
            downBar4.SetActive(false);
        }
        else if (bar1 * -1 > landerSpeedY)
        {
            Debug.Log("Flying West over 1");
            upBar1.SetActive(false);
            upBar2.SetActive(false);
            upBar3.SetActive(false);
            upBar4.SetActive(false);
            downBar1.SetActive(true);
            downBar2.SetActive(false);
            downBar3.SetActive(false);
            downBar4.SetActive(false);
        }
        else if (bar4 < landerSpeedY)
        {
            Debug.Log("Flying East over 4");
            upBar1.SetActive(true);
            upBar2.SetActive(true);
            upBar3.SetActive(true);
            upBar4.SetActive(true);
            downBar1.SetActive(false);
            downBar2.SetActive(false);
            downBar3.SetActive(false);
            downBar4.SetActive(false);
        }
        else if (bar3 < landerSpeedY)
        {
            Debug.Log("Flying East over 3");
            upBar1.SetActive(true);
            upBar2.SetActive(true);
            upBar3.SetActive(true);
            upBar4.SetActive(false);
            downBar1.SetActive(false);
            downBar2.SetActive(false);
            downBar3.SetActive(false);
            downBar4.SetActive(false);
        }
        else if (bar2 < landerSpeedY)
        {
            Debug.Log("Flying East over 2");
            upBar1.SetActive(true);
            upBar2.SetActive(true);
            upBar3.SetActive(false);
            upBar4.SetActive(false);
            downBar1.SetActive(false);
            downBar2.SetActive(false);
            downBar3.SetActive(false);
            downBar4.SetActive(false);
        }
        else if (bar1 < landerSpeedY)
        {
            Debug.Log("Flying East over 1");
            upBar1.SetActive(true);
            upBar2.SetActive(false);
            upBar3.SetActive(false);
            upBar4.SetActive(false);
            downBar1.SetActive(false);
            downBar2.SetActive(false);
            downBar3.SetActive(false);
            downBar4.SetActive(false);
        }
        else
        {
            Debug.Log("Basically not moving on Y axis");
            upBar1.SetActive(false);
            upBar2.SetActive(false);
            upBar3.SetActive(false);
            upBar4.SetActive(false);
            downBar1.SetActive(false);
            downBar2.SetActive(false);
            downBar3.SetActive(false);
            downBar4.SetActive(false);
        }
    }
}
