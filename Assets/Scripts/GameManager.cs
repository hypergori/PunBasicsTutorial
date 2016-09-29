using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;

namespace jp.co.leapman.PUNtutorial
{
    public class GameManager : Photon.PunBehaviour
    {
        static public GameManager Instance;
        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;
        public void Start()
        {
            Instance = this;
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else {
                Debug.Log("We are Instantiating LocalPlayer from " + SceneManager.GetActiveScene().name);
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                if (PlayerManager.LocalPlayerInstance == null)
                {
                    Debug.Log("We are Instantiating LocalPlayer from " + SceneManager.GetActiveScene().name);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
                }
                else {
                    Debug.Log("Ignoring scene load for " + SceneManager.GetActiveScene().name);
                    //when scene reloading , existing player UI is gone with canvas. So re-creating UI
                    PlayerManager localPlayer = PlayerManager.LocalPlayerInstance.GetComponent<PlayerManager>();
                    if (!localPlayer.playerUIexists())
                    {
                        Debug.Log("Re-creating Local player UI");
                        localPlayer.setupUi();
                    }
                }
            }
        }

        //Maybe Tutorial is wrong //http://doc.photonengine.com/en-us/pun/current/tutorials/pun-basics-tutorial/player-instantiation
        //void OnLevelWasLoaded(int level)
        //{
        //    // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
        //    if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
        //    {
        //        Debug.Log("PhotonNetwork : Level loaded because out of floor : " );
        //        transform.position = new Vector3(0f, 5f, 0f);
        //    }
        //}

        #region Private Methods


        void LoadArena()
        {
            if (!PhotonNetwork.isMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.Log("PhotonNetwork : Loading Level : " + PhotonNetwork.room.playerCount);
            PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.room.playerCount);
        }


        #endregion
        #region Photon Messages


        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        override public void OnLeftRoom()
        {
            Debug.Log("Left the room and load the launcher scenesss");
            SceneManager.LoadScene(0);
        }

        public override void OnPhotonPlayerConnected(PhotonPlayer other)
        {
            Debug.Log("OnPhotonPlayerConnected() " + other.name); // not seen if you're the player connecting


            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


                LoadArena();
            }
        }


        public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
        {
            Debug.Log("OnPhotonPlayerDisconnected() " + other.name); // seen when other disconnects


            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


                LoadArena();
            }
        }

        #endregion


        #region Public Methods


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


        #endregion
    }
}