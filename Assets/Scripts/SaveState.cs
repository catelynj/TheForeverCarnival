using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
    public int score;
    public float[] playerPosition;
    public SaveState(){    }
    public SaveState(int score, Vector3 location)
    {
        this.score = score;
        this.playerPosition = new float[] { location.x, location.y, location.z };
    }
    public Vector3 getPlayerLocation()
    {
        return new Vector3(playerPosition[0], playerPosition[1], playerPosition[2]);
    }
    /*Nothing here is working atm*/
    public float[] getCurrentPos()
    {
        return null;//new float[] { pos.x, pos.y, pos.z };
    }

    //private GameObject player = GameObject.FindGameObjectWithTag("Player");
    //private Vector3 pos = player.transform.position;
    /**/

}