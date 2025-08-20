using UnityEngine;

public class PlayerController : Singleton<PlayerController>, IEventBus
{
    private BasePlayerObject currentControlObject;

    public EventBus EventBus { get; private set; }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventBus?.RaisePlayerInput();
            currentControlObject?.InputAction();
        }
    }

    internal void SetupControlObject(BasePlayerObject baseObject)
    {
        currentControlObject = baseObject;
    }

    public void SetupEventBus(EventBus eventBus)
    {
        EventBus = eventBus;
    }
}
