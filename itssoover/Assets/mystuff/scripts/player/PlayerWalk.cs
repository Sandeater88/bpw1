using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the player's movement speed as needed
    public Sprite defaultSprite; // The default sprite of the player
    public Sprite runSprite; // The run sprite of the player

    private SpriteRenderer spriteRenderer;
    private Coroutine swapCoroutine;
    private bool isRunning = false;

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the player's keyboard
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate the player's movement direction based on input
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        // Move the player's Rigidbody2D component
        GetComponent<Rigidbody2D>().velocity = movement * moveSpeed;

        // Handle sprite flipping and swapping
        HandleSprite(horizontalInput);
    }

    private void HandleSprite(float horizontalInput)
    {
        if (horizontalInput < 0)
        {
            // Player is moving left, flip the sprite and start run sprite loop
            spriteRenderer.flipX = true;
            if (!isRunning)
            {
                if (swapCoroutine != null)
                {
                    StopCoroutine(swapCoroutine);
                }
                swapCoroutine = StartCoroutine(SwapSpriteCoroutine(true));
            }
        }
        else if (horizontalInput > 0)
        {
            // Player is moving right, unflip the sprite and start run sprite loop
            spriteRenderer.flipX = false;
            if (!isRunning)
            {
                if (swapCoroutine != null)
                {
                    StopCoroutine(swapCoroutine);
                }
                swapCoroutine = StartCoroutine(SwapSpriteCoroutine(false));
            }
        }
        else
        {
            // Player is not moving horizontally, stop the run sprite loop
            if (swapCoroutine != null)
            {
                StopCoroutine(swapCoroutine);
                swapCoroutine = null;
            }
            spriteRenderer.sprite = defaultSprite;
            isRunning = false;
        }
    }

    private IEnumerator SwapSpriteCoroutine(bool flip)
    {
        isRunning = true;
        while (true)
        {
            spriteRenderer.sprite = runSprite;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.sprite = defaultSprite;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
