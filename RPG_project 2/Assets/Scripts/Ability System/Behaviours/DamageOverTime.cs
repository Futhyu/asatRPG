using UnityEngine;
using System.Collections;
using System.Diagnostics;

[RequireComponent(typeof(BoxCollider2D))]
public class DamageOverTime : AbilityBehaviour {

    private const string abName = "Damage Over Time";
    private const string abDescription = "A dot!";
    private const BehaviourStartTimes startTime = BehaviourStartTimes.Beginning;   //on impact

    public float effectDuration;
    public float baseEffectDamage;
    public float damagePerLoop;

    private float amountDamaged = 0;
    private Stopwatch timer;
    private CharacterStats target;
    
    void Start () {
        GetComponent<BoxCollider2D>().isTrigger = true;
        timer = new Stopwatch();
        damagePerLoop = baseEffectDamage / effectDuration;

    }

    void Update() {
        if (target != null) {
            GetComponent<BoxCollider2D>().size = target.GetComponent<Collider2D>().bounds.size;
            transform.position = target.gameObject.transform.position;

            if (timer.Elapsed.TotalSeconds >= 1) {
                timer.Reset();

            }
            if (timer.Elapsed.TotalSeconds == 0) {
                timer.Start();
                StartCoroutine(DOT());
                if (amountDamaged <= baseEffectDamage) {
                    target.TakeDamage(damagePerLoop);
                    amountDamaged += damagePerLoop;
                }
            }
            else StartCoroutine(DOT());
        }
        
    }
    public override void PerformBehaviour(GameObject player, GameObject prefab) {
        StartCoroutine(DOT());
    }
    
    private IEnumerator DOT() {
        //currentTime += Time.deltaTime;
        yield return new WaitForSeconds(effectDuration);
        UnityEngine.Debug.Log("Destroy");
        Destroy(gameObject);
        yield return null;
    }
    
    public float EffectDuration { get { return effectDuration; } set { effectDuration = value; } }
    public float BaseEffectDamage { get { return baseEffectDamage; } set { baseEffectDamage = value; } }
    internal CharacterStats Target { set { target = value; } }
}
