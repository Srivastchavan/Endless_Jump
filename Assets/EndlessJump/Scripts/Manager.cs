using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{

    public int levelCount = 50;
    public Text coin = null;
    public Text distance = null;
    public Camera camera = null;
    public LevelGenerator levelGenerator = null;


    private int currentCoins = 0;
    private int currentDist = 0;
    private bool canPlay = false;

    public GameObject guiGameOver = null;
    private static Manager staticInst;
    public static Manager instance
    {
        get
        {
            if (staticInst == null)
            {
                staticInst = FindObjectOfType(typeof(Manager)) as Manager;
            }

            return staticInst;
        }
    }

    private void Start()
    {
        for (int i = 0; i < levelCount; i++)
        {
            levelGenerator.RandomGenerator();
        }


    }

    public void updateCoinCount(int value)
    {
        Debug.Log("Player picked up another coin" + value);
        currentCoins += value;
        coin.text = currentCoins.ToString();
    }

    public void updateDistCount()
    {
        Debug.Log("player moved forward by one pint");
        currentDist += 1;
        distance.text = currentDist.ToString();

        levelGenerator.RandomGenerator();
    }

    public void startPlay()
    {
        canPlay = true;
    }
    public bool checkCanPlay()
    {
        return canPlay;
    }

    public void gameOver()
    {
        camera.GetComponent<CameraShake>().shake();
        camera.GetComponent<CameraFollow>().enabled = false;

        GuiGameOver();
    }

    void GuiGameOver()
    {
        Debug.Log("Game over!");

        guiGameOver.SetActive(true);
    }

    public void playAgain()
    {
        Scene scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(scene.name);
    }

    public void quit()
    {
        Application.Quit();
    }
}
