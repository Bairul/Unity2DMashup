using UnityEngine;

public class SetResolution : MonoBehaviour
{
    void Awake()
    {
        Screen.SetResolution(640, 360, FullScreenMode.Windowed);
    }
}
