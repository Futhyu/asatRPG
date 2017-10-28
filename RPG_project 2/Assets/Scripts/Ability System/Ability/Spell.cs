using UnityEngine;
using System.Collections;
using System.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler {

    public Sprite icon;

    public Ability abilitySpell;
    
    private bool isCast;
    public bool isCooldown;
    private Stopwatch timer;
    private Image fillImage;
    private GameObject ability;
    private Vector2 offset;
    public int slot;
    private HotbarController hbController;
    //public bool requiresTarget;
    //public bool canCastOnSelf;

    private float time = 0;

    void Start() {
        ability = gameObject;
        hbController = FindObjectOfType<HotbarController>();
        fillImage = transform.GetChild(0).GetComponent<Image>();
        timer = new Stopwatch();


        //AbilityManager.instance.onAbilityCast += OnAbilityCast;
    }
    
    void Update() {
        if (isCast) UIManager.instance.valueCastBar = (float)timer.Elapsed.TotalSeconds / abilitySpell.castTime;

        if (isCooldown) Cooldown();

    }
    void Cooldown() {
        StartCoroutine(SpinImage(fillImage));
    }
    
    public void CastSpell() {
        if (PlayerStats.instance.currentMana >= abilitySpell.manaCost && !AbilityManager.instance.IsCast && !isCooldown) {
            
            StartCoroutine(Cast());
        }
    }

    private IEnumerator Cast() {
        timer.Start();
        isCast = true;
        AbilityManager.instance.IsCast = true;
        UIManager.instance.CastBar.gameObject.SetActive(!UIManager.instance.CastBar.gameObject.activeInHierarchy);
        
        PlayerController.main.canMove = false;
        yield return new WaitForSeconds(abilitySpell.castTime);
        UIManager.instance.CastBar.gameObject.SetActive(!UIManager.instance.CastBar.gameObject.activeInHierarchy);
        
        isCast = false;
        timer.Stop();
        timer.Reset();
        AbilityManager.instance.IsCast = false;
        AbilityManager.instance.CastAbility(this, PlayerController.main.gameObject.transform);
        isCooldown = true;
        PlayerController.main.canMove = true;   // need to rebuild it
    }

    private float OnCast(float value) {
        value = (float)timer.Elapsed.TotalSeconds / abilitySpell.castTime;
        return value;
    }

    //private IEnumerator Cool(Ability ability) {
    //    yield return new WaitForSeconds(ability.cooldown);
    //}

    private IEnumerator SpinImage(Image fillImage) {
        
        isCooldown = true;
        if (time < abilitySpell.cooldown) {
            time += Time.deltaTime;
            fillImage.fillAmount = time / abilitySpell.cooldown;
        }
        else {
            fillImage.fillAmount = 0;
            time = 0;
            isCooldown = false;
        }
        yield return null;

    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (!Input.GetKey(KeyCode.LeftShift)) {
            return;
            //zachem tak delat', ha-ze
        }
        else {
            if (ability != null) {
                this.transform.SetParent(this.transform.parent.parent.parent.parent);
                this.transform.position = eventData.position;

                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (!Input.GetKey(KeyCode.LeftShift)){

        } else {
            if (ability != null) {
                this.transform.position = eventData.position - offset;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData) {


        //Debug.Log(true);
        UnityEngine.Debug.Log(eventData.pointerDrag.gameObject.name);
        this.transform.SetParent(hbController.slots[slot].transform);
        this.transform.position = hbController.slots[slot].transform.position;

        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

    public void OnPointerDown(PointerEventData eventData) {

        offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
        
    }

    public bool IsCast { get { return isCast; } set { isCast = value; } }
}
