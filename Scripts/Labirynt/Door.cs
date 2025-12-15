using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [Tooltip("Jeœli true -> ³aduje scenê, jeœli false -> obiekt zostanie zniszczony")]
    public bool isExitDoor = false;

    [Tooltip("Nazwa sceny do za³adowania (u¿ywane gdy isExitDoor == true)")]
    public string sceneToLoad = "EndScene";

    public void Activate()
    {
        if (isExitDoor)
        {
            // upewnij siê ¿e scena jest dodana w Build Settings
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}