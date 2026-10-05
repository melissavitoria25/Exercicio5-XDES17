//Melissa Vitória dos Santos - 2024000313

using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D playerRb;
    float xDir;
    [SerializeField] float speedX;

    Animator playerSpriteAnimator;

    int qtdPulos;
    [SerializeField] float forcaPulo;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerSpriteAnimator = GetComponentInChildren<Animator>();
    }

    void OnMove(InputValue inputValue)
    {
        xDir = inputValue.Get<Vector2>().x;
    }

    void MovePlayer()
    {
        playerRb.linearVelocityX = xDir * speedX;
        bool isRunning = Mathf.Abs(playerRb.linearVelocityX) > Mathf.Epsilon;
        playerSpriteAnimator.SetBool("IsRunning", isRunning);

        if (isRunning)
        {
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        transform.localScale = new Vector3(Mathf.Sign(playerRb.linearVelocityX), 1, 1);
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void OnJump()
    {
        if(qtdPulos < 2)
        {
            qtdPulos++;
            Pular();
        }
    }

    void Pular()
    {
        if(qtdPulos == 1)
        {
            playerRb.AddForceY(forcaPulo);
        }
        else if(qtdPulos == 2)
        {
            playerRb.AddForceY(1.5f * forcaPulo);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        qtdPulos = 0;
    }
}
