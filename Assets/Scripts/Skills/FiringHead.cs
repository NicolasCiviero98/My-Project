using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringHead : Skill
{
    public static FiringHead Instance;

    [SerializeField] private float BaseDamage = 8;
    [SerializeField] private GameObject Head;
    [SerializeField] private GameObject Player;

    public float DamageMultiplier => _damageMultiplier[Level];
    public float HeadCount => _headCount[Level];

    private float[] _damageMultiplier = {0, 1, 1.2f, 1.4f, 1.6f, 1.8f, 2f, 2f};
    private int[] _headCount = {0, 1, 2, 3, 4, 5, 6, 8};

    private float angleGap = 0.524f;
    
    private List<GameObject> _heads = new List<GameObject>();

    void Start() {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
        MaxLevel = _damageMultiplier.Length - 1;
        CreateHeads();
    }

    public override void LevelUp() {
        if (Level < MaxLevel) {
            Level++;
            CreateHeads();
        }
    }

    private void CreateHeads() {
        if (_heads.Count == HeadCount) return;
        var offset = - (HeadCount - 1) / 2 * angleGap;
        for (int i = 0; i < HeadCount; i++) {
            var angle = offset + angleGap * i;
            if (i < _heads.Count) UpgradeHead(_heads[i], angle);
            else CreateHead(angle);
        }
    }

    private void CreateHead(float angle) {
        var head = Instantiate(Head, Player.transform.position, Quaternion.identity);
        var minionSumonner = head.GetComponent<MinionSummoner>();
        minionSumonner.Damage = (int)(BaseDamage * DamageMultiplier);
        minionSumonner.Player = Player;
        minionSumonner.Angle = angle;
        _heads.Add(head);

        head.SetActive(true);
    }

    private void UpgradeHead(GameObject head, float angle) {
        var minionSumonner = head.GetComponent<MinionSummoner>();
        minionSumonner.Damage = (int)(BaseDamage * DamageMultiplier);
        minionSumonner.Angle = angle;
        minionSumonner.Player = Player;
    }
}
