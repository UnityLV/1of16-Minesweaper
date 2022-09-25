using UnityEngine;
using UnityEngine.Events;

public class GuideView : MonoBehaviour
{
    public event UnityAction Closed;

    public void Close()
    {
        Destroy(gameObject);
        Closed?.Invoke();
    }
}
