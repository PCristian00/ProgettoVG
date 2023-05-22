using UnityEngine;

//Da tutorial Space Invaders. Modificare

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyScript : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Sprite[] animationSprites = new Sprite[0];
    public float animationTime = 1f;
    public int AnimationFrame { get; private set; }
    public int score = 10;
    public System.Action<EnemyScript> killed;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = animationSprites[0];
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
    }

    private void AnimateSprite()
    {
        AnimationFrame++;

        // Loop back to the start if the animation frame exceeds the length
        if (AnimationFrame >= animationSprites.Length)
        {
            AnimationFrame = 0;
        }

        SpriteRenderer.sprite = animationSprites[AnimationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            killed?.Invoke(this);
        }
    }

}
