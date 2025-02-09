using UnityEngine;
using UnityEngine.InputSystem;

public class BreakOut : MonoBehaviour
{
    public InputActionReference inputAction;
    private Vector3 position1 = new(0, 2, 0);
    private Vector3 position2 = new(30, 2, 0);
    private bool isPosition1 = true;

    void Start()
    {
        inputAction.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputAction.action.triggered)
        {
            if (isPosition1)
            {
                transform.position = position2;
            }
            else
            {
                transform.position = position1;
            }
            isPosition1 = !isPosition1;
        }
    }
}
