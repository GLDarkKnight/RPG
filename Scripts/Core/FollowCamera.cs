/*FollowCamera.cs
 * RPG.Core
 * NOTE this follows the player and is simple system.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [Header("Camera Settings")]
        [SerializeField] Transform target;

        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}