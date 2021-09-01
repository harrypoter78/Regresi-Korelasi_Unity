using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static string _ukuransepatu;
    public GameObject inputField;

    public void MulaiTemukan()
    {
        _ukuransepatu = inputField.GetComponent<Text>().text;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
