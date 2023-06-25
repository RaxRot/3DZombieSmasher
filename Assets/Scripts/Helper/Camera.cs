using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform _targetToFollow;
    [SerializeField]private Vector3 offset;

    private void Start()
    {
        _targetToFollow = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = _targetToFollow.position + offset;
    }
}
