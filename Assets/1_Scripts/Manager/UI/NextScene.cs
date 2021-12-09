using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    
    public void SceneLoad(string name)
    {
        SceneManager.LoadScene(name);
    }
}
