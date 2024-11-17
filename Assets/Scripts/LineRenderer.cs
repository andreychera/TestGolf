using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    bool Hitted = false;
   [SerializeField] private LineRenderer Line;
   [SerializeField] private SphereCollider sphere;

    private void DrawLine(Vector3 worldPoint)
    {
        Vector3 localStartPoint = sphere.transform.InverseTransformPoint(sphere.transform.position); 
        Vector3 localEndPoint = sphere.transform.InverseTransformPoint(worldPoint); 

        Line.SetPositions(new Vector3[] { localStartPoint, localEndPoint });
        Line.enabled = true;

        Debug.DrawLine(localStartPoint, localEndPoint, Color.green);
    }
    private void Start() {
        Camera.main.nearClipPlane = 1.5f;
    }
   public void Update()
   {
      if (Input.GetMouseButtonDown(0) && !Hitted && CheckIfLookingAtSphere())
        {
            Hitted = true;
        }

        if (Input.GetMouseButton(0) && Hitted)
        {
            Vector3? worldPoint = CastMouseClickRay();

            if (worldPoint.HasValue)
            {
                DrawLine(worldPoint.Value);
            }
        }
        else
        {
            Line.enabled = false;
            Hitted = false;
        }
   }

   private Vector3? CastMouseClickRay()
   {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane
        );
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane
        );
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
   }


    private Vector3 GetMouseWorldPosition() {
        
        Vector3 screenMousePos = new Vector3(Input.mousePosition.x, -70, Camera.main.nearClipPlane);
        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }

    public bool CheckIfLookingAtSphere(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider == sphere){
            Debug.Log("Hit");
            return true;
        }else{
            Debug.Log("Doesnt hit");
            return false;
        }
    }

}