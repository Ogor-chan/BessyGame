using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Data", menuName = "Data/LastCheckpoint")]

public class GameSaveStateByLastCheckpoint : MonoBehaviour
{
    public enum State {EmptyLevel, Intro, CrystalLake }
    public State lastSavedState;
    public int checkpointNumberInScene;
    public Vector2 currentCheckpointVector;
    



    public void LoadLastSavedCheckpoint()
    {
        Debug.Log("Load Game on: "+ lastSavedState + " on " + checkpointNumberInScene + " checkpoint");
    }



}
