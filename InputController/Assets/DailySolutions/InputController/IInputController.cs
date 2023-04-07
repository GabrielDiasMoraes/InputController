using UnityEngine.Events;

namespace DailySolutions.InputController
{
    public interface IInputController
    {
        public void UpdateInputs();
    
        public UnityAction<PointerData[]> OnNewPointer { get; set; }
        public UnityAction<PointerData[]> OnUpdatePointer { get; set; }
        public UnityAction<PointerData[]> OnExitPointer { get; set; }
    
    }
}
