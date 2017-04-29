﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour {

	string _room = "Tutorial_Convrge";

	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	void OnJoinedLobby()
	{
		Debug.Log("joined lobby");

		RoomOptions roomOptions = new RoomOptions() { };
		PhotonNetwork.JoinOrCreateRoom(_room, roomOptions, TypedLobby.Default);
	}

	void OnJoinedRoom()
	{
		PhotonNetwork.Instantiate("NetworkedPlayerOld 2", Vector3.zero, Quaternion.identity, 0);
	}
}
