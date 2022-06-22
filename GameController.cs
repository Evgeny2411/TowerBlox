using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameController : MonoBehaviour
{

    private static bool roofLose;
    public static bool playing;
    public bool lose = false;
    bool totalLose = false;
    public Transform crane;
    public Transform ground;
    private Transform onHook;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject menuButton;

    [SerializeField]
    Transform buildingBlock, roofBlock;

    private int numBlocks = 0;

    void Start()
    {
        generateBlock(buildingBlock);
        playing = true;

    }
    void Update()
    {

        controlCam();
        if (detectFalling.lives == 0)
        {
            lose = true;
        }
        if ((Input.GetMouseButtonDown(0) || Input.touchCount < 0) & onHook != null)
        {
            StartCoroutine(building());
            if (totalLose == true)
            {
                StartCoroutine(dropRoof());
                StopCoroutine(building());
            }
        }

        scoreText.text = "Score: " + numBlocks.ToString();
        livesText.text = "Lives: " + detectFalling.lives.ToString();

    }
    //регулирует камеру от высоты башни
    void controlCam()
    {
        if (getHighestBlock() == null)
        {
            return;
        }
        if (numBlocks > 0 && crane.transform.GetChild(1).position.y - getHighestBlock().transform.position.y < 10.5)
        {
            crane.transform.position = crane.transform.position + new Vector3(0, 1.5f, 0) * Time.deltaTime;
            ground.transform.position = ground.transform.position + new Vector3(0, 1.5f, 0) * Time.deltaTime;
        }
        else if (numBlocks > 0 && crane.transform.GetChild(1).position.y - getHighestBlock().transform.position.y > 15)
        {
            if (numBlocks > 0 && crane.transform.GetChild(1).position.y - getHighestBlock().transform.position.y > 20)
            {
                crane.transform.position = crane.transform.position + new Vector3(0, 50f, 0) * -Time.deltaTime;
                ground.transform.position = ground.transform.position + new Vector3(0, 50f, 0) * -Time.deltaTime;
            }
            else
            {
                crane.transform.position = crane.transform.position + new Vector3(0, 1.5f, 0) * -Time.deltaTime;
                ground.transform.position = ground.transform.position + new Vector3(0, 1.5f, 0) * -Time.deltaTime;
            }
        }
    }
    //запускает сброс крыши
    IEnumerator dropRoof()
    {
        if (lose == true & onHook == roofBlock)
        {
            GameObject roof = crane.transform.GetChild(2).gameObject as GameObject;
            roof.AddComponent<Rigidbody>();
            roof.tag = "roof";
            crane.transform.GetChild(2).SetParent(null);


            yield return new WaitForSeconds(3);
            playing = false;

        }
        if(playing == false)
        {
            StopCoroutine(dropRoof());
            menuButton.SetActive(true);
        }

    }


    //сбрасывает обычные блоки, проверяет что бы блок упал нормально, генерирует блок в зависимости от результатов
    IEnumerator building()
    {
        if (totalLose != true)
        {
            GameObject dropping = crane.transform.GetChild(2).gameObject as GameObject;
            dropping.AddComponent<Rigidbody>();
            dropping.name = "dropping";
            dropping.tag = "recentBlock";
            crane.transform.GetChild(2).SetParent(null);
            onHook = null;

            yield return new WaitForSeconds(3);

            if (dropping)
            {
                dropping.name = "TowerPart";
            }
            numBlocks++;

            if (numBlocks > 1 & getHighestBlock() != null)
            {
                if (!(GameObject.Find("TowerTop") is null))
                {
                    GameObject.Find("TowerTop").name = "TowerPart";
                }
                getHighestBlock().name = "TowerTop";
            }
            if (lose != true)
            {
                generateBlock(buildingBlock);
            }
            else
            {
                generateBlock(roofBlock);
                totalLose = true;
            }


        }

    }

    //возвращает верхний блок башни
    GameObject getHighestBlock()
    {
        GameObject[] tower = GameObject.FindGameObjectsWithTag("recentBlock");
        GameObject highestBlock = null;
        float highestBlockPosition = -99999f;
        for (int f = 0; f < tower.Length; f++)
        {
            float thisY = tower[f].transform.position.y;
            if (thisY > highestBlockPosition)
            {
                highestBlockPosition = thisY;
                highestBlock = tower[f];
            }
        }
        if (numBlocks > 1 & highestBlock == null)
        {
            lose = true;
        }
        return highestBlock;
    }
    // генерирует блок на кране
    void generateBlock(Transform blockToGen)
    {
        Transform block = Instantiate(blockToGen);
        block.SetParent(crane);
        if (blockToGen == roofBlock)
        {
            block.position = crane.transform.GetChild(1).transform.position + new Vector3(0, -2.6f, 0);
        }
        else
        {
            block.position = crane.transform.GetChild(1).transform.position + new Vector3(0, -1.9f, 0);
        }
        block.rotation = Quaternion.Euler(0, 0, rotation.finalAngle);
        Destroy(block.GetComponent<Rigidbody>());
        onHook = blockToGen;

    }

}