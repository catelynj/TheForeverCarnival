using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
    public int score;
    public Vector3 playerPosition;
    public SaveState(){    }
    public SaveState(int score, Vector3 location)
    {
        this.score = score;
        this.playerPosition = location;
    }
    public Vector3 getPlayerLocation()
    {
        return new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
    }

}