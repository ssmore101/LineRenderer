using UnityEngine;
 
public class LineDraw : MonoBehaviour
{
    //To add Line Renderer object 
    public LineRenderer Line;
    public float lineWidth = 0.001f;

    //vertex value
    public float minimumVertexDistance = 0.1f;

    int currlines = 0;

    private bool isLineStarted;

    void Start()
    {
        // set the color of the line
        Line.startColor = Color.red;
        Line.endColor = Color.red;

        // set width of the renderer
        Line.startWidth = lineWidth;
        Line.endWidth = lineWidth;

        isLineStarted = false;
        Line.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // To get current mouse/curser position
            Line.positionCount = 0;
            Vector3 mousePos = GetWorldCoordinate(Input.mousePosition);
            // to count current position
            Line.positionCount = 2;
            Line.SetPosition(0, mousePos);
            Line.SetPosition(1, mousePos);
            isLineStarted = true;
        }

        if (Input.GetMouseButton(0) && isLineStarted)
        {
            
            Vector3 currentPos = GetWorldCoordinate(Input.mousePosition);
            float distance = Vector3.Distance(currentPos, Line.GetPosition(Line.positionCount - 1));
            if (distance > minimumVertexDistance)
            {
                Debug.Log(distance);
                UpdateLine();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isLineStarted = false;
        }
    }

    private void UpdateLine()
    {
        Line.positionCount++;
        Line.SetPosition(Line.positionCount - 1, GetWorldCoordinate(Input.mousePosition));
    }

    private Vector3 GetWorldCoordinate(Vector3 mousePosition)
    {
        Vector3 mousePos = new Vector3(mousePosition.x, mousePosition.y, 1);
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}