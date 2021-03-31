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

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
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

        //Debug.Log("I have collected a " + other.gameObject.name);


        if(other.gameObject.CompareTag("wayfinding-potion")) {
            totalPotions += 1;
            totalWPotion += 1;
            Destroy(other.gameObject);
            Debug.Log("You have " + totalPotions + " potions.");
            Debug.Log("You have " + totalWPotion + " wayfinding potions.");
        }

        if(other.gameObject.CompareTag("strength-potion")) {
            totalPotions += 1;
            totalSPotion += 1;
            Destroy(other.gameObject);
            Debug.Log("You have " + totalPotions + " potions.");
            Debug.Log("You have " + totalSPotion + " strength potions.");
        }

        if(other.gameObject.CompareTag("reveal-potion")) {
            totalPotions += 1;
            totalRPotion += 1;
            Destroy(other.gameObject);
            Debug.Log("You have " + totalPotions + " potions.");
            Debug.Log("You have " + totalRPotion + " reveal potions.");
        }

        if(other.gameObject.CompareTag("Purple Door")) {
            if(totalWPotion > 10) {
                totalWPotion -= 10;
                totalPotions -= 10;
                Destroy(other.gameObject);
                //aud.PlayOneShot(doorOpen);
            }
            else {
                Debug.Log("You need to collect 10 wayfinding potions to open this door.");
            }
        }

        if(other.gameObject.CompareTag("Red Door")) {
            if(totalSPotion > 10) {
                totalSPotion -= 10;
                totalPotions -= 10;
                Destroy(other.gameObject);
                //aud.PlayOneShot(doorOpen);
            }
            else {
                Debug.Log("You need to collect 10 strength potions to open this door.");
            }
        }

        if(other.gameObject.CompareTag("Black Door")) {
            if(totalRPotion > 10) {
                totalRPotion -= 10;
                totalPotions -= 10;
                Destroy(other.gameObject);
                //aud.PlayOneShot(doorOpen);
            }
            else {
                Debug.Log("You need to collect 10 reveal potions to open this door.");
            }
        }

        if(other.gameObject.CompareTag("White Door")) {
            if(totalPotions == 30) {
                totalWPotion -= 10;
                totalRPotion -= 10;
                totalSPotion -= 10;
                totalPotions -= 30;
                Destroy(other.gameObject);
                //aud.PlayOneShot(doorOpen);
            }
            else {
                Debug.Log("You need to collect all potions to open this door.");
            }
        }

//         //TO DO
//         //Restart the level if the player didn't collect enough potions to advance
    }

    void Jump() {
        if(isGrounded) {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }
}
