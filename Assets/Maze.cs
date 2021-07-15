using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public static Maze Instance { get; internal set; }
    public Camera mainCamera;
    public GameObject playerCam, mazeCam;
    public GameObject mazeLine;


    private void Awake()
    {
        Instance = this;
    }

    public void KeyObtained()
    {
        throw new NotImplementedException();
    }
    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInteractions>().CanShowMap = true;
            mazeCam.SetActive(true);
            playerCam.SetActive(false);
            mainCamera.orthographic = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInteractions>().CanShowMap = false;
            BackToPlayerView();
        }
    }

    public void BackToPlayerView()
    {
        mazeCam.SetActive(false);
        playerCam.SetActive(true);
        mainCamera.orthographic = false;
    }

    public void EnableMap(bool value)
    {
        mazeLine.SetActive(value);
    }
}
