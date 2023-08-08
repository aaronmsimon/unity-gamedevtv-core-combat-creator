using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] private float gizmoRadius = 1f;
        private void OnDrawGizmos() {
            Gizmos.color = Color.white;
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i) + Vector3.up * gizmoRadius, gizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        private Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

        private int GetNextIndex(int i)
        {
            return i < transform.childCount - 1 ? i + 1 : 0;
        }
    }
}
