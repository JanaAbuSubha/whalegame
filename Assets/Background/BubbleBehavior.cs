using UnityEngine;

public class BubbleBehavior : MonoBehaviour
{
    public int columns = 3;
    public int rows = 2;
    public int totalFrames = 6;
    public float framesPerSecond = 10f;
    public float changeZ = 10f;
    public float moveDuration = 5f;

    private Renderer rend;
    private Material mat;
    private int currentFrame;
    private float timer;
    private float startX;
    private float startY;
    private float startZ;
    private float movementTimer;

    void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;

        mat.SetTextureScale("_BaseMap", new Vector2(1f / columns, 1f / rows));
        
        startX = transform.position.x;
        startY = transform.position.y;
        startZ = transform.position.z;
    }

    void Update()
    {
        AnimateSpriteSheet();
        MovePlane();
    }

    void AnimateSpriteSheet()
    {
        timer += Time.deltaTime;

        if (timer >= 1f / framesPerSecond)
        {
            timer -= 1f / framesPerSecond;
            currentFrame++;

            if (currentFrame >= totalFrames)
            {
                currentFrame = 0;
            }

            int column = currentFrame % columns;
            int row = currentFrame / columns;

            float xOffset = column / (float)columns;
            float yOffset = 1f - ((row + 1f) / rows);

            mat.SetTextureOffset("_BaseMap", new Vector2(xOffset, yOffset));
        }
    }

    void MovePlane()
    {
        movementTimer += Time.deltaTime;

        float progress = movementTimer / moveDuration;
        
        float newZ = Mathf.Lerp(startZ, startZ + changeZ, progress);

        transform.position = new Vector3(startX, startY, newZ);

        if (progress >= 1f)
        {
            movementTimer = 0f;
            transform.position = new Vector3(startX, startY, startZ);
        }
    }
}