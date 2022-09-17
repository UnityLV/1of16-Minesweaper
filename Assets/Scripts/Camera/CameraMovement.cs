using UnityEngine;

public sealed class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlatesGrid _platesGrid;
    [SerializeField] private float _speed = 30;
    

    private void OnEnable()
    {
        _platesGrid.FindetStartPosition += OnFindetStartPosition;
    }

    private void Update()
    {
        TryMove();
    }    

    private void OnDisable()
    {
        _platesGrid.FindetStartPosition -= OnFindetStartPosition;
    }

    private void TryMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * _speed);
        }
    }

    private void OnFindetStartPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x , position.y , transform.position.z);
    }

    public void SetDefaultPosition()
    {
        transform.position = new Vector3(0, 0, -10);
    }
}
