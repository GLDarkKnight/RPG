/*Mover.cs
 * Depended on Core
 * RPG.Movment
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movment
{
    public class Mover : MonoBehaviour, IAction
    {
        Health health;
        private NavMeshAgent navMeshAgent;
        private void Start()
        {
            health = GetComponent<Health>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }
        void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localvelocity = transform.InverseTransformDirection(velocity);
            float lspeed = localvelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", lspeed);
        }
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        public void MoveTo (Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }
        public void startMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }
    }
}