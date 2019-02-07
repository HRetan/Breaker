using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Upload());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Upload()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("filed1=foo&field2=bar"));
        formData.Add(new MultipartFormDataSection("my file data", "myfile.txt"));

        UnityWebRequest www = UnityWebRequest.Post("http://www.my-server.com/myform", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
            Debug.Log("form upload completed");
    }
}
