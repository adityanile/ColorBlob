using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    float radius = 0;

    private GameObject pivot;

    public float speed = 1f;
    public float offset = 0.2f;

    public List<Vector3> positions = new();

    private int step = 1;
    public bool move = false;
    private bool circularMotion = false;

    // Control which Motion to init according to the selected shape
    public void InitMotion()
    {
        pivot = GameObject.Find("Pivot");

        InitTriangle();
        //InitSquare();
        //InitRectangle();

        //InitCircle();
    }

    public void StopMotion()
    {
        if (move)
        {
            positions.Clear();
            move = false;
        }
    }

    void InitCircle()
    {
        positions.Clear();

        Vector3 dir = (pivot.transform.position - transform.position);
        radius = dir.magnitude;

        circularMotion = true;
        move = true;
    }

    void InitRectangle()
    {
        positions.Clear();

        Vector3 dir = (pivot.transform.position - transform.position);
        radius = dir.magnitude;

        float b = 2 * radius * Mathf.Cos(45 * Mathf.Deg2Rad);
        float l = b * 0.5f;

        Vector3 startPos = transform.position;
        positions.Add(startPos);

        float angle = Vector2.Angle(transform.right, dir);
        float upAngle = Vector2.Angle(transform.up, dir);

        float x1 = 0, y1 = 0, x2 = 0, y2 = 0, x3 = 0, y3 = 0;

        if (upAngle < 90)
        {
            if (angle < 90)
            {
                x1 = startPos.x;
                y1 = startPos.y + l;

                x2 = startPos.x + b;
                y2 = startPos.y;

                x3 = x2;
                y3 = y2 + l;
            }
            else
            {
                x1 = startPos.x;
                y1 = startPos.y + l;

                x2 = startPos.x - b;
                y2 = startPos.y;

                x3 = x2;
                y3 = y2 + l;
            }
        }
        else
        {
            if (angle < 90)
            {
                x1 = startPos.x;
                y1 = startPos.y - l;

                x2 = startPos.x + b;
                y2 = startPos.y;

                x3 = x2;
                y3 = y2 - l;
            }
            else
            {
                x1 = startPos.x;
                y1 = startPos.y - l;

                x2 = startPos.x - b;
                y2 = startPos.y;

                x3 = x2;
                y3 = y2 - l;
            }
        }


        Vector3 end1 = new Vector3(x1, y1, 0);
        Vector3 end2 = new Vector3(x2, y2, 0);
        Vector2 end3 = new Vector3(x3, y3, 0);

        positions.Add(end2);
        positions.Add(end3);
        positions.Add(end1);

        move = true;
    }
    void InitSquare()
    {
        positions.Clear();

        Vector3 dir = (pivot.transform.position - transform.position);
        radius = dir.magnitude;

        float b = 2 * radius * Mathf.Cos(45 * Mathf.Deg2Rad);
        float l = b;

        float diag = Mathf.Sqrt(2) * b;

        Vector3 startPos = transform.position;
        positions.Add(startPos);

        float angle = Vector2.Angle(transform.right, dir);
        float upAngle = Vector2.Angle(transform.up, dir);

        float x1 = 0, y1 = 0, x2 = 0, y2 = 0, x3 = 0, y3 = 0;

        if (upAngle < 90)
        {
            if (angle < 90)
            {
                x1 = startPos.x;
                y1 = startPos.y + l;

                x2 = startPos.x + b;
                y2 = startPos.y;

                x3 = startPos.x + diag * Mathf.Cos(45 * Mathf.Deg2Rad);
                y3 = startPos.y + diag * Mathf.Sin(45 * Mathf.Deg2Rad);
            }
            else
            {
                x1 = startPos.x;
                y1 = startPos.y + l;

                x2 = startPos.x - b;
                y2 = startPos.y;

                x3 = startPos.x - diag * Mathf.Cos(45 * Mathf.Deg2Rad);
                y3 = startPos.y + diag * Mathf.Sin(45 * Mathf.Deg2Rad);
            }
        }
        else
        {
            if (angle < 90)
            {
                x1 = startPos.x;
                y1 = startPos.y - l;

                x2 = startPos.x + b;
                y2 = startPos.y;

                x3 = startPos.x + diag * Mathf.Cos(45 * Mathf.Deg2Rad);
                y3 = startPos.y - diag * Mathf.Sin(45 * Mathf.Deg2Rad);
            }
            else
            {
                x1 = startPos.x;
                y1 = startPos.y - l;

                x2 = startPos.x - b;
                y2 = startPos.y;

                x3 = startPos.x - diag * Mathf.Cos(45 * Mathf.Deg2Rad);
                y3 = startPos.y - diag * Mathf.Sin(45 * Mathf.Deg2Rad);
            }
        }


        Vector3 end1 = new Vector3(x1, y1, 0);
        Vector3 end2 = new Vector3(x2, y2, 0);
        Vector2 end3 = new Vector3(x3, y3, 0);

        positions.Add(end2);
        positions.Add(end3);
        positions.Add(end1);

        move = true;
    }

    void InitTriangle()
    {
        positions.Clear();

        Vector3 dir = (pivot.transform.position - transform.position);
        radius = dir.magnitude;
        float side = Mathf.Sqrt(3) * radius;

        Vector3 startPos = transform.position;
        positions.Add(startPos);

        float angle = Vector2.Angle(transform.right, dir);
        float upAngle = Vector2.Angle(transform.up, dir);

        float x1 = 0, y1 = 0, x2 = 0, y2 = 0;

        if (upAngle < 90)
        {
            if (angle < 90)
            {
                x1 = startPos.x + side * Mathf.Cos(60 * Mathf.Deg2Rad);
                y1 = startPos.y + side * Mathf.Sin(60 * Mathf.Deg2Rad);

                x2 = startPos.x + side;
                y2 = startPos.y;
            }
            else
            {
                x1 = startPos.x + side * Mathf.Cos((90 + 30) * Mathf.Deg2Rad);
                y1 = startPos.y + side * Mathf.Sin((90 + 30) * Mathf.Deg2Rad);

                x2 = startPos.x - side;
                y2 = startPos.y;
            }
        }
        else
        {
            if (angle < 90)
            {
                x1 = startPos.x + side * Mathf.Cos(60 * Mathf.Deg2Rad);
                y1 = startPos.y - side * Mathf.Sin(60 * Mathf.Deg2Rad);

                x2 = startPos.x + side;
                y2 = startPos.y;
            }
            else
            {
                x1 = startPos.x + side * Mathf.Cos((90 + 30) * Mathf.Deg2Rad);
                y1 = startPos.y - side * Mathf.Sin((90 + 30) * Mathf.Deg2Rad);

                x2 = startPos.x - side;
                y2 = startPos.y;
            }
        }

        Vector3 end1 = new Vector3(x1, y1, 0);
        Vector3 end2 = new Vector3(x2, y2, 0);

        positions.Add(end2);
        positions.Add(end1);

        move = true;
    }

    private void Update()
    {
        if (move)
        {
            if (!circularMotion)
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
            else
            {
                float m1 = 100;
                float m2 = 1;

                float force = (6.67f * m1 * m2) / Mathf.Pow(radius, 2);
                transform.RotateAround(pivot.transform.position, Vector3.forward, force * Time.deltaTime);
            }
        }
    }

}
