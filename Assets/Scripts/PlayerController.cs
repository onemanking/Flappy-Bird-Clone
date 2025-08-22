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
    }

    internal void SetupControlObject(BasePlayerObject baseObject)
    {
        CurrentControlObject = baseObject;
    }
}
