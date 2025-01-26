using UnityEngine;

public class OrbitingMoon : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.position += new Vector3(0, 0, 0);
        }
    }

    public float degreesPerSecond = 2.0f;
    void Update()
    {
        transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0);
    }
}
