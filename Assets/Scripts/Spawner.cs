using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Motion current;

    private void OnMouseDown()
    {
        current.StopMotion();

        if (CompareTag("Circle"))
            current.gameObject.tag = "Circle";

        if (CompareTag("Square"))
            current.gameObject.tag = "Square";

        if (CompareTag("Rectangle"))
            current.gameObject.tag = "Rectangle";

        if (CompareTag("Triangle"))
            current.gameObject.tag = "Triangle";

        if (CompareTag("Wave"))
            current.gameObject.tag = "Wave";

        current.InitMotion();
    }
}
