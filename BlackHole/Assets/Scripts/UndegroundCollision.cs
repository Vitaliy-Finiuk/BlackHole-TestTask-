using UnityEngine;

public class UndegroundCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!Game.isGameOver)
        {
            string tag = other.tag;

            if (tag.Equals("Object"))
            {
                Debug.Log("Object");

                Destroy(other.gameObject);
            }
            if (tag.Equals("Obstacle"))
            {
                Game.isGameOver = true;
            }
        }
    }

}
