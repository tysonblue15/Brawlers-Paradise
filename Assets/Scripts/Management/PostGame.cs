using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PostGame : MonoBehaviour
{
    [Header("Post-Game")]
    public GameObject eventSystem;
    ResultStats resultStatsInstance;
    public GameObject blackOut;
    public TextMeshProUGUI winnerText;
    public GameObject postGameMenu;
    public GameObject playAgainButton;
    public GameObject[] postGameMenuParts = new GameObject[5];

    [Header("Match Stats Menu")]
    bool b1Side = true;
    bool b2Side = false;
    public GameObject matchStatsMenu;
    public TextMeshProUGUI brawlerName;
    public TextMeshProUGUI matchTime;
    public TextMeshProUGUI totalAttacks;
    public TextMeshProUGUI attacksLanded;
    public TextMeshProUGUI attackRate;
    public TextMeshProUGUI damageInflicted;
    public TextMeshProUGUI healthRecovered;
    public TextMeshProUGUI staminaUsed;
    public TextMeshProUGUI dodgesPerformed;
    public TextMeshProUGUI throwablesUsed;
    public TextMeshProUGUI techniqueUsed;
    public TextMeshProUGUI souvenirUsed;


    public enum Scene
    {
        MainMenu,
        Dojo
    }

    private void Awake()
    {
        winnerText.GetComponent<CanvasGroup>().alpha = 0;
        resultStatsInstance = GetComponent<ResultStats>();
    }

    private void Update()
    {
        bool switchB1Side = Input.GetButtonDown("Secondary Special");
        bool switchB2Side = Input.GetButtonDown("Primary Special");
        bool bButton = Input.GetButtonDown("Dodge");

        if (switchB1Side && matchStatsMenu.activeSelf)
        {
            b1Side = true;
            b2Side = false;
            MatchStats();
        }

        if (switchB2Side && matchStatsMenu.activeSelf)
        {
            b1Side = false;
            b2Side = true;
            MatchStats();
        }

        if(bButton && matchStatsMenu.activeSelf)
        {
            b1Side = true;
            b2Side = false;

            for (int i = 0; i < postGameMenuParts.Length; i++)
            {
                postGameMenuParts[i].SetActive(true);
            }

            matchStatsMenu.SetActive(false);
            eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(playAgainButton);
        }
    }

    public IEnumerator ShowboatCompleteCoroutine()
    {
        resultStatsInstance.b1BasicAttackPercentage = (int)((resultStatsInstance.b1BasicAttacksLanded / resultStatsInstance.b1TotalBasicAttacks) * 100);
        resultStatsInstance.b2BasicAttackPercentage = (int)((resultStatsInstance.b2BasicAttacksLanded / resultStatsInstance.b2TotalBasicAttacks) * 100);

        yield return new WaitForSeconds(.5f);

        if (UniversalFight.usingMenuData)
        {
            if (GetComponent<IdManagear>().brawler1.GetComponent<Health>().health <= 0)
            {
                winnerText.text = StateNameController.b2Name + "\nWINS";
            }
            else
            {
                winnerText.text = StateNameController.b1Name + "\nWINS";
            }
        }
        else
        {
            winnerText.text = "BRAWLER \nWINS";
        }

        winnerText.GetComponent<Fade>().fadeIn = true;

        yield return new WaitForSeconds(3);

        winnerText.GetComponent<Fade>().fadeOut = true;

        yield return new WaitForSeconds(2);

        postGameMenu.SetActive(true);
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(playAgainButton);
    }

    public void PlayAgain()
    {
        blackOut.GetComponent<Fade>().fadeIn = true;
        StartCoroutine(PlayAgainCoroutine());
    }

    IEnumerator PlayAgainCoroutine()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(Scene.Dojo.ToString());
    }

    public void MatchStats()
    {
        for(int i = 0; i < postGameMenuParts.Length; i++)
        {
            postGameMenuParts[i].SetActive(false);
        }

        matchStatsMenu.SetActive(true);
        matchTime.text = "Work In Progress";
        if (b1Side)
        {
            if(UniversalFight.usingMenuData)
                brawlerName.text = "" + StateNameController.b1Name;

            totalAttacks.text = "" + resultStatsInstance.b1TotalBasicAttacks;
            attacksLanded.text = "" + resultStatsInstance.b1BasicAttacksLanded;
            attackRate.text = "" + resultStatsInstance.b1BasicAttackPercentage + "%";
            damageInflicted.text = "" + resultStatsInstance.b1DamageInflicted;
            healthRecovered.text = "" + resultStatsInstance.b1HealthRecoverd;
            staminaUsed.text = "" + resultStatsInstance.b1StaminaUsed;
            dodgesPerformed.text = "" + resultStatsInstance.b1DodgesPerformed;
            throwablesUsed.text = "" + resultStatsInstance.b1ThrowablesUsed;
            techniqueUsed.text = "" + resultStatsInstance.b1TechniqueUsage;
            souvenirUsed.text = "" + resultStatsInstance.b1SouvenirUsage;
        } 
        else if(b2Side)
        {
            if (UniversalFight.usingMenuData)
                brawlerName.text = StateNameController.b2Name;

            totalAttacks.text = "" + resultStatsInstance.b2TotalBasicAttacks;
            attacksLanded.text = "" + resultStatsInstance.b2BasicAttacksLanded;
            attackRate.text = "" + resultStatsInstance.b2BasicAttackPercentage + "%";
            damageInflicted.text = "" + resultStatsInstance.b2DamageInflicted;
            healthRecovered.text = "" + resultStatsInstance.b2HealthRecoverd;
            staminaUsed.text = "" + resultStatsInstance.b2StaminaUsed;
            dodgesPerformed.text = "" + resultStatsInstance.b2DodgesPerformed;
            throwablesUsed.text = "" + resultStatsInstance.b2ThrowablesUsed;
            techniqueUsed.text = "" + resultStatsInstance.b2TechniqueUsage;
            souvenirUsed.text = "" + resultStatsInstance.b2SouvenirUsage;
        }
    }

    public void MainMenu()
    {
        StartCoroutine(MainMenuCoroutine());
    }

    IEnumerator MainMenuCoroutine()
    {
        StateNameController.useMainMenu = false;
        blackOut.GetComponent<Fade>().fadeIn = true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }
}