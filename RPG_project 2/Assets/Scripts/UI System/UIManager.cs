using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;
    private PlayerController thePlayer;
    
    public delegate void OnCast(float value);
    public OnCast onCast;

    [Header("Character Info")]
    private Image info;

    public Transform canvas;
    //quest info
    
    [Header("Quest Info")]
    public Transform questInfo;
    public Transform questInfoContent;
    public Button questInfoAcceptButton;
    public Button questInfoCancelButton;
    public Button questInfoCompleteButton;
    //quest grid
    [Header("Quest Book")]
    public Transform questBook;
    public Transform questBookContent;
    private Button questBookCancelButton;
    //interact text
    [Header("Interactive Text")]
    public Transform screenText;
    private Transform castBar;
    public float valueCastBar;
    //ability book
    [Header("Ability Book")]
    public Transform abilityBook;
    public Transform abilityBookContent;
    private Transform abilityBookCancelButton;
    [Header("Inventory")]
    private Transform inventory;

    void Awake() {

        thePlayer = FindObjectOfType<PlayerController>();
        canvas = FindObjectOfType<Canvas>().transform;
        if (!instance)
            instance = this;
        info = canvas.transform.Find("Character Info").GetComponent<Image>();
        
        questInfo = canvas.Find("Quest Info");
        questInfoContent = questInfo.Find("Background/Info/Viewport/Content");
        questInfoAcceptButton = questInfo.Find("Background/Buttons/Accept").GetComponent<Button>();
        questInfoCompleteButton = questInfo.Find("Background/Buttons/Complete").GetComponent<Button>();

        screenText = canvas.Find("Screen Text");

        questBook = canvas.Find("Quest Book");
        questBookContent = canvas.Find("Quest Book/Background/Info/Viewport/Content");

        //quest info Cancel Button
        questInfoCancelButton = questInfo.Find("Background/Buttons/Cancel").GetComponent<Button>();
        questInfoCancelButton.onClick.AddListener(() => {
            questInfo.gameObject.SetActive(false);
            thePlayer.canMove = true;
        });
        //quest book cancel button
        questBookCancelButton = canvas.Find("Quest Book/Background/Buttons/Cancel").GetComponent<Button>();
        questBookCancelButton.onClick.AddListener(() => {
            questBook.gameObject.SetActive(false);
        });
        //ability book
        abilityBook = canvas.Find("Ability Book");
        abilityBookContent = abilityBook.Find("Background/Info/Content");
        abilityBookCancelButton = abilityBook.Find("Background/Buttons/Cancel");
        abilityBookCancelButton.GetComponent<Button>().onClick.AddListener(() => {
            AbilityManager.instance.ToogleAbilityBook(false);
        });

        inventory = canvas.Find("Inventory");

        castBar = canvas.Find("Cast Bar");
    }

    

    //IEnumerator ChangeValue() {
    //    if (onCast != null) {
    //        onCast(valueCastBar);
    //    }
    //    yield return null;
    //}

    void Update() {
        if (thePlayer != null) {
            info.transform.Find("Lvl").GetComponent<Text>().text = "Lvl: " + thePlayer.GetComponent<PlayerStats>().Level;

            info.transform.Find("HP bar").Find("HP").GetComponent<Text>().text = "HP: " + Mathf.Round(thePlayer.GetComponent<PlayerStats>().currentHealth) + "/" + Mathf.Round(thePlayer.GetComponent<PlayerStats>().maxHealth);

            info.transform.Find("HP bar").GetComponent<Slider>().maxValue = thePlayer.GetComponent<PlayerStats>().maxHealth;

            info.transform.Find("HP bar").GetComponent<Slider>().value = thePlayer.GetComponent<PlayerStats>().currentHealth;

            info.transform.Find("MP bar").Find("MP").GetComponent<Text>().text = "MP: " + Mathf.Round(thePlayer.GetComponent<PlayerStats>().currentMana) + "/" + thePlayer.GetComponent<PlayerStats>().maxMana;

            info.transform.Find("MP bar").GetComponent<Slider>().maxValue = thePlayer.GetComponent<PlayerStats>().maxMana;

            info.transform.Find("MP bar").GetComponent<Slider>().value = thePlayer.GetComponent<PlayerStats>().currentMana;

            castBar.GetComponentInChildren<Slider>().value = valueCastBar;
        }
    }
    
    public void ToogleInventory() {
        inventory.gameObject.SetActive(!inventory.gameObject.activeInHierarchy);
    }

    public Vector2 ScreenToCanvasPoint(Vector2 screenPosition) {
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(screenPosition);

        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        return (new Vector2(viewportPoint.x * canvasSize.x, viewportPoint.y * canvasSize.y) - (canvasSize / 2));
    }

    public Transform CastBar { get { return castBar; } }

}
