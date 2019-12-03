using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class ClientAPI : MonoBehaviour {

    public string url;
    public EnemyViewController enemyViewController;

    public string getUrl = "localhost:3000/enemy";
    public string postUrl = "localhost:3000/enemy/create";

	/* void Start()
        {
            StartCoroutine(Get(url));
        }
    */
    
    void Start()
    {
        var enemy = new Enemy()
        {
            id = 100,
            name = "Balrog",
            health = 1000,
            attack = 2500
        };

        StartCoroutine(Post(postUrl, enemy));
    }


    public IEnumerator Get(string url)
	{
		using(UnityWebRequest www = UnityWebRequest.Get(url))
		{
            yield return www.Send();

			if (www.isError)
			{
				Debug.Log(www.error);
			}
			else
			{
				if (www.isDone)
				{
					// handle the result
					var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    var enemy = JsonUtility.FromJson<Enemy>(result);

                    enemyViewController.DisplayEnemyData(enemy.name, enemy.health.ToString(), enemy.attack.ToString());
				}
				else
				{
					//handle the problem
					Debug.Log("Error! data couldn't get.");
				}

			}
		}
	}
    public IEnumerator Post(string url, Enemy enemy)
    {
        var jsonData = JsonUtility.ToJson(enemy);
        Debug.Log(jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));

            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    // handle the result
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    result = "{\"result\":" + result + "}";
                    var resultEnemyList = JsonHelper.FromJson<Enemy>(result);

                    foreach (var item in resultEnemyList)
                    {
                        Debug.Log(item.name);
                    }
                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }
}
