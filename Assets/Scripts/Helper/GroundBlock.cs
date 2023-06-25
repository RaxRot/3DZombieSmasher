using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlock : MonoBehaviour
{
    [SerializeField] private Transform otherBlock;
    [SerializeField] private float halfLength = 100f;
    private Transform _player;
    private float _endOffSet=10;

    private void Start()
    {
        _player = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
    }

    private void Update()
    {
        MoveGround();
    }

    private void MoveGround()
    {
        if (transform.position.z+halfLength<_player.transform.position.z-_endOffSet)
        {
            transform.position = new Vector3(otherBlock.transform.position.x, otherBlock.transform.position.y,
                otherBlock.transform.position.z + halfLength * 2);
        }
    }
}
