using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public float gravity = 9.81f * 2;
    public float jumpForce = 8.5f;

    private AudioSource playerAudio;
    public AudioClip jumpSound;
    private void Awake()
    {
        character = GetComponent<CharacterController>();
        playerAudio = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;
        if (character.isGrounded)
        {
            direction = Vector3.down;
            if (Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpForce;
                playerAudio.PlayOneShot(jumpSound, 1f);

            }
        }
        character.Move(direction * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
