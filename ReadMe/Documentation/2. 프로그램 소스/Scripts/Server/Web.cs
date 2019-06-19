using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetDate());
        //StartCoroutine(GetUsers());
        StartCoroutine(Login("hi", "bye")); 
        //StartCoroutine(RegisterUser("hi", "bye"));
    }

    IEnumerator GetDate()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://dg-test.iptime.org//Graduationwork/GetDate.php"))
        {
            // Request and wait for the desired page.
            yield return www.Send();   
     
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                byte[] results = www.downloadHandler.data;
            }
        }
    }

    IEnumerator GetUsers()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://http://guide94.cafe24.com/graduationwork/GetUsers.php"))
        {
            // Request and wait for the desired page.
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                byte[] results = www.downloadHandler.data;
            }
        }
    }

    IEnumerator Login(string username, string password)
    {
        //WWWForm form = new WWWForm();
        //form.AddField("loginUser", username);
        //form.AddField("loginPass", password);
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("loginUser", username));
        form.Add(new MultipartFormDataSection("loginPass", password));

        using (UnityWebRequest www = UnityWebRequest.Post("http://http://guide94.cafe24.com/graduationwork/GetUsers.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://http://guide94.cafe24.com/graduationwork/GetUsers.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
