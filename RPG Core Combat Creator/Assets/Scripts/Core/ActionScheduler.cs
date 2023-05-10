using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction lastAction;

        public void StartAction(IAction action) {
            if (lastAction != null && action != lastAction) {
                lastAction.Cancel();
            }
            lastAction = action;
        }
    }
}