﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // External Settings
    public float minShootDelay= 2f;     // 2 Game Seconds
    public float maxShootDelay= 5f;
    public bool  facingRight = true;    // Facing Right
    public float shootStartDist = 10f;  // Distance from Player till Shooting Starts

    // Internal References
    GameObject player;
    EnemyShooter shooter;

    // Internal State
    float   lastShotTime = -1f;         // Last Time when Shot
    
    
    
    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        shooter = GetComponent<EnemyShooter>();

        // Ignore Player Collision
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Character"), 
            LayerMask.NameToLayer("Enemy")
        );
    }

    void FixedUpdate() {
        // Player Died?
        if (!player) {
            Destroy(this);      // Clean up
            return;
        }
        
        // Face the Player
        float dir = player.transform.position.x - transform.position.x;
        Vector2 scale = transform.localScale;
        if ((dir > 0f && !facingRight) || (dir < 0f && facingRight)) {
            scale.x *= -1;
            facingRight = !facingRight;
            transform.localScale = scale;
        }
    }

    void LateUpdate() {
        // Early Return
        if (!player) return;
        
        // Check if Enemy within Distance of Player
        if (Vector2.Distance(transform.position, player.transform.position) <= shootStartDist) {
            // First Check within Range
            if (lastShotTime == -1f) {
                lastShotTime = Time.time;
            }
            
            // Calculate Time since last Shot
            float dTime = Time.time - lastShotTime;


            // Determine to Shoot
            float delayTime = Random.Range(minShootDelay, maxShootDelay);
            if (dTime >= delayTime) {
                shooter.shootAt(player.transform.position);
                lastShotTime = Time.time;
            }
        } else {    // Far from Range
            lastShotTime = -1f;
        }
    }
}
