using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private MonoBehaviour lastAction;

        public void StartAction(MonoBehaviour action) {
            if (lastAction != null && action != lastAction) {
                Debug.Log("Canceling " + lastAction);
            }
            lastAction = action;
        }
    }
}