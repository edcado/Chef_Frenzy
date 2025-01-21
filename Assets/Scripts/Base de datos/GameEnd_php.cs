using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameEnd_php : MonoBehaviour
{
    public void SendPlayerData(string username, int platesDelivered, int gamesPlayed, int wins)
    {
        StartCoroutine(SendPlayerDataCoroutine(username, platesDelivered, gamesPlayed, wins));
    }

    private IEnumerator SendPlayerDataCoroutine(string username, int platesDelivered, int gamesPlayed, int wins)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("platesDelivered", platesDelivered);
        form.AddField("gamesPlayed", gamesPlayed);
        form.AddField("wins", wins);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/chefrenzy/game_end.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Datos enviados con éxito: " + www.downloadHandler.text);
                Debug.Log($"Enviando -> username: {username}, platesDelivered: {platesDelivered}, gamesPlayed: {gamesPlayed}, wins: {wins}");
            }
            else
            {
                Debug.LogError("Error al enviar datos: " + www.error);
            }
        }
    }

    


}
