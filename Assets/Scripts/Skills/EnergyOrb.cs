using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : Skill
{
    public static EnergyOrb Instance;

    [SerializeField] private float BaseDamage = 8;
    [SerializeField] private GameObject Orb;
    [SerializeField] private GameObject Player;

    public float DamageMultiplier => _damageMultiplier[Level];
    public float OrbCount => _orbCount[Level];

    private float[] _damageMultiplier = {0, 1, 1.2f, 1.4f, 1.6f, 1.8f, 2f, 2f};
    private int[] _orbCount = {0, 1, 2, 3, 4, 5, 6, 8};

    
    private List<GameObject> _orbs = new List<GameObject>();

    void Start() {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
        MaxLevel = _damageMultiplier.Length - 1;
    }

    public override void LevelUp() {
        if (Level < MaxLevel) {
            Level++;
            CreateOrbs();
        }
    }

    private void ClearOrbs() {
        while (_orbs.Count > 0) {
            Destroy(_orbs[0]);
            _orbs.RemoveAt(0);
        }
    }
    
    private void CreateOrbs() {
        if (_orbs.Count == OrbCount) return;

        float baseAngle = _orbs.Count == 0 ? 0 : _orbs[0].GetComponent<OrbitalMovement>().Angle;
        var angleOffset = 2 * Mathf.PI / OrbCount;
        for (int i = 0; i < OrbCount; i++) {
            var angle = baseAngle + i * angleOffset;
            if (i < _orbs.Count) UpgradeOrb(_orbs[i], angle);
            else CreateOrb(angle);
        }
    }

    private void CreateOrb(float angle) {
        var orb = Instantiate(Orb, Player.transform.position, Quaternion.identity);
        var damageSource = orb.GetComponent<DamageSource>();
        var movement = orb.GetComponent<OrbitalMovement>();
        damageSource.Source = Player;
        damageSource.damage = (int)(BaseDamage * DamageMultiplier);
        movement.Center = Player;
        movement.Angle = angle;
        _orbs.Add(orb);

        orb.SetActive(true);
    }

    private void UpgradeOrb(GameObject orb, float angle) {
        var damageSource = orb.GetComponent<DamageSource>();
        var movement = orb.GetComponent<OrbitalMovement>();
        damageSource.damage = (int)(BaseDamage * DamageMultiplier);
        movement.Angle = angle;
    }

}
