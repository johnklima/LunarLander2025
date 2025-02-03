using UnityEngine;

    public class Planet : MonoBehaviour
    {
        public float gravity;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                other.GetComponent<Gravity>().GRAVITY_CONSTANT = gravity;
        }
    }