using UnityEngine;

public class Linear : MonoBehaviour
{
    [SerializeField] private LineRenderer Line;
    [SerializeField] private Collider objectCollider;

    private void LineDrawer(Vector3 worldPoint)
    {
        Vector3 colliderCenter = objectCollider.transform.position;
        Vector3[] positions = {
            colliderCenter, 
            worldPoint          
        };
        Debug.Log(worldPoint + " " + colliderCenter);
        Line.SetPositions(positions);
        Line.enabled = true;
    }

    private Vector3 GetMouseWorldPosition() {
        
        Vector3 screenMousePos = new Vector3(Input.mousePosition.x, -70, Camera.main.nearClipPlane);
        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }


    private void Start() 
    {
       // Camera.main.nearClipPlane = 1.5f;
    }
    void Update()
    {
        Vector3 worldPoint = GetMouseWorldPosition();
        LineDrawer(worldPoint);
    }
}
