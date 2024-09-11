using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersScript : MonoBehaviour
{
    protected List<IStageObserver> _observers = new List<IStageObserver>();

    public int playerNumber;
    private float speed;
    public Color playerColor;  
    public KeyCode tapKey;
    public BuffHolder BuffHolder;
    public int StageScore;
    public int Position;
    
    public int Hp;
    [SerializeField] private HealthBarScript HpBar;
    [SerializeField] private HealthBarScript ExtraHpBar;

    public UIPlayerCard PlayerCard;

    [HideInInspector] public float DistanceToFinish;
    [HideInInspector] public bool IsFinish { get; private set; }
    [HideInInspector] public bool IsAttacking { get; private set; }
    [HideInInspector] public bool IsStuned { get; private set; }
    [HideInInspector] public bool IsDead { get => Hp <= 0; }
    [HideInInspector] public bool IsExtraHealth { get => ExtraHpBar.fill.value > 0; }
    private bool isRunning;

    private Rigidbody player;
    private Animator anim;

    public const int MaxHealth = 1000;

    private int sceneIndex;

    private void Awake()
    {
        player = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        BuffHolder = GetComponent<BuffHolder>();

        LoadPlayerScore();
        LoadPlayerBuff();
    }

    private void Start()
    {
        Hp = MaxHealth;
        HpBar.setMaxHealth(Hp, MaxHealth);
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerColor = GetComponent<SpriteRenderer>().color;

        anim.SetBool("IsBossStage", false);
        anim.SetBool("IsRunningStage", false);

        if (sceneIndex == 2)
        {
            anim.SetBool("IsRunningStage", true);
        }
        else if (sceneIndex == 4)
        {
            anim.SetBool("IsBossStage", true);
            StartCoroutine(AttackingTrigger());
        }
    }

    private void Update()
    {
        if (GameManager.Instance.isPlay == true)
        {
            if (sceneIndex == 2)
            {
                RunningStage();
            }

            if (IsDead)
            {
                if (BuffHolder.GetBuffType() == BuffType.Life && BuffHolder.GetBuffValue() == 1f)
                {
                    BuffHolder.SetBuffValue(0);
                    Hp = MaxHealth;
                    HpBar.SetHealth(Hp);
                }
                else {
                    Position = 0;
                    playerIsFinish();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (sceneIndex == 4) { 
            UpdatePosition();
            PlayerCard.SetPositionText(Position.ToString());    
        }
    }

    void RunningStage() {
        if (!IsFinish)
        {
            DistanceToFinish = RunningStageManager.Instance.FinishLine.position.x - transform.position.x;
       
            PlayerCard.SetScoreText(StageScore.ToString());

            if (Input.GetKeyDown(tapKey))
            { 
                if (BuffHolder.GetBuffType() == BuffType.BuffSpeed)
                {
                    speed += .2f * BuffHolder.GetBuffValue();
                }
                else
                {
                    speed += .2f;
                }
            }
            else
            {
                if (speed > 0)
                {
                    if (BuffHolder.GetBuffType() == BuffType.BuffSpeed && BuffHolder.GetBuffValue() > 1f)
                        speed -= .023f;
                    else
                        speed -= .02f;
                    if (!isRunning) {
                        isRunning = true;
                        anim.SetBool("IsRunning", isRunning);
                    }
                }
                else {
                    speed = 0;
                    isRunning = false;
                    anim.SetBool("IsRunning", isRunning);
                }
            }
            speed = (float)Math.Round(speed, 2);
            Vector3 tempVect = new Vector3(1f, 0, 0);
            player.MovePosition(player.position + tempVect * speed * Time.fixedDeltaTime);
        }
    }

    void playerIsFinish()
    {
        IsFinish = true;
        GameManager.Instance.SavePlayerFinish(this);
        NotifyObserver(StageEvents.PlayerFinish);
        gameObject.SetActive(false);
    }

    public bool PlayerLastStand() {
        return Hp <= 100;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "Finish Line")
        {
            if (Position == 1)
            {
                DistanceToFinish = -40;
            }
            else if (Position == 2)
            {
               DistanceToFinish = -30;
            }
            else if (Position == 3)
            {
                DistanceToFinish = -20;
            }
            else if (Position == 4)
            {
                DistanceToFinish = -10;
            }
            playerIsFinish();
        }

    }

    IEnumerator AttackingTrigger() {
        while (!IsDead)
        {
            if (Input.GetKeyDown(tapKey))
            {
                IsAttacking = true;
                yield return new WaitForSeconds(0.5f);
                IsAttacking = false;
            }
            yield return null;
        }
    }

    public void OnStun(float time)
    {
        StartCoroutine(Stun(time));
    }

    private IEnumerator Stun(float time)
    {
        IsStuned = true;
        anim.SetBool("isStuned", IsStuned);
        anim.SetFloat("stunDuration", time);
        yield return new WaitForSeconds(time);
        IsStuned = false;
        anim.SetBool("isStuned", IsStuned);
    }

    public void ResetHp()
    {
        Hp = MaxHealth;
    }

    public void SetExtraHpBar(int value)
    {
        ExtraHpBar.SetExtraMaxHealth(value);
        ExtraHpBar.SetHealth(value);
    }

    public void TakeDamage(int damage) {
        if (IsExtraHealth)
        {
            SetExtraHpBar(damage);
        }
        else
        {
            Hp -= damage;
            HpBar.SetHealth(Hp);
        }
    }

    void UpdatePosition() {
        // Sort the dictionary by score in descending order
        var playerScores = GameManager.Instance.PolePositions;
        var sortedScores = playerScores.OrderByDescending(player => player.Value.PlayerScore).ToList();

        // Display rankings based on sorted scores
        for (int i = 0; i < sortedScores.Count; i++)
        {
            if (sortedScores[i].Key == playerNumber)
            {
                Position = i + 1;
            }
        }
    }

    public void AddScore(int score) {
        StageScore += score;
        GameManager.Instance.SavePlayerScore(this);
        PlayerCard.SetScoreText(StageScore.ToString());
        PlayerCard.SetPositionText(Position.ToString());
    }

    private void LoadPlayerScore() {
        StageScore = GameManager.Instance.PolePositions[playerNumber].PlayerScore;
        PlayerCard.SetScoreText(StageScore.ToString());

        Position = GameManager.Instance.PolePositions[playerNumber].PlayerPosition;
        PlayerCard.SetPositionText(Position.ToString());
    }

    private void LoadPlayerBuff()
    {
        BuffHolder.SetBuff(new Buff(GameManager.Instance.PolePositions[playerNumber].BuffType, GameManager.Instance.PolePositions[playerNumber].BuffValue));
        PlayerCard.SetEffectImage(BuffHolder.GetBuffType());
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            TakeDamage((int)BuffHolder.GetBuffValue());
        }
    }


    public void AddObserver(IStageObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IStageObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void NotifyObserver(StageEvents stageEvent)
    {
        _observers.ForEach((_observers) =>
        {
            _observers.OnNotify(stageEvent);
        });
    }

}
