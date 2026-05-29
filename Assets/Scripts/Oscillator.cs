using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;

    Vector3 startPosition;
    Vector3 endPositon;
    float movementFactor;

    void Start()
    {
        startPosition = transform.position;
        endPositon = startPosition + movementVector;
    }
    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPositon, movementFactor);
    }
}
