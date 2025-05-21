using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FruitDropGuide : MonoBehaviour
{
    public float maxRayDistance = 20f;
    public LayerMask collisionMask;

    private LineRenderer lineRenderer;
    private Fruit fruit;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        fruit = GetComponent<Fruit>();

        lineRenderer.positionCount = 2;
        lineRenderer.enabled = true;

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.textureMode = LineTextureMode.Tile;

        if (GameManager.Instance.dottedLineTexture != null)
        {
            lineRenderer.material.mainTexture = GameManager.Instance.dottedLineTexture;
            lineRenderer.material.mainTextureScale = new Vector2(1f, 1f);
        }
        else
        {
            Debug.LogWarning("Dotted line texture is not assigned in GameManager!!!");
        }
    }

    private void Update()
    {
        if (!fruit.hasDropped)
        {
            Vector2 origin = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, maxRayDistance, collisionMask);

            Vector3 origin3D = new Vector3(origin.x, origin.y, 0f);
            Vector3 endPos = hit.collider ? (Vector3)hit.point : origin3D + Vector3.down * maxRayDistance;

            lineRenderer.SetPosition(0, origin3D);
            lineRenderer.SetPosition(1, endPos);
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

}
