using UnityEngine;
///
/// Creates movement, fading, and animation behaviour for a Fish plane  
/// By Jana Abu Subha 
/// 5/1/2026
/// 
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

    ///
    /// Runs once at runtime, gets the renderer and material from the fish plane.
    /// Sets up the sprite sheet scale, saves the starting position,
    /// and makes the fish invisible at the beginning.
    /// 
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
    /// 
    /// Runs every frame. Updates the fish animation, movement, and opacity.
    /// 
    void Update()
    {
        AnimateSpriteSheet();
        MovePlane();
        UpdateOpacity();
    }

    ///
    /// Animates the fish by switching between frames in the sprite sheet
    /// based on the frames per second value.
    /// 
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

    ///
    /// Moves the fish from its starting x-position to the target x-position.
    /// When the movement is finished, it resets the fish back to the start
    /// and makes it invisible again.
    /// 
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

    ///
    /// Controls the fade in and fade out effect.
    /// The fish fades in at the start, stays visible in the middle,
    /// and fades out near the end.
    /// 
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

    /// 
    /// Changes the transparency of the fish material.
    /// 0 means fully invisible, and 1 means fully visible.
    /// 
    void SetOpacity(float opacity)
    {
        Color color = mat.color;
        color.a = opacity;
        mat.color = color;
    }
}