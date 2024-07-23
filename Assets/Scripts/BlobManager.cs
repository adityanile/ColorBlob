using UnityEngine;
using XDPaint;

public class BlobManager : MonoBehaviour
{
    public Color color;

    private Vector3 screenPoint;
    private Vector3 offset;

    private GameObject pivot;
    public bool painting = false;

    private Rigidbody rb;

    private float radius = 0;
    public PaintManager pm;

    private Renderer mioRenderer;
    Texture2D texture;

    RaycastHit hit;
    Vector2 pixelUV;
    Vector2 pixelPoint;

    private void Start()
    {
        pivot = GameObject.Find("Pivot");
        rb = GetComponent<Rigidbody>();

        mioRenderer = pivot.GetComponent<Renderer>();
        texture = mioRenderer.material.mainTexture as Texture2D;
    }

    private void Update()
    {
        if (painting)
        {
            float m1 = 100;
            float m2 = rb.mass;

            float force = (6.67f * m1 * m2) / Mathf.Pow(radius, 2);
            transform.RotateAround(pivot.transform.position, Vector3.forward, force * Time.deltaTime);

            Paint();
        }
    }

    void OnMouseDown()
    {
        StopPainting();

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        CalculateRadius();
        StartPainting();
    }

    void StopPainting()
    {
        if (painting)
        {
            painting = false;

            if(pm)
               pm.PaintObject.FinishPainting();

        }
    }
    void StartPainting()
    {
        if (!painting)
        {
            painting = true;

            if (!pm)
                pm = pivot.GetComponent<PaintManager>();
        }
    }

    void Paint()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, out hit))
        {
            pixelUV = hit.textureCoord;
            pixelPoint = new Vector2(pixelUV.x * texture.width, pixelUV.y * texture.height);

            var clr = pm.Brush.Color;
            clr = new Color(color.r, color.g, color.b, clr.a);
            pm.Brush.SetColor(clr);

            pm.PaintObject.DrawPoint(pixelPoint);
        }
    }

    void CalculateRadius()
    {
        Vector3 dir = (pivot.transform.position - transform.position);
        radius = dir.magnitude;
    }

}
