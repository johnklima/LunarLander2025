using UnityEngine;

public class PadTouchdown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player on pad");

            BallGravitySphere bgs = other.transform.GetComponent<BallGravitySphere>();

            bgs.onGround = true;

            Debug.Log("velo Z = " + bgs.velocity.y);

            if ( bgs.velocity.z > -2.5f)  
            {
                bgs.velocity = Vector3.zero;
                Debug.Log("SUCCESS!!");
            }
            else 
            {
                bgs.velocity = Vector3.zero;
                Debug.Log("FAILURE!!");
            }

        }
    }
}
