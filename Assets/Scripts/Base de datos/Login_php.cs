using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerLogin : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;

    public Player player;

    public void LoginPlayer()
    {
        string username = usernameField.text.Trim();
        string password = passwordField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("El nombre de usuario o la contraseña están vacíos.");
            return;
        }


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
                string responseText = www.downloadHandler.text;
                Debug.Log("Respuesta del servidor: " + responseText);

                // Dividir la respuesta para obtener el estado y el nombre del juego
                string[] responseParts = responseText.Split(':');
                string status = responseParts[0];

                if (status == "success")
                {
                    string gameName = responseParts[1];
                    Debug.Log("Inicio de sesión exitoso. Game Name: " + gameName);

                    PlayerPrefs.SetString("GameName", gameName);
                    PlayerPrefs.SetString("Username", username);
                    PlayerPrefs.Save();

                    player.gameName = gameName;

                    SceneManager.LoadScene("MainMenu");
                }
                else
                {
                    string errorMessage = responseParts[1];
                    Debug.LogError("Error: " + errorMessage);
                }
            }
            else
            {
                Debug.LogError("Error en el inicio de sesión: " + www.error);
            }
        }
    }

}
