using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    private void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        Vector3 movement = speed * Time.deltaTime * new Vector3(horizontalMovement, 0.0f, 0.0f);
        transform.position += movement;
    }
}