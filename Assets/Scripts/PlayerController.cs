using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    internal BasePlayerObject CurrentControlObject { get; private set; }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventBus.RaisePlayerInput();
            CurrentControlObject?.InputAction();
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.I))
        {
            CurrentControlObject?.ToggleTrigger();
        }
#endif
    }

    internal void SetupControlObject(BasePlayerObject baseObject)
    {
        CurrentControlObject = baseObject;
    }
}
