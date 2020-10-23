﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [Header("Patrol Path")]
        [SerializeField, Range(0.01f, 2)] float gizmosRadius = 0.5f;
        [SerializeField] bool ShowAlways = false;
        private int j = 1;
        private void OnDrawGizmosSelected()
        {
            if (!ShowAlways)
            { 
                for (int i = 0; i < transform.childCount; i++)
                {
                    j = GetNext(i);
                    Gizmos.color = Color.white;
                    Gizmos.DrawSphere(GetWaypoint(i), gizmosRadius);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
                }
            }
        }
        private void OnDrawGizmos()
        {
            if (ShowAlways) { 
                for (int i = 0; i < transform.childCount; i++)
                {
                    j = GetNext(i);
                    Gizmos.color = Color.white;
                    Gizmos.DrawSphere(GetWaypoint(i), gizmosRadius);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
                }
            }
        }

        public int GetNext(int i)
        {
            if (i+1 == transform.childCount) return 0;
            return i+1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
