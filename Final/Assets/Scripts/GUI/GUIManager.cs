using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public List<GUIWeapon> Weapons;
    public Text HpText;
    public GameObject ScoreBoard;
    public GameObject ScoreTable;
    public GameObject ScoreRow;
    GameSceneManager gameSceneManager;

    private void Awake()
    {
        gameSceneManager = FindObjectOfType<GameSceneManager>();
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (ScoreBoard.activeInHierarchy)
            {
                ScoreBoard.SetActive(false);
            }
            else
            {
                ScoreBoard.SetActive(true);
                RefreshScoreBoard();
            }
        }
    }

    public void SetWeaponAcive(Constants.Guns gunType, bool active)
    {
        foreach (GUIWeapon gUIWeapon in Weapons)
        {
            if (gUIWeapon.GunType == gunType)
            {
                gUIWeapon.SetWeaponActive(active);
            }
        }
    }

    public void SetWeaponAmmo(Constants.Guns gunType, int count)
    {
        foreach (GUIWeapon gUIWeapon in Weapons)
        {
            if (gUIWeapon.GunType == gunType)
            {
                gUIWeapon.SetAmmoCount(count);
            }
        }
    }

    public void SetHp(int value)
    {
        if (HpText != null)
        {
            HpText.text = value.ToString();
        }
    }

    public void RefreshScoreBoard()
    {
        // reset
        foreach (Transform childTransform in ScoreTable.transform)
        {
            Destroy(childTransform.gameObject);
        }

        if (gameSceneManager != null)
        {

            Dictionary<string, int> scores = gameSceneManager.Scores;
            foreach (var item in scores)
            {
                GameObject newRow = Instantiate(ScoreRow, ScoreTable.transform);
                GUIPlayerScoreRow tableRow = newRow.GetComponent<GUIPlayerScoreRow>();
                if (tableRow != null)
                {
                    tableRow.Display(item.Key, item.Value);
                }
            }
        }
    }
}
