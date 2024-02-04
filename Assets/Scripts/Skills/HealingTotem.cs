using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTotem : Skill
{
    public static HealingTotem Instance;

    [SerializeField] private float BaseCooldown = 6.0f;
    [SerializeField] private GameObject TotemPrefab;
    
    private float[] _rangeMultiplier = {1, 1, 1.3f, 1.5f, 1.8f, 2.1f, 2.4f, 3.0f};
    
    public float RangeMultiplier => _rangeMultiplier[Level];

    private GameObject _totem;
    private float _cooldown;

    void Start() {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
        MaxLevel = _rangeMultiplier.Length - 1;
        SpawnTotem();
    }

    void Update() {
        _cooldown -= Time.deltaTime;
        if (_cooldown < 0) {
            _cooldown = BaseCooldown;
            
            SpawnTotem();
        }
    }

    private void SpawnTotem() {
        if (_totem != null) Destroy(_totem);

        _totem = Instantiate(TotemPrefab, Player.Instance.transform.position, Quaternion.identity);

    }
}
