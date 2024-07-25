using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    private Renderer mioRenderer;
    Texture2D texture;

    Vector3 startPos;

    private Motion motion;

    private void Start()
    {
        startPos = transform.position;

        pivot = GameObject.Find("Pivot");
        rb = GetComponent<Rigidbody>();

        mioRenderer = pivot.GetComponent<Renderer>();
        texture = mioRenderer.material.mainTexture as Texture2D;
    }

    private void Update()
    {
        if (painting)
        {
            Paint();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OnMouseDown()
    {
        if (!motion)
            motion = GetComponent<Motion>();

        Spawner.current = motion;

        motion.StopMotion();
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

    public void OnMouseUp()
    {
        if (ShouldSpawn(transform.position))
        {
            StartPainting();
            motion.InitMotion();
        }
        else
        {
            transform.position = startPos;
        }
    }

    public bool ShouldSpawn(Vector3 pos)
    {
        RaycastHit hit;

        if (Physics.Raycast(pos, Vector3.forward, out hit))
        {
            if (hit.collider.CompareTag("Pivot"))
            {
                return true;
            }
        }
        return false;
    }

    void StopPainting()
    {
        if (painting)
        {
            painting = false;

            if (pm)
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
        RaycastHit hit;
        Vector2 pixelUV = new();
        Vector2 pixelPoint = new();

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
}
