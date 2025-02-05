using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public float speed = 3.0f;
    public float zDistance = 10.0f;
    public float allowableOffset = 3.0f;
    public GameObject bg0;
    public float bg0speed = .8f;
    public GameObject bg1;
    public float bg1speed = .6f;
    public GameObject bg2;
    public float bg2speed = .4f;

    private GameObject player;

    // Improvements to consider:
    // - Easing movement at start and end
    // - Catching up more quickly if player is too far from center


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + Vector3.back * zDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position + Vector3.back * zDistance) > allowableOffset)
        {
            Vector3 oldPos = transform.position; 
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + Vector3.back * zDistance, speed * Time.deltaTime);
            Vector3 newPos = transform.position;

            Vector3 delta = newPos - oldPos;
            bg0.transform.position += delta * bg0speed;
            bg1.transform.position += delta * bg1speed;
            bg2.transform.position += delta * bg2speed;
        }
    }
}
