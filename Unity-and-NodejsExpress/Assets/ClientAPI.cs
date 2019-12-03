using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class ClientAPI : MonoBehaviour {

    public string url;
    public EnemyViewController enemyViewController;
	 void Start()
    {
        StartCoroutine(Get(url));
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
}
