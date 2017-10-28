using UnityEngine;
using System.Collections;

public class SpellConfigurator : MonoBehaviour {

    public Ability ability;
    
    void Start() {
        if(ability != null) {
            if(ability.type == Ability.AbilityType.Range) {
                ability.SetRange();
                Sprite sprite = GetComponent<SpriteRenderer>().sprite;
                BoxCollider2D coll = GetComponent<BoxCollider2D>();
                coll.bounds.SetMinMax(sprite.bounds.min, sprite.bounds.max);
                
            }
            else if(ability.type == Ability.AbilityType.AOE) {
                CircleCollider2D aoeColl = GetComponent<CircleCollider2D>();
                Sprite sprite = GetComponent<SpriteRenderer>().sprite;
                aoeColl.radius = sprite.bounds.max.x;
                //ability.SetRadius(aoeColl);
                //Debug.Log(aoeColl.bounds.size);
            }
        }
    }

    void Update() {
        if(ability != null) {
            if(ability.type == Ability.AbilityType.Range) {
                float tempDistance = Vector2.Distance(transform.parent.position, gameObject.transform.position);
                
                if (tempDistance < ability.lifeDistance) {
                    tempDistance = Vector2.Distance(transform.parent.position, ability.spellPrefab.transform.position);
                    //Debug.Log("Distance: " + tempDistance);
                }
                else {
                    Debug.Log("Destroy");
                    Destroy(gameObject);
                    
                }
            }
            else if(ability.type == Ability.AbilityType.AOE) {
                StartCoroutine(AOE());
            }
        }
    
    }

    private IEnumerator AOE() {
        yield return new WaitForSeconds(ability.effectDuration);
        Destroy(gameObject);
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy") && !other.isTrigger && transform.GetComponentInParent<PlayerController>()) {
            if (ability != null) {
                EnemyStats target = other.GetComponent<EnemyStats>();
                target.TakeDamage(ability.damage);
                target.AddBuff(ability.buff);
                if (ability.type == Ability.AbilityType.Range) Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Player") && !other.isTrigger && transform.GetComponentInParent<EnemyController>()) {
            if(ability != null) {
                PlayerStats target = other.GetComponent<PlayerStats>();
                target.TakeDamage(ability.damage);
                target.AddBuff(ability.buff);
                if (ability.type == Ability.AbilityType.Range) Destroy(gameObject);
            }
        }
    }

}
