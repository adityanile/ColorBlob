using System.Collections.Generic;
using UnityEngine;

public class TriagleMotion : MonoBehaviour
{
    public float side = 0;
    float radius = 0;

    private GameObject pivot;

    public float speed = 1f;
    public float offset = 0.2f;

    public List<Vector3> positions = new();

    private int step = 1;
    public bool move = false;

    public void Init()
    {
        positions.Clear();

        pivot = GameObject.Find("Pivot");

        Vector3 dir = (pivot.transform.position - transform.position);
        radius = dir.magnitude;
        side = Mathf.Sqrt(3) * radius;

        Vector3 startPos = transform.position;
        positions.Add(startPos);

        float angle = Vector2.Angle(transform.right, dir);

        if (angle < 90)
        {
            float x1 = startPos.x + side * Mathf.Cos(60 * Mathf.Deg2Rad);
            float y1 = startPos.y + side * Mathf.Sin(60 * Mathf.Deg2Rad);

            Vector3 end1 = new Vector3(x1, y1, 0);

            float x2 = startPos.x + side;
            float y2 = startPos.y;

            Vector3 end2 = new Vector3(x2, y2, 0);
            
            positions.Add(end2);
            positions.Add(end1);

            Debug.Log(Vector3.Distance(startPos, end1));

            Debug.DrawLine(startPos, end1, Color.gray, 20);
            Debug.DrawLine(startPos, end2, Color.gray, 20);

            move = true;
        }
        else
        {

        }
    }

    private void Update()
    {
        if (move)
        {
            int index = step % positions.Count;

            Vector3 dir = (positions[index] - transform.position).normalized;
            float dist = Vector3.Distance(positions[index], transform.position);

            if (dist > offset)
            {
                transform.Translate(dir * speed * Time.deltaTime);
            }
            else
            {
                step++;
            }
        }
    }

}
