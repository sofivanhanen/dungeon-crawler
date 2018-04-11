using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float Interpolation = 5.0f;
    private Vector3 _offset;

    public GameObject Player;

    private void Start()
    {
        _offset = transform.position - Player.transform.position;
    }

    private void LateUpdate()
    {
        // Follow player smoothly
        var position = transform.position;
        position.z = Mathf.Lerp(transform.position.z, Player.transform.position.z + _offset.z,
            Interpolation * Time.deltaTime);
        position.x = Mathf.Lerp(transform.position.x, Player.transform.position.x + _offset.x,
            Interpolation * Time.deltaTime);
        transform.position = position;
    }
}