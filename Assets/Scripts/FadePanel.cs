using UnityEngine.Events;
using UnityEngine;

public class FadePanel : MonoBehaviour
{
    [Space, SerializeField] private UnityEvent onFadeCompleted;
    [Space, SerializeField] private UnityEvent onUnfadeFinished;

    /// <summary>
    /// Executes a series of events defined in the editor
    /// </summary>
    public void FadeCompleted() => onFadeCompleted.Invoke();

    /// <summary>
    /// Executes a series of events defined in the editor
    /// </summary>
    public void UnfadeFinished() => onUnfadeFinished.Invoke();
}
