using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public bool canMoveToNextMenu;
    public string currentMenu = "";
    public GameObject eventSystem;
    public GameObject blackOut;


    [Header("Cameras")]
    public GameObject titleCamera;
    public GameObject mainMenuCamera;

    [Header("Title Menu References")]
    public GameObject mainLogo;
    public GameObject startText;

    [Header("Main Menu References")]
    public GameObject mainMenu;
    public GameObject fightButton;

    [Header("Fight Menu")]
    public GameObject fightMenu;
    GameObject currentSelected;
    public bool b1Side;
    public bool b2Side;
    public GameObject[] b1MenuSide;
    public GameObject[] b2MenuSide;
    //GAME MODE
    public GameObject modeDropDown;
    //FIGHT STYLE
    public string[] fightStyles = { "" };
    bool canSwtichFightStyle;
    public TextMeshProUGUI b1FightStyleText;
    public TextMeshProUGUI b2FightStyleText;
    public int b1FightStyle = 0;
    public int b2FightStyle = 0;
    //OUTFIT SELECTION
    bool canSwitchOutfitSelection;
    public TextMeshProUGUI b1OutfitSelectionText;
    public TextMeshProUGUI b2OutfitSelectionText;
    public int b1OutfitSelection;
    public int b2OutfitSelection;
    //OUTFIT VARIATION
    bool canSwitchOutfitVariation;
    public TextMeshProUGUI b1OutfitVariationText;
    public TextMeshProUGUI b2OutfitVariationText;
    public int b1OutfitVariation;
    public int b2OutfitVariation;
    //HAIR TYPE
    bool canSwitchHairType;
    public TextMeshProUGUI b1HairTypeText;
    public TextMeshProUGUI b2HairTypeText;
    public int b1HairType;
    public int b2HairType;
    //HAIR COLOR
    bool canSwitchHairColor;
    public Image b1HairColorIndicator;
    public Image b2HairColorIndicator;
    public int b1HairColor;
    public int b2HairColor;
    //SKIN COLOR
    bool canSwitchSkinColor;
    public Image b1SkinColorIndicator;
    public Image b2SkinColorIndicator;
    public int b1SkinColor;
    public int b2SkinColor;

    private void Awake()
    {
        mainLogo.GetComponent<CanvasGroup>().alpha = 0;
        startText.GetComponent<CanvasGroup>().alpha = 0;
        currentMenu = "Title Menu";
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MainLogoTime(2));

    }

    private void Update()
    {
        currentSelected = eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject;

        bool nextMenu = Input.GetButton("Pick Up");
        bool switchB1Side = Input.GetButtonDown("Secondary Special");
        bool switchB2Side = Input.GetButtonDown("Primary Special");
        

        if (canMoveToNextMenu && nextMenu)
        {
            if (currentMenu == "Title Menu")
            {
                mainLogo.GetComponent<Fade>().fadeOut = true;
                startText.GetComponent<Fade>().fadeOut = true;
                StartCoroutine(BlackOut(1, 3));
                StartCoroutine(SwitchCameras(2, titleCamera, mainMenuCamera));
                currentMenu = "Main Menu";
            }

            canMoveToNextMenu = false;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");

        if (currentMenu == "Fight Menu")
        {
            if(currentSelected.name == "Mode Dropdown" || currentSelected.name == "Difficulty Dropdown")
            {
                b1Side = true;
                b2Side = false;
            }

            if (switchB1Side)
            {
                if (currentSelected.name == "B2 FightStyleSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1MenuSide[0]);
                }
                else if (currentSelected.name == "B2 OutfitSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1MenuSide[1]);
                }
                else if (currentSelected.name == "B2 OutfitVariation")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1MenuSide[2]);
                }
                else if (currentSelected.name == "B2 HairTypeSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1MenuSide[3]);
                }
                else if (currentSelected.name == "B2 HairColorSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1MenuSide[4]);
                }
                else if (currentSelected.name == "B2 SkinColorSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b1MenuSide[5]);
                }

                b1Side = true;
                b2Side = false;
            } else if (switchB2Side)
            {
                if (currentSelected.name == "B1 FightStyleSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b2MenuSide[0]);
                } else if(currentSelected.name == "B1 OutfitSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b2MenuSide[1]);
                }
                else if (currentSelected.name == "B1 OutfitVariation")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b2MenuSide[2]);
                }
                else if (currentSelected.name == "B1 HairTypeSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b2MenuSide[3]);
                }
                else if (currentSelected.name == "B1 HairColorSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b2MenuSide[4]);
                }
                else if (currentSelected.name == "B1 SkinColorSelection")
                {
                    eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(b2MenuSide[5]);
                }

                b2Side = true;
                b1Side = false;
            }

            if (canSwtichFightStyle && (currentSelected.name == "B1 FightStyleSelection" || currentSelected.name == "B2 FightStyleSelection"))
            {
                if (horizontal > .75f)
                {
                    StartCoroutine(FightStyle("right"));
                }
                else if (horizontal < -.75f)
                {
                    StartCoroutine(FightStyle("left"));
                }
            }

            if (canSwitchOutfitSelection && (currentSelected.name == "B1 OutfitSelection" || currentSelected.name == "B2 OutfitSelection"))
            {
                if (horizontal > .75f)
                {
                    StartCoroutine(OutfitSelection("right"));
                }
                else if (horizontal < -.75f)
                {
                    StartCoroutine(OutfitSelection("left"));
                }
            }

            if (canSwitchOutfitVariation && (currentSelected.name == "B1 OutfitVariation" || currentSelected.name == "B2 OutfitVariation"))
            {
                if (horizontal > .75f)
                {
                    StartCoroutine(OutfitVariation("right"));
                }
                else if (horizontal < -.75f)
                {
                    StartCoroutine(OutfitVariation("left"));
                }
            }

            if (canSwitchHairType && (currentSelected.name == "B1 HairTypeSelection" || currentSelected.name == "B2 HairTypeSelection"))
            {
                if (horizontal > .75f)
                {
                    StartCoroutine(HairType("right"));
                }
                else if (horizontal < -.75f)
                {
                    StartCoroutine(HairType("left"));
                }
            }

            if (canSwitchHairColor && (currentSelected.name == "B1 HairColorSelection" || currentSelected.name == "B2 HairColorSelection"))
            {
                if (horizontal > .75f)
                {
                    StartCoroutine(HairColor("right"));
                }
                else if (horizontal < -.75f)
                {
                    StartCoroutine(HairColor("left"));
                }
            }

            if (canSwitchSkinColor && (currentSelected.name == "B1 SkinColorSelection" || currentSelected.name == "B2 SkinColorSelection"))
            {
                if (horizontal > .75f)
                {
                    StartCoroutine(SkinColor("right"));
                }
                else if (horizontal < -.75f)
                {
                    StartCoroutine(SkinColor("left"));
                }
            }

            b1FightStyleText.text = fightStyles[b1FightStyle];
            b2FightStyleText.text = fightStyles[b2FightStyle];

            b1OutfitSelectionText.text = "" + b1OutfitSelection;
            b2OutfitSelectionText.text = "" + b2OutfitSelection;

            b1OutfitVariationText.text = "" + b1OutfitVariation;
            b2OutfitVariationText.text = "" + b2OutfitVariation;

            b1HairTypeText.text = "" + b1HairType;
            b2HairTypeText.text = "" + b2HairType;

            b1HairColorIndicator.GetComponent<Image>().color = fightMenu.GetComponent<BrawlerUpdates>().hairColors[b1HairColor - 1].color;
            b2HairColorIndicator.GetComponent<Image>().color = fightMenu.GetComponent<BrawlerUpdates>().hairColors[b2HairColor - 1].color;

            b1SkinColorIndicator.GetComponent<Image>().color = fightMenu.GetComponent<BrawlerUpdates>().skinColors[b1SkinColor - 1].color;
            b2SkinColorIndicator.GetComponent<Image>().color = fightMenu.GetComponent<BrawlerUpdates>().skinColors[b2SkinColor - 1].color;
        }
    }

    IEnumerator MainLogoTime(float t)
    {
        yield return new WaitForSeconds(t);
        mainLogo.GetComponent<Fade>().fadeIn = true;
        yield return new WaitForSeconds(t);
        startText.GetComponent<Fade>().fadeIn = true;
        yield return new WaitForSeconds(1.5f);
        canMoveToNextMenu = true;

        fightMenu.GetComponent<BrawlerUpdates>().brawler1.transform.position += new Vector3(0, 0, -10);
        fightMenu.GetComponent<BrawlerUpdates>().brawler2.transform.position += new Vector3(0, 0, -10);
    }

    IEnumerator BlackOut(float waitTime, float blackOutTime)
    {
        yield return new WaitForSeconds(waitTime);
        blackOut.GetComponent<Fade>().fadeIn = true;
        yield return new WaitForSeconds(blackOutTime);
        blackOut.GetComponent<Fade>().fadeOut = true;
        MainMenu();
        //canMoveToNextMenu = true;
    }

    IEnumerator SwitchCameras(float waitTime, GameObject currentCamera, GameObject newCamera)
    {
        yield return new WaitForSeconds(waitTime);
        newCamera.SetActive(true);
        currentCamera.SetActive(false);
        canSwtichFightStyle = true;
        canSwitchOutfitSelection = true;
        canSwitchOutfitVariation = true;
        canSwitchHairType = true;
        canSwitchHairColor = true;
        canSwitchSkinColor = true;
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = fightButton;
    }

    //FIGHT MENU

    public void FightMenu() //Used when Fight button is pressed
    {
        StartCoroutine(FightMenuCoroutine());
    }

    public IEnumerator FightMenuCoroutine() //
    {
        currentMenu = "Fight Menu";
        
        mainMenuCamera.GetComponent<SmoothDampCamera>().smoothDamp = true;
        mainMenu.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        fightMenu.SetActive(true);
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(modeDropDown);
        b1Side = true;
        b2Side = false;
        fightMenu.GetComponent<BrawlerUpdates>().brawler1.transform.position += new Vector3(0, 0, 10);
        fightMenu.GetComponent<BrawlerUpdates>().brawler2.transform.position += new Vector3(0, 0, 10);
    }

    IEnumerator FightStyle(string dir)
    {
        canSwtichFightStyle = false;

        if (b1Side)
        {
            if(dir == "right")
            {
                if (b1FightStyle == fightStyles.Length - 1)
                {
                    b1FightStyle = 0;
                }
                else
                {
                    b1FightStyle++;
                }
            }
            else
            {
                if (b1FightStyle == 0)
                {
                    b1FightStyle = fightStyles.Length - 1;
                }
                else
                {
                    b1FightStyle--;
                }
            }
        }
        else
        {
            if (dir == "right")
            {
                if (b2FightStyle == fightStyles.Length - 1)
                {
                    b2FightStyle = 0;
                }
                else
                {
                    b2FightStyle++;
                }
            }
            else
            {
                if (b2FightStyle == 0)
                {
                    b2FightStyle = fightStyles.Length - 1;
                }
                else
                {
                    b2FightStyle--;
                }
            }
        }

        yield return new WaitForSeconds(.5f);
        canSwtichFightStyle = true;
    }

    IEnumerator OutfitSelection(string dir)
    {
        canSwitchOutfitSelection = false;

        if (b1Side)
        {
            if (dir == "right")
            {
                if (b1OutfitSelection == 2)
                {
                    b1OutfitSelection = 1;
                }
                else
                {
                    b1OutfitSelection++;
                }
            }
            else
            {
                if (b1OutfitSelection == 1)
                {
                    b1OutfitSelection = 2;
                }
                else
                {
                    b1OutfitSelection--;
                }
            }
        }
        else
        {
            if (dir == "right")
            {
                if (b2OutfitSelection == 2)
                {
                    b2OutfitSelection = 1;
                }
                else
                {
                    b2OutfitSelection++;
                }
            }
            else
            {
                if (b2OutfitSelection == 1)
                {
                    b2OutfitSelection = 2;
                }
                else
                {
                    b2OutfitSelection--;
                }
            }
        }

        yield return new WaitForSeconds(.5f);
        canSwitchOutfitSelection = true;
    }

    IEnumerator OutfitVariation(string dir)
    {
        canSwitchOutfitVariation = false;

        if (b1Side)
        {
            if (dir == "right")
            {
                if (b1OutfitVariation == 5)
                {
                    b1OutfitVariation = 1;
                }
                else
                {
                    b1OutfitVariation++;
                }
            }
            else
            {
                if (b1OutfitVariation == 1)
                {
                    b1OutfitVariation = 5;
                }
                else
                {
                    b1OutfitVariation--;
                }
            }
        }
        else
        {
            if (dir == "right")
            {
                if (b2OutfitVariation == 5)
                {
                    b2OutfitVariation = 1;
                }
                else
                {
                    b2OutfitVariation++;
                }
            }
            else
            {
                if (b2OutfitVariation == 1)
                {
                    b2OutfitVariation = 5;
                }
                else
                {
                    b2OutfitVariation--;
                }
            }
        }

        yield return new WaitForSeconds(.5f);
        canSwitchOutfitVariation = true;
    }

    IEnumerator HairType(string dir)
    {
        canSwitchHairType = false;

        if (b1Side)
        {
            if (dir == "right")
            {
                if (b1HairType == 10)
                {
                    b1HairType = 1;
                }
                else
                {
                    b1HairType++;
                }
            }
            else
            {
                if (b1HairType == 1)
                {
                    b1HairType = 10;
                }
                else
                {
                    b1HairType--;
                }
            }
        }
        else
        {
            if (dir == "right")
            {
                if (b2HairType == 10)
                {
                    b2HairType = 1;
                }
                else
                {
                    b2HairType++;
                }
            }
            else
            {
                if (b2HairType == 1)
                {
                    b2HairType = 10;
                }
                else
                {
                    b2HairType--;
                }
            }
        }

        yield return new WaitForSeconds(.5f);
        canSwitchHairType = true;
    }

    IEnumerator HairColor(string dir)
    {
        canSwitchHairColor = false;

        if (b1Side)
        {
            if (dir == "right")
            {
                if (b1HairColor == 9)
                {
                    b1HairColor = 1;
                }
                else
                {
                    b1HairColor++;
                }
            }
            else
            {
                if (b1HairColor == 1)
                {
                    b1HairColor = 9;
                }
                else
                {
                    b1HairColor--;
                }
            }
        }
        else
        {
            if (dir == "right")
            {
                if (b2HairColor == 9)
                {
                    b2HairColor = 1;
                }
                else
                {
                    b2HairColor++;
                }
            }
            else
            {
                if (b2HairColor == 1)
                {
                    b2HairColor = 9;
                }
                else
                {
                    b2HairColor--;
                }
            }
        }

        yield return new WaitForSeconds(.5f);
        canSwitchHairColor = true;
    }

    IEnumerator SkinColor(string dir)
    {
        canSwitchSkinColor = false;

        if (b1Side)
        {
            if (dir == "right")
            {
                if (b1SkinColor == 11)
                {
                    b1SkinColor = 1;
                }
                else
                {
                    b1SkinColor++;
                }
            }
            else
            {
                if (b1SkinColor == 1)
                {
                    b1SkinColor = 11;
                }
                else
                {
                    b1SkinColor--;
                }
            }
        }
        else
        {
            if (dir == "right")
            {
                if (b2SkinColor == 11)
                {
                    b2SkinColor = 1;
                }
                else
                {
                    b2SkinColor++;
                }
            }
            else
            {
                if (b2SkinColor == 1)
                {
                    b2SkinColor = 11;
                }
                else
                {
                    b2SkinColor--;
                }
            }
        }

        yield return new WaitForSeconds(.5f);
        canSwitchSkinColor = true;
    }
}
