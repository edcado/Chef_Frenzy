using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameEnd_php : MonoBehaviour
{ 
    public void GameEnd(string username, int platesDelivered/*, int IngredientsGenerated, int platesCooked*/)
    {
        
        StartCoroutine(GameEndCorutine(username, platesDelivered));
    }

    private IEnumerator GameEndCorutine(string username, int platesDelivered/*, int IngredientsGenerated*/)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        //form.AddField("IngredientsGenerated", IngredientsGenerated);
        form.AddField("platesDelivered", platesDelivered);

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
