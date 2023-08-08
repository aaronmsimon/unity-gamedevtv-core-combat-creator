using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] private Color pathColor = Color.white;
        [SerializeField] private float waypointRadius = 1f;
        private void OnDrawGizmos() {
            Gizmos.color = pathColor;

            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i) + Vector3.up * waypointRadius, waypointRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

        public int GetNextIndex(int i)
        {
            return i < transform.childCount - 1 ? i + 1 : 0;
        }
    }
}
