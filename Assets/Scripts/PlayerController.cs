using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private BasePlayerObject currentControlObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventBus.RaisePlayerInput();
            currentControlObject?.InputAction();
        }
    }

    internal void SetupControlObject(BasePlayerObject baseObject)
    {
        currentControlObject = baseObject;
    }
}
