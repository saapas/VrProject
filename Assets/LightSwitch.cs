using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public Light myLight;
    public InputActionReference inputAction;
    void Start()
    {
        inputAction.action.Enable();
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputAction.action.triggered)
        {
            myLight.color = new Color(Random.value, Random.value, Random.value, 1);
        }
    }
}
