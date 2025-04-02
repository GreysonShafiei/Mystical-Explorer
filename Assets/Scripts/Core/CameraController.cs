using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room Camera
    [SerializeField]private float speed;
    private float currentPositionX;
    private Vector3 velocity = Vector3.zero;

    //Follow Player
    //[SerializeField] private Transform player;
    //[SerializeField] private float offsetDistance;
    //[SerializeField] private float camSpeed;
    //private float lookAhead;

    private void Update()
    {
        //Room Camera
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPositionX, transform.position.y, transform.position.z), ref velocity, speed);

        //Follow Player
        //transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        //lookAhead = Mathf.Lerp(lookAhead, (offsetDistance * player.localScale.x), Time.deltaTime * camSpeed);
    }

    public void MoveToNextRoom(Transform _nextRoom)
    {
        currentPositionX = _nextRoom.position.x;
    }
}
