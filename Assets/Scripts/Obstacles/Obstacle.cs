using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    
    [Header("FX")]
    [SerializeField] private GameObject enemyFx;
    [SerializeField] private GameObject obstacleFx;

    
   
    [SerializeField] private int damagePlayerAmount = 1;
    public static Action OnEnemyKilled;
    public static Action<int> OnPlayerDamaged;
    


    private enum EnemyType
    {
        Enemy,
        Obstacle
    }
    [SerializeField] private EnemyType enemyType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_BULLET_TAG))
        {
            switch (enemyType)
            {
                case EnemyType.Enemy:
                    AudioManager.Instance.PlaySfx(2);
                    OnEnemyKilled?.Invoke();
                    Instantiate(enemyFx, transform.position+Vector3.up, Quaternion.identity);
                    break;
                
                case EnemyType.Obstacle:
                    AudioManager.Instance.PlaySfx(1);
                    Instantiate(obstacleFx, transform.position, Quaternion.identity);
                    break;
            }
            
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            switch (enemyType)
            {
                case EnemyType.Enemy:
                    AudioManager.Instance.PlaySfx(2);
                    OnEnemyKilled?.Invoke();
                    Instantiate(enemyFx, transform.position+Vector3.up, Quaternion.identity);
                    break;
                
                case EnemyType.Obstacle:
                    AudioManager.Instance.PlaySfx(1);
                    Instantiate(obstacleFx, transform.position, Quaternion.identity);
                    OnPlayerDamaged?.Invoke(damagePlayerAmount);
                    break;
            }
            
            Destroy(gameObject);
        }
    }
}
