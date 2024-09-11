using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningStageManager : MonoBehaviour, IStageObserver
{
    public static RunningStageManager Instance { get; private set; }

    public Transform FinishLine;

    [SerializeField] private PlayersScript[] players;
    private int countFinish;
    private Dictionary<StageEvents, System.Action> _stageEventHandlers;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        _stageEventHandlers = new Dictionary<StageEvents, System.Action>()
        {
            {StageEvents.Completed, OnStageCompleted},
            {StageEvents.PlayerFinish, OnPlayerFinished},
        };

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CountPos();
        if (countFinish == 4) {
            OnNotify(StageEvents.Completed);
        }
    }

    public void CountPos()
    {
        for (int i = 0; i < players.Length; i++)
        {
            int pos = 0;
            int j = 0;
            while (j < players.Length)
            {
                if (players[j].DistanceToFinish <= players[i].DistanceToFinish)
                    pos++;
                j++;
            }
            players[i].Position = pos;
            players[i].PlayerCard.ChangePosText(pos);
        }
    }


    public void OnNotify(StageEvents stageEvent)
    {
        if (_stageEventHandlers.ContainsKey(stageEvent))
        {
            _stageEventHandlers[stageEvent]();
        }
    }


    public void OnPlayerFinished()
    {
        GameManager.Instance.countFinish++;
        countFinish += 1;
    }

    public void OnStageCompleted ()
    {
    
    }

    private void OnEnable()
    {
        foreach (var player in players)
        {
            player.AddObserver(this);
        }   
    }

    private void OnDisable()
    {
        foreach (var player in players)
        {
            player.RemoveObserver(this);
        }
    }

}
