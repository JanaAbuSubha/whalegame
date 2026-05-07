using UnityEngine;
///
/// Creates movement and animation behaviour for a Bubble plane  
/// By Jana Abu Subha 
/// 5/1/2026
/// 
public class BubbleBehavior : MonoBehaviour
{
    [Header("Animation Settings")]
    public int columns = 3;
    public int rows = 2;
    public int totalFrames = 6;
    public float framesPerSecond = 10f;

    [Header("Move Settings")]
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

    /// 
    /// Gets the renderer and material from the bubble plane, sets the sprite sheet scale, 
    /// and saves the bubble's starting position.
    /// 
    void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;

        mat.SetTextureScale("_BaseMap", new Vector2(1f / columns, 1f / rows));
        
        startX = transform.position.x;
        startY = transform.position.y;
        startZ = transform.position.z;
    }

    ///
    /// Runs every frame, updates the bubble sprite sheet animation, and 
    /// updates the bubble movement.
    /// 
    void Update()
    {
        AnimateSpriteSheet();
        MovePlane();
    }

    ///
    /// Controls the bubble sprite sheet animation.
    /// Moves through each frame based on framesPerSecond and resets back to the first 
    /// frame after the last frame.
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
    /// Moves the bubble upward along the z-axis, and resets the bubble back to its starting position 
    /// when the movement is complete. Keeps the x and y positions the same.
    /// 
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