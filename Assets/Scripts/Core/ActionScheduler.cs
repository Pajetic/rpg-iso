using UnityEngine;

namespace RPGIso.Core {
    public class ActionScheduler : MonoBehaviour {
        private IAction currentAction;

        public void StartAction(IAction action) {
            if (currentAction != null && currentAction != action) {
                currentAction.CancelAction();
            }
            currentAction = action;
        }

        public void CancelCurrentAction() {
            StartAction(null);
        }
    }
    
}
