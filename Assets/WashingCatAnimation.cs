// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class WashingCatAnimation : MonoBehaviour
// {
//     public Animator animator;
//     public float animationDuration = 20f;
//     private float timer = 0f;
//     private bool isAnimating = false;
    

//     private void Update()
//     {
//         if (!isAnimating)
//         {
//             // Start the animation
//             isAnimating = true;
//             animator.SetBool("isCooking", true);
//             timer = 0f;
//         }

//         if (isAnimating)
//         {
//             timer += Time.deltaTime;
//             if (timer >= animationDuration)
//             {
//                 // Stop the animation
//                 animator.SetBool("isCooking", false);
//                 isAnimating = false;
//             }
//         }
//     }
// }