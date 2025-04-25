using System;
using UnityEngine;

namespace RPGIso.Control {
    public class PatrolPath : MonoBehaviour
    {
        // TODO don't use child
        private Transform GetChildAtIndex(int index) {
            return transform.GetChild(index % transform.childCount);
        }
        
        public Vector3 GetPatrolPosition(int index) {
            return GetChildAtIndex(index).position;
        }

        public int GetNextPatrolPointIndex(int index) {
            return (index + 1) % transform.childCount;
        }
        
        private void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < transform.childCount; i++) {
                Transform child = transform.GetChild(i);
                Transform nextChild = transform.GetChild(GetNextPatrolPointIndex(i));
                Gizmos.DrawSphere(child.position, .2f);
                Gizmos.DrawLine(child.position, nextChild.position);
            }
        }
    }
}
