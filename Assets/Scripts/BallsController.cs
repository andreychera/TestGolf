using UnityEngine;

public class BallsController : MonoBehaviour
{
    private Vector3 startMousePosition;
    private Vector3 endMousePosition;
    private bool isDragging = false;
    private float Stopped = 0f;
    private int AttemptsAmount = 3;
    public static bool isStopped = false; 
    private bool Lose = false; 
    public static bool Win = false; 
    public float forceMultiplier;
    private Rigidbody BallRigidbody;

    public static Vector3 currentPosition;

    void Start()
    {
        BallRigidbody = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        if(isStopped == true && AttemptsAmount > 0)
        {
            startMousePosition = GetMouseWorldPosition();
            isDragging = true;
        }
    }

    void OnMouseDrag()
    {
        if(isStopped == true && AttemptsAmount > 0)
        {
            Vector3 currentMousePosition = GetMouseWorldPosition();
            Debug.DrawLine(startMousePosition, currentMousePosition, Color.red);
        }
    }

    void OnMouseUp()
    {
        if(isStopped == true && AttemptsAmount > 0)
        {
            if (!isDragging) return;

            endMousePosition = GetMouseWorldPosition();
            Vector3 direction = (endMousePosition - startMousePosition).normalized;
            float distance = Vector3.Distance(startMousePosition, endMousePosition);

            Vector3 force = -1 * direction * distance * forceMultiplier;

            float maxForce = 7f;
            force = Vector3.ClampMagnitude(force, maxForce);
            BallRigidbody.AddForce(force, ForceMode.Impulse);

            isDragging = false;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    void AttemptsSystem()
    {
        // Заменяем linearVelocity на velocity
        if(!isStopped && BallRigidbody.velocity.magnitude == Stopped && BallRigidbody.angularVelocity.magnitude == Stopped)
        {
            AttemptsAmount -= 1;
            isStopped = true;
        }

        if (BallRigidbody.velocity.magnitude > Stopped || BallRigidbody.angularVelocity.magnitude > Stopped)
        {
            isStopped = false;
        }

        if(AttemptsAmount == 0 && Lose == false && Win == false) 
        {
            Debug.Log("You Lose!");
            Lose = true;
        }
    }

    void Update() 
    {
        if(isStopped == true)
        {
            // Обновление текущей позиции
            currentPosition = transform.position;
        }

        AttemptsSystem();
    }
}