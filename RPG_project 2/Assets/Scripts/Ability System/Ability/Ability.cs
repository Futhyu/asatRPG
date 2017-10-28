using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject {

    #region Ability
    [Header("Ability")]
    public string abilityName;
    public Sprite icon;
    public GameObject spellPrefab;
    public int manaCost;
    public Buff buff;
    public float damage;
    public float projectileSpeed;
    public float cooldown;
    public float castTime;
    public AbilityType type;
    #endregion
    #region Range
    [Header("Range")]
    public float minDistance;
    public float maxDistance;
    public bool isRandom;
    public float lifeDistance { get; private set; }
    public float stunTime;
    #endregion
    #region AOE
    [Header("AOE")]
    public float areaRadius;
    public float effectDuration;
    public bool isOccupied;
    #endregion
    //public bool requiresTarget;
    //public bool canCastOnSelf;
    [Header("Other")]
    public bool isCooldown;

    public enum AbilityType { Range, AOE }
    
    public void SetRange() {
        lifeDistance = isRandom ? Random.Range(minDistance, maxDistance) : maxDistance;
    }

    public void SetRadius(CircleCollider2D coll) {
        coll.radius = areaRadius;
        coll.isTrigger = true;
    }

    //public void Cooldown(Ability ability) {

    //    abilityCooldownTimer = new Stopwatch();
    //    abilityCooldownTimer.Start();
    //    if (abilityCooldownTimer.Elapsed.TotalSeconds < cooldown) {

    //        UnityEngine.Debug.Log("Cooldown");
    //        //return;
    //    }
    //    abilityCooldownTimer.Stop();
    //    abilityCooldownTimer.Reset();
    //}

    //public void UseAbility(GameObject player) {
    //    spellPrefab = GameObject.Find("Range");
    //    behaviour.PerformBehaviour(player, spellPrefab);

    //}
}
