/*AIController.cs
 * Used for AI Control
 * Depended on Movement, Combat and Core
 * RPG.Control
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movment;
using RPG.Combat;
using RPG.Core;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [Header("Guard Setttings")]
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] float wayPointDwellTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float wayPointTolerance = 1f;
        [SerializeField] int currentWayPoint = 0;

        private Fighter fighter;
        private GameObject player;
        private Health health;
        private Mover mover;

        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceArrivedAtWayPoint = Mathf.Infinity;
        int currentWayPointIndex = 0;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");
            guardPosition = transform.position;
        }
        //Update
        private void Update()
        {
            if (health.IsDead()) return;
            GameObject player = GameObject.FindWithTag("Player");
            GameObject AIControl = gameObject;

            if (InAttackRangeofPlayer() && fighter.CanAttack(player))
            {
                AttackBehaviour(player);
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWayPoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    timeSinceArrivedAtWayPoint = 0;
                    CycleNextWayPoint();
                }
                nextPosition = GetCurrentWayPoint();
            }
            if(timeSinceArrivedAtWayPoint > wayPointDwellTime)
            {
                mover.startMoveAction(nextPosition);
            }
        }

        private Vector3 GetCurrentWayPoint()
        {
            currentWayPoint = currentWayPointIndex;
            return patrolPath.GetWaypoint(currentWayPointIndex);
        }

        private void CycleNextWayPoint()
        {
                currentWayPointIndex = patrolPath.GetNext(currentWayPointIndex);
        }

        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWayPoint < wayPointTolerance;
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour(GameObject player)
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player.gameObject);
        }
        private bool InAttackRangeofPlayer()
        {
            float DistancetoPlayer = Vector3.Distance(player.transform.position, transform.position);
            return DistancetoPlayer < chaseDistance;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
