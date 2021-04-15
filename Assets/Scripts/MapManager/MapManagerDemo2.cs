using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class MapManagerDemo2: MapManager
{


    int rndSpeed;
    int rndHP;
    int rndScale;
    int rndAtt;
    int rnd;
    int att = 3;
    float meleeAttackEnemyScale = 1;

    protected override void Start()
    {
        base.Start();

        phase = 0;

        OnLoadPrefab(0,"MeleeAttackPathFinding");
        OnLoadPrefab(1,"Black/EnemySideStepTentacle");


    }

    public override void ActivatePhase(int phase)
    {
        switch (phase)
        {
            case 0:
                Log.log("on phase 0");

                break;
            case 1:
                for (int i = 0; i < 4; i++)
                {
                    SpawnMelleeAttackEnemy(0, spawnerPoints[i].position);
                }
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
        }
    }


    void SpawnMelleeAttackEnemy(int i, Vector3 pos)
    {
        //ChooseARandomStrengthForEnemy();
        enemyScale = new Vector3(meleeAttackEnemyScale, meleeAttackEnemyScale, 1.0f);
        enemyAttack = att;
        enemySpeed = 2.0f;
        enemyMaxHP = 50;
        enemyHP = 50;
        enemyColor = Colors.Black;
        SpawnEnemy(i, pos);
    }

    void SpawnTentacleAttackEnemy(int i, Vector3 pos)
    {
        enemyHP = 300;
        enemyMaxHP = 300;
        enemySpeed = 1.0f;
        enemyScale = new Vector3(1.0f, 1.0f, 1.0f);
        enemyColliderScale = new Vector3(0.9f, 0.9f);
        enemyAttack = 4;
        enemySpeed = 1.0f;
        enemyColor = Colors.Black;
        SpawnEnemy(i, pos);
    }
    void ChooseARandomStrengthForEnemy()
    {
        rnd = UnityEngine.Random.Range(1, 3);
        Log.log("rnd " + rnd);
        if (rnd == 1)
        {
            meleeAttackEnemyScale = rnd;
            att = rnd;
            enemyHP = 50;
            enemyMaxHP = 50;
            enemySpeed = 4 / rnd;
        }
        else
        {
            rnd = UnityEngine.Random.Range(2, 4);
            Log.log("rnd2 " + rnd);
            meleeAttackEnemyScale = rnd;
            att = 2 * rnd;
            enemyHP = rnd * 50;
            enemyMaxHP = rnd * 50;
            enemySpeed = 4 / rnd;
            enemyColliderScale = new Vector3(0.9f, 0.9f) / 2;
            Log.log("hp " + enemyHP);
        }
    }

}
