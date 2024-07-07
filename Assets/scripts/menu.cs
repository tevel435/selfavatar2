using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void option1()
    {
        SceneManager.LoadScene("with_avatar");
    }

    public void option2()
    {
        SceneManager.LoadScene("wo_avatar");
    }

    public void option3()
    {
        SceneManager.LoadScene("fat_cooper");
    }
}
