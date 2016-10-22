# PunBasicsTutorial Project
This is the Unity project which implemented the PUN(Photon Unity Network) Basics Tutorial.
The URL of the tutorial is , 

http://doc.photonengine.com/en-us/pun/current/tutorials/pun-basics-tutorial/intro

This is good tutorial to learn

1. Setup PUN application ID and Project setup with asset store
1. Lobby and Room with Launcher Scene
1. Master Client and Scene Synchronizaton
1. Networked Instantiation and Resouces folder 
1. PhotonView and Auto-Synchronization of Transform/Animation
1. Manual synchronization using IPunObservable
1. PUN Callbacks when new players join

But, As of today(29Sep2016), the PUN tutorial has some issues like using deprecated Unity api and missing/wrong instructions.
I managed to find the solution and finally it works now. and I publish this here.

The issues I found are  
## Player re-position
logic using Raycast to be implemented in GameManager instead of  Player Manager  
## Health and Beaming not synch
It doesnot mention to register the IPunObservablized PlayerManager to PhotonView observed Components list
## Camera doesnot follow local player
Transform is not assigned
## Player UI gets disapear
When Scene switches ,Player UI gets removed with Canvas. We need addtional logic to re-create it.
## some Unity API are old for Unity 5.4

#Development Environment
Unity 5.4.0f3
Pun Package v1.76

#How to use
This project doesnot contain the PUN unity package.
After you get this project you need to import the PUN unity pakcage by downloading from Unity Asset Store

