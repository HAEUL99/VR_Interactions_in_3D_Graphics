using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EnumManager;

public class churchPlantMissionComplArgs : EventArgs
{
    public MissionType _missionType;
}


public class checkPlants : MonoBehaviour
{
    private struct Plants
    {
        public Plants(GameObject plant, bool isGrow)
        {
            Plant = plant;
            IsGrow = isGrow;
        }

        public GameObject Plant { get; }
        public bool IsGrow { get; }
    }

    [SerializeField]
    private GameObject[] plantObjs;

    private Plants[] plants;

    public event EventHandler churchPlantMissionCompl;
    public void Start()
    {
        plants = new Plants[plantObjs.Length];

        for (int i = 0; i < plantObjs.Length; i++)
        {
            plants[i] = new Plants(plantObjs[i], false);
        }
    }


    public void SetUp(int numOfplant)
    {
        plants[numOfplant] = new Plants(plantObjs[numOfplant], true);

        int count = 0;
        for (int i = 0; i < plantObjs.Length; i++)
        {
            if (plants[i].IsGrow)
                count++;
        }

        if (count == plantObjs.Length)
        {
            CompletePlantMission();
        }
    }

    private void CompletePlantMission()
    {
        Debug.Log("plants");
        churchPlantMissionComplArgs arg = new churchPlantMissionComplArgs
        {
            _missionType = EnumManager.MissionType.plnats
        };
        this.churchPlantMissionCompl(this, arg);
    }

}
