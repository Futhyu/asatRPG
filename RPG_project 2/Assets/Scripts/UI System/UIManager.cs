using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;
    private PlayerController thePlayer;
    
    [Header("Character Info")]
    public Image info;

    public Transform canvas;
    //quest info
    [Header("Quest Info")]
    public Transform questInfo;
    public Text questName;
    public Text questDescription;
    public Button questInfoAcceptButton;
    public Button questInfoCancelButton;
    public Button questInfoCompleteButton;
    //quest grid
    [Header("Quest Book")]
    public Transform questBook;
    public Transform questBookContent;
    public Button questBookCancelButton;
    //interact text
    [Header("Interactive Text")]
    public Text displayText;
    private Transform castBar;
    public float valueCastBar;
    //ability book
    [Header("Ability Book")]
    public Transform abilityBook;
    public Transform abilityBookContent;
    public Button abilityBookCancelButton;
    [Header("Inventory")]
    private Transform inventory;

    void Awake() {

        thePlayer = FindObjectOfType<PlayerController>();
        canvas = FindObjectOfType<Canvas>().transform;
        if (!instance)
            instance = this;
        
        //quest info Cancel Button
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
        abilityBookCancelButton.onClick.AddListener(() => {
            AbilityManager.instance.ToogleAbilityBook(false);
        });

        inventory = canvas.Find("Inventory");

        castBar = canvas.Find("Cast Bar");
    }
    
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
    
    public static void ToogleScreenText(string text) {
        instance.displayText.text = text;
        instance.displayText.transform.parent.gameObject.SetActive(!instance.displayText.transform.parent.gameObject.activeSelf);
    }
       
    public Vector2 ScreenToCanvasPoint(Vector2 screenPosition) {
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(screenPosition);

        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        return (new Vector2(viewportPoint.x * canvasSize.x, viewportPoint.y * canvasSize.y) - (canvasSize / 2));
    }

    public Transform CastBar { get { return castBar; } }

    

}
