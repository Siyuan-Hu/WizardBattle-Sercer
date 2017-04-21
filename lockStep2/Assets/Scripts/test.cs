using UnityEngine;
using System.Collections;
using Pomelo.DotNetClient;
using SimpleJson;

public class test : MonoBehaviour {
	private PomeloClient pc;
	public GameObject player;
	private int state=0;

	private int count = 0;
	private int upCount=0;
	private int downCount=0;
	private int leftCount=0;
	private int rightCount=0;

	// Use this for initialization
	void Start () {
		string host = "128.237.211.68";
		int port = 3010;
		pc = new PomeloClient(host, port);
		pc.connect(null, (data)=>{
			JsonObject msg = new JsonObject();
			msg["uid"] = "hehe";
			pc.request("connector.entryHandler.entry", msg, OnQuery);
		});

		pc.on("onChat", (data) => {
			OnChat(data);
		});
	}
	
	// Update is called once per frame
	void Update () {
		string route = "connector.entryHandler.send";
		if (Input.GetKeyDown (KeyCode.UpArrow))
		{
			JsonObject msg = new JsonObject();
			msg["direction"] = "up";
			pc.request(route, msg, OnQuery);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow))
		{
			JsonObject msg = new JsonObject();
			msg["direction"] = "down";
			pc.request(route, msg, OnQuery);
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow))
		{
			JsonObject msg = new JsonObject();
			msg["direction"] = "left";
			pc.request(route, msg, OnQuery);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow))
		{
			JsonObject msg = new JsonObject();
			msg["direction"] = "right";
			pc.request(route, msg, OnQuery);
		}

		while (upCount) 
		{
			player.transform.position += new Vector3 (0, 1, 0);
			upCount--;
		}
		while (downCount)
		{
			player.transform.position += new Vector3 (0, -1, 0);
			downCount--;
		}
		while (leftCount)
		{
			player.transform.position += new Vector3 (-1, 0, 0);
			leftCount--;
		}
		while (rightCount)
		{
			player.transform.position += new Vector3 (1, 0, 0);
			rightCount--;
		}
	}

	void OnQuery(JsonObject result){
		//print (result["msg"]);
		//print (result["direction"]);
	}

	void OnChat(JsonObject result){
		count++;
		//print (count);
		if (result ["msg"].ToString() == "up") 
		{
			upCount++;
		}
		if (result ["msg"].ToString() == "down") 
		{
			downCount++;
		}
		if (result ["msg"].ToString() == "left") 
		{
			leftCount++;
		}
		if (result ["msg"].ToString() == "right") 
		{
			rightCount++;
		}
		//print (result);
		//print (result["direction"]);
	}
}
