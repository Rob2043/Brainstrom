using UnityEngine;
using UnityEngine.SceneManagement;

public class Cup : MonoBehaviour
{
    private Vector3 touchStartPos;
    private Vector3 objectStartPos;
    private SelectedObject selectedObject;
    [SerializeField] private float speed;

    private void Start()
    {
        selectedObject = FindObjectOfType<SelectedObject>();
    }

    private void Update()
    {
        if (selectedObject.FoundObject != null && selectedObject.FoundObject.CompareTag("Player"))
        {
            HandleTouchInput();
        }
        if (selectedObject.FoundObject != null && selectedObject.FoundObject.CompareTag("GreenPlayer"))
        {
            HandeleTouchInputGreen();
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    objectStartPos = selectedObject.FoundObject.transform.position;
                    break;

                case TouchPhase.Moved:
                    Vector3 deltaPosition = new Vector3(touch.position.x, 0, 0) - new Vector3(touchStartPos.x, 0,0);

                    selectedObject.FoundObject.transform.position = objectStartPos + new Vector3(deltaPosition.x, 0,0) * Time.deltaTime * speed;
                    break;

                case TouchPhase.Ended:
                    break;
            }
        }
    }
    private void HandeleTouchInputGreen()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    objectStartPos = selectedObject.FoundObject.transform.position;
                    break;

                case TouchPhase.Moved:
                    Vector3 deltaPosition = new Vector3(0, 0, touch.position.y) - new Vector3(0, 0, touchStartPos.y);

                    selectedObject.FoundObject.transform.position = objectStartPos + new Vector3(0, 0, deltaPosition.z) * Time.deltaTime * speed;
                    break;

                case TouchPhase.Ended:
                    break;
            }
        }
    }
}

//if (Input.GetKey(keycode1)) // вперед
//{
//    GetComponent<Rigidbody>().velocity += moveDirection;
//}
//if (Input.GetKey(keycode2)) // назад
//{
//    GetComponent<Rigidbody>().velocity -= moveDirection;
//}
//if (Input.GetKey(KeyCode.R))
//{
//    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//}
//if (Input.GetKey(KeyCode.Q))
//{
//    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//}