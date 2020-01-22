using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;
using System.Net;
using System.Text;
using System.IO;
using System;

public class LoginScript : MonoBehaviour
{

    // Convencional Login
    public InputField userName;
    public InputField password;
    public GameObject login;

    // Components
    public GameObject modalError;
    public Text textError;

    private HttpWebRequest request;

    private const string ULR_API = "http://localhost:3000";

    // Start is called before the first frame update
    void Start()
    {
        modalError.SetActive(false);
    }

    public void CloseModal() 
    {
        modalError.SetActive(false);
    }

    public void UserLogin()
    {
        if(userName.text!= "" && password.text != ""){
            try
            {
                request = WebRequest.Create(ULR_API + "/login") as HttpWebRequest;
                request.Method = "POST";
                string postData = "username=" + userName.text + "&password=" + password.text;
                // Debug.Log(postData);
                byte[] data = Encoding.ASCII.GetBytes(postData);

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

                string pageContent = myStreamReader.ReadToEnd();
                if (pageContent != "Invalid password" && pageContent != "Invalid user")
                {
                    PlayerPrefs.SetString("Token", pageContent);
                    SceneManager.LoadScene("MainMenu");
                }
                else
                {
                    modalError.SetActive(true);
                    textError.text = pageContent;

                }

                myStreamReader.Close();
                responseStream.Close();

                response.Close();
            }
            catch (Exception)
            {
                textError.text = "Ha habido un problema con la conexión";
                modalError.SetActive(true);
            }
        }
        else
        {
            textError.text = "Debe rellenar todos los campos";
            modalError.SetActive(true);
        }
            
    }
}
