using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    [Header("Animation Settings")]
    public int columns = 3;
    public int rows = 2;
    public int totalFrames = 6;
    public float framesPerSecond = 10f;
    
    [Header("Move Settings")]
    public float xDistance = 10f;
    public float moveDuration = 5f;

    [Header("Fade Settings")]
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;

    private Renderer rend;
    private Material mat;
    private int currentFrame;
    private float timer;

    private float movementTimer;

    private float startX;
    private float startY;
    private float startZ;

    void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;

        mat.SetTextureScale("_BaseMap", new Vector2(1f / columns, 1f / rows));

        startX = transform.position.x;
        startY = transform.position.y;
        startZ = transform.position.z;

        SetOpacity(0f);
    }

    void Update()
    {
        AnimateSpriteSheet();
        MovePlane();
        UpdateOpacity();
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

        float newX = Mathf.Lerp(startX, startX + xDistance, progress);

        transform.position = new Vector3(newX, startY, startZ);

        if (progress >= 1f)
        {
            movementTimer = 0f;
            transform.position = new Vector3(startX, startY, startZ);
            SetOpacity(0f);
        }
    }

    void UpdateOpacity()
    {
        float opacity = 1f;

        if (movementTimer < fadeInDuration)
        {
            opacity = movementTimer / fadeInDuration;
        }
        else if (movementTimer > moveDuration - fadeOutDuration)
        {
            float fadeOutTimer = moveDuration - movementTimer;
            opacity = fadeOutTimer / fadeOutDuration;
        }
        else
        {
            opacity = 1f;
        }

        opacity = Mathf.Clamp01(opacity);
        SetOpacity(opacity);
    }

    void SetOpacity(float opacity)
    {
        Color color = mat.color;
        color.a = opacity;
        mat.color = color;
    }
}