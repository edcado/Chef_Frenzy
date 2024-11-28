using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerLogin : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;


    public void LoginPlayer()
    {
        string username = usernameField.text;
        string password = passwordField.text;

        StartCoroutine(LoginCoroutine(username, password));
    }

    private IEnumerator LoginCoroutine(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/chefrenzy/login_player.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error en el inicio de sesión: " + www.error);
            }
        }
    }
}