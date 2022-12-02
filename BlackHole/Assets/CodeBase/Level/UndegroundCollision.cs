using UnityEngine;

namespace CodeBase.Level
{
    public class UndegroundCollision : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            string otherTag = other.tag;

            if (otherTag.Equals("Object"))
            {
                Debug.Log("Object");

                Destroy(other.gameObject);
            }
        }
    }
}