using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSpawn : MonoBehaviour
{
    public RootBehavior RootToSpawn;
    public float RootSpawnTimeFrequencySecond = 5.0f;
    public float RootSpawnTimeVariationSecond = 1.0f;
    public BoxCollider2D BoxCollider2D;
    public float SpawnPointVariation = 0.50f;
    public int MaximumRootAround = 5;
    public float CheckSpawnRectangleToleranceMultiplicative = 0.40f;
    private float _currentTimer = 0.0f;
    private SacredTree _sacredTree;

    public void Start()
    {
        _sacredTree = FindObjectOfType<SacredTree>();
        SpriteSpawnTime();
    }

    public void Update()
    {
        if (GameManager.Instance.IsGameStarted == true && GameManager.Instance.IsGamePaused != true && GameManager.Instance.IsGameOver != true)
        {
            _currentTimer -= Time.deltaTime;
            if (_currentTimer <= 0)
            {
                List<Collider2D> col = new List<Collider2D>();
                ContactFilter2D fil = new ContactFilter2D();
                Physics2D.OverlapCircle(this.transform.position, BoxCollider2D.size.x * 0.5f, fil.NoFilter(), col);
                if (col.Count <= MaximumRootAround)
                {
                    SpawnRoot();
                }
                SpriteSpawnTime();
            }
        }
    }

    public void SpriteSpawnTime()
    {
        _currentTimer = RootSpawnTimeFrequencySecond + Random.Range(-RootSpawnTimeVariationSecond, RootSpawnTimeVariationSecond);
        _currentTimer = _currentTimer / GameManager.Instance.GetCurrentScaling();
    }

    public void SpawnRoot()
    {
        Vector3 dir = (_sacredTree.transform.position - transform.position).normalized;
        dir.x = (dir.x + Random.Range(-SpawnPointVariation, SpawnPointVariation)) * BoxCollider2D.size.x /1.3f;
        dir.y = (dir.y + Random.Range(-SpawnPointVariation, SpawnPointVariation)) * BoxCollider2D.size.y /1.7f;
        Vector3 spawnPosition = transform.position + dir;
        Collider2D col = Physics2D.OverlapCircle(spawnPosition, BoxCollider2D.size.x * CheckSpawnRectangleToleranceMultiplicative);
        if (col != null && col.GetComponent<RootSpawn>() != null)
        {
            return;
        }

        RootBehavior rootInstantitated = Instantiate<RootBehavior>(RootToSpawn, spawnPosition, Quaternion.identity);
        rootInstantitated.name = "Root";
        //play anim
    }
}
