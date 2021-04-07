using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour
{
    public float moveSpeed = 10, jumpSpeed = 5;
    private Rigidbody rb;
    private bool isGrounded = true;
    int totalPotions = 0;
    int totalWPotion = 0;
    int totalSPotion = 0;
    int totalRPotion = 0;

    [SerializeField]
    AudioClip doorOpen;
    
    [SerializeField]
    AudioClip jump;
    
    [SerializeField]
    AudioClip powerUp;

    AudioSource aud;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        rb = this.GetComponent<Rigidbody>();
        aud = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }

    void FixedUpdate() {
        rb.AddForce(Vector3.right * moveSpeed);

        if(!isGrounded && rb.velocity.y > 0) {
            rb.AddForce(Vector3.down * jumpSpeed);
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Floor")) {
            isGrounded = true;
        }

        if(other.gameObject.CompareTag("wayfinding-potion")) {
            totalPotions += 1;
            totalWPotion += 1;
            Destroy(other.gameObject);
            Debug.Log("You have " + totalPotions + " potions.");
            Debug.Log("You have " + totalWPotion + " wayfinding potions.");
            aud.PlayOneShot(powerUp);
            if(totalPotions > 3) {
                moveSpeed = 10;
            }
            if(totalPotions > 5) {
                moveSpeed = 13;
            }
            if(totalPotions > 7) {
                moveSpeed = 16;
            }
        }

        if(other.gameObject.CompareTag("strength-potion")) {
            totalPotions += 1;
            totalSPotion += 1;
            Destroy(other.gameObject);
            Debug.Log("You have " + totalPotions + " potions.");
            Debug.Log("You have " + totalSPotion + " strength potions.");
            aud.PlayOneShot(powerUp);
            if(totalPotions > 3) {
                moveSpeed = 10;
            }
            if(totalPotions > 5) {
                moveSpeed = 13;
            }
            if(totalPotions > 7) {
                moveSpeed = 16;
            }
        }

        if(other.gameObject.CompareTag("reveal-potion")) {
            totalPotions += 1;
            totalRPotion += 1;
            Destroy(other.gameObject);
            Debug.Log("You have " + totalPotions + " potions.");
            Debug.Log("You have " + totalRPotion + " reveal potions.");
            aud.PlayOneShot(powerUp);
            if(totalPotions > 3) {
                moveSpeed = 10;
            }
            if(totalPotions > 5) {
                moveSpeed = 13;
            }
            if(totalPotions > 7) {
                moveSpeed = 16;
            }
        }

        if(other.gameObject.CompareTag("Door")) {
            moveSpeed = 0;
            if(totalPotions == 30) {
                totalWPotion -= 10;
                totalRPotion -= 10;
                totalSPotion -= 10;
                totalPotions -= 30;
                Destroy(other.gameObject);
                aud.PlayOneShot(doorOpen);
            }
            else {
                Debug.Log("You need to collect all potions to open this door. Try again.");
                this.transform.position = startPosition;
            }
        }
    }

    void Jump() {
        if(isGrounded) {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            aud.PlayOneShot(jump);
        }
    }
}
