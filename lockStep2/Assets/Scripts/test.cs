using UnityEngine;
using System.Collections;
using Pomelo.DotNetClient;
using SimpleJson;

public class test : MonoBehaviour {
	private PomeloClient pc;
	public GameObject player;
	private int state=0;

	// Use this for initialization
	void Start () {
		string host = "128.237.195.212";
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

		if (state == 1) 
		{
			player.transform.position += new Vector3 (0, 1, 0);
			state = 0;
		}
		if (state == 2) 
		{
			player.transform.position += new Vector3 (0, -1, 0);
			state = 0;
		}
		if (state == 3) 
		{
			player.transform.position += new Vector3 (-1, 0, 0);
			state = 0;
		}
		if (state == 4) 
		{
			player.transform.position += new Vector3 (1, 0, 0);
			state = 0;
		}
	}

	void OnQuery(JsonObject result){
		if (result ["msg"].ToString() == "up") 
		{
			state = 1;
		}
		if (result ["msg"].ToString() == "down") 
		{
			state = 2;
		}
		if (result ["msg"].ToString() == "left") 
		{
			state = 3;
		}
		if (result ["msg"].ToString() == "right") 
		{
			state = 4;
		}
		//print (result["msg"]);
		//print (result["direction"]);
	}

	void OnChat(JsonObject result){
		print (result);
		//print (result["direction"]);
	}
}
