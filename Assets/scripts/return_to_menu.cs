using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class return_to_menu : MonoBehaviour
{
    public void go_menu()
    {
        //load specific scene, not the next one in the build order
        SceneManager.LoadScene("menu");
    }
}
