using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerRegistration : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TMP_InputField gameNameField;

    public void RegisterPlayer()
    {
        string username = usernameField.text;
        string password = passwordField.text;
        string gameName = gameNameField.text;

        StartCoroutine(RegisterCoroutine(username, password, gameName));
    }

    private IEnumerator RegisterCoroutine(string username, string password, string gameName)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("game_name", gameName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/chefrenzy/register_player.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error en el registro: " + www.error);
            }
        }
    }
}