using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCasting : MonoBehaviour
{
    List<GameObject> casts;
    List<Transform> fireballSpawns;//use this for bullethell attack
    List<Transform> laserSpawns;
    List<Transform> addSpawns;
    int phase = 1;

    public void Cast()
    {
        switch(Random.Range(0, 11))//random chance to cast certain abilities (will weight soon
        {
            case >= 0 and <= 4:
                //for loop to pick from spawnpoints to do stuff
                for(int i = 0; i < phase; i++)
                {
                    Instantiate(casts[0], fireballSpawns[Random.Range(0, fireballSpawns.Count)]);
                }
                break;
            case >4 and <= 6:
                for(int i = 0; i < phase; i++)
                {
                    Instantiate(casts[1], laserSpawns[Random.Range(0, laserSpawns.Count)]);
                }
                break;
            case > 6 and <= 8:
                for(int i = 0; i < phase; i++)
                {
                    Instantiate(casts[2], fireballSpawns[Random.Range(0, fireballSpawns.Count)]);
                }
                break;
            case >8 and <= 10:
                for(int i = 0; i < phase; i++)
                {
                    Instantiate(casts[3], addSpawns[Random.Range(0, addSpawns.Count)]);
                }
                break;
            default:
                break;
        }
    }
}
