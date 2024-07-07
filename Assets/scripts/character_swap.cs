using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class character_swap : MonoBehaviour
{
    public GameObject[] leftCharacters; // Assign the 3 characters on z=4 in the inspector
    public GameObject[] rightCharacters; // Assign the 3 characters on z=-4 in the inspector
    public GameObject hmd; // Assign the HMD in the inspector
    public GameObject leftCharacterInstance;
    public GameObject rightCharacterInstance;
    private List<GameObject> leftSequence;
    private List<GameObject> rightSequence;
    private int interaction_counter = 0;
    public int num_interactions; //Assign the number of interactions in the inspector
    private int leftIndex = 0;
    private int rightIndex = 0;
    private bool changeLeftNext = true;
    private float lastZPosition;
    public int total_shoko = 0;
    public int total_vanil = 0;


    void Start()
    {
        InitializeLeftSequence();
        InitializeRightSequence();
        DisplayInitialCharacters();
        lastZPosition = hmd.transform.position.z;
        StartCoroutine(CheckProximityAndChangeCharacter());
    }

    void InitializeLeftSequence()
    {
        leftSequence = new List<GameObject>(leftCharacters);
        ShuffleList(leftSequence);
    }

    void InitializeRightSequence()
    {
        rightSequence = new List<GameObject>(rightCharacters);
        ShuffleList(rightSequence);
    }


    void ShuffleList(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void DisplayInitialCharacters()
    {
        leftCharacterInstance = leftSequence[leftIndex];
        rightCharacterInstance = rightSequence[rightIndex];
        leftCharacterInstance.SetActive(true);
        rightCharacterInstance.SetActive(true);
    }

    IEnumerator CheckProximityAndChangeCharacter()
    {
        while (true)
        {
            float currentZPosition = hmd.transform.position.z;

            // Check if player crosses the z=-1 plane
            if ((lastZPosition > -1 && currentZPosition <= -1) || (lastZPosition < -1 && currentZPosition >= -1))
            {
                if (changeLeftNext)
                {
                    ChangeLeftCharacter();
                    if (leftIndex == 2)
                    {
                        InitializeLeftSequence();
                    }
                }
                else
                {
                    ChangeRightCharacter();
                    if (rightIndex == 2)
                    {
                        InitializeRightSequence();
                    }
                }
                interaction_counter++;
                changeLeftNext = !changeLeftNext;
            }
            lastZPosition = currentZPosition;
            yield return null;
            if (interaction_counter == num_interactions)
            {
                end_interactions();
                break;
            }
        }
    }


    void ChangeLeftCharacter()
    {
        leftCharacterInstance.SetActive(false);
        leftIndex = (leftIndex + 1) % leftSequence.Count;
        leftCharacterInstance = leftSequence[leftIndex];
        leftCharacterInstance.SetActive(true);
    }

    void ChangeRightCharacter()
    {
        rightCharacterInstance.SetActive(false);
        rightIndex = (rightIndex + 1) % rightSequence.Count;
        rightCharacterInstance = rightSequence[rightIndex];
        rightCharacterInstance.SetActive(true);
    }

    private void end_interactions()
    {
        rightCharacterInstance.SetActive(false);
        leftCharacterInstance.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("menu");

    }
}