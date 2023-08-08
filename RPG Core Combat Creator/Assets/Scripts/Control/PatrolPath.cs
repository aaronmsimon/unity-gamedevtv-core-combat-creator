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
                Gizmos.DrawSphere(transform.GetChild(i).position + Vector3.up * gizmoRadius, gizmoRadius);
            }
        }
    }
}
