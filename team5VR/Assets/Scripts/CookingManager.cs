using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class CookingManager : MonoBehaviour
{
    // It's script in pot
    // If pot colide food Ingredient object


    // If ( FoodMaterailSum < 3 )
    // 2. FoodIngredientDic[name]++;
    // 3. CookingUI update

    // else { TextUI Activated a few second. (ex. "It's Full.") }

    // food Ingredient object destroy

    public Dictionary<string, int> foodIngredientDic = new Dictionary<string, int>()
    {
        {"Apple", 0},
        {"Acorn", 0},
        {"Cherry", 0},
        {"Carrot", 0},
        {"Corn", 0},
        {"Eggplant", 0},
        {"Tomato", 0},
        {"Turnip", 0}
    };

    private bool isCook = false;
    private int foodIngredientSum = 0;
    private int FruitSum = 0;
    private int RootSum = 0;
    private GameObject FoodObject;

    void Start()
    {
        IngredientImageReset();
        cookingUIUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCook)
        {
            if (other.CompareTag("fruits") || other.CompareTag("crops"))
            {
                if (foodIngredientDic.ContainsKey(other.GetComponent<ItemInfo>().GetId()))
                {
                    if (foodIngredientSum < 3)
                    {
                        foodIngredientDic[other.GetComponent<ItemInfo>().GetId()]++;

                        FruitSum = foodIngredientDic["Apple"] + foodIngredientDic["Acorn"] + foodIngredientDic["Cherry"];
                        RootSum = foodIngredientDic["Carrot"] + foodIngredientDic["Corn"] + foodIngredientDic["Eggplant"]
                            + foodIngredientDic["Tomato"] + foodIngredientDic["Turnip"];
                        foodIngredientSum = FruitSum + RootSum;

                        cookingUIUpdate();
                    }
                    else
                    {
                        isFullText();
                    }
                    Destroy(other.gameObject);
                }
            }
        }
    }

    // void Cooking : Cook by FoodMaterailNum.
    // If ( FoodIngredientSum >= 2 ) { CookingButton Activated. }
    // If player push cooking button
    // 1. 
    // isCook = true;

    public void Cooking()
    {
        if(foodIngredientSum >= 2)
        {
            whatFood();
            Delete();
            isCook = true;
            cookingUIUpdate();
        }
        else
        {
            isNotEnoughText();
        }
    }

    private void whatFood()
    {
        if (foodIngredientDic["Corn"] > 0 && FruitSum > 0)
        {
            FoodObject = Instantiate(FoodSet[0], transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            FoodObject.GetComponent<XRGrabInteractable>().selectEntered.AddListener(OnSelectEntered);
        }
        else if (RootSum >= 2 && FruitSum == 0)
        {
            FoodObject = Instantiate(FoodSet[1], transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            FoodObject.GetComponent<XRGrabInteractable>().selectEntered.AddListener(OnSelectEntered);
        }
        else if (FruitSum == 1 && RootSum == 2)
        {
            FoodObject = Instantiate(FoodSet[2], transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            FoodObject.GetComponent<XRGrabInteractable>().selectEntered.AddListener(OnSelectEntered);
        }
        else
        {
            FoodObject = Instantiate(FoodSet[3], transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            FoodObject.GetComponent<XRGrabInteractable>().selectEntered.AddListener(OnSelectEntered);
        }
    }


    // void Delete : All FoodIngredientNum Delete in CookinManager.
    public void Delete()
    {
        string[] keys = new string[foodIngredientDic.Count];
        foodIngredientDic.Keys.CopyTo(keys, 0);

        foreach (string key in keys)
        {
            foodIngredientDic[key] = 0;
        }
        foodIngredientSum = 0;
        FruitSum = 0;
        RootSum = 0;

        cookingUIUpdate();
    }

    [SerializeField]
    private GameObject[] FoodSet;

    // If ( isCook =  true )
    // Player can take a food in dish.

    private void OnSelectEntered(SelectEnterEventArgs args) => TakeFood();

    private void TakeFood()
    {
        FoodObject.GetComponent<XRGrabInteractable>().selectEntered.RemoveAllListeners();
        FoodObject.GetComponent<Rigidbody>().isKinematic = false;
        FoodObject = null;
        isCook = false;
        cookingUIUpdate();
    }

    // Cooking UI
    [SerializeField]
    private GameObject NoticeCanvas;
    [SerializeField]
    private TMPro.TMP_Text Text;

    [SerializeField]
    private GameObject IngredientUIcanvas;

    [SerializeField]
    private Sprite[] IngredientImageSet;


    [SerializeField]
    private Image IngredientImage1;
    [SerializeField]
    private Image IngredientImage2;
    [SerializeField]
    private Image IngredientImage3;


    private void cookingUIUpdate()
    {
        if (!isCook)
        {
            IngredientUIcanvas.SetActive(true);

            int DicKeyNum = 0;

            IngredientImageReset();

            foreach (int num in foodIngredientDic.Values)
            {
                for (int i = 0; i < num; i++)
                {
                    if (IngredientImage1.sprite == null)
                    {
                        IngredientImage1.sprite = IngredientImageSet[DicKeyNum];
                        IngredientImage1.gameObject.SetActive(true);
                    }
                    else if(IngredientImage2.sprite == null)
                    {
                        IngredientImage2.sprite = IngredientImageSet[DicKeyNum];
                        IngredientImage2.gameObject.SetActive(true);
                    }
                    else if(IngredientImage3.sprite == null)
                    {
                        IngredientImage3.sprite = IngredientImageSet[DicKeyNum];
                        IngredientImage3.gameObject.SetActive(true);
                    }
                }
                DicKeyNum++;
            }
        }
        else
        {
            IngredientImageReset();
            IngredientUIcanvas.SetActive(false);
        }
    }

    private void IngredientImageReset()
    {
        IngredientImage1.sprite = null;
        IngredientImage2.sprite = null;
        IngredientImage3.sprite = null;

        IngredientImage1.gameObject.SetActive(false);
        IngredientImage2.gameObject.SetActive(false);
        IngredientImage3.gameObject.SetActive(false);
    }

    // 재료 못 넣는다는 Text 띄우기
    private void isFullText()
    {
        NoticeCanvas.gameObject.SetActive(true);
        Text.text = "Pot is Full!";
        StartCoroutine(CoText());
    }

    // 요리 불가 Text 띄우기
    private void isNotEnoughText()
    {
        NoticeCanvas.gameObject.SetActive(true);
        Text.text = "It's not enough!";
        StartCoroutine(CoText());
    }

    IEnumerator CoText()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        NoticeCanvas.gameObject.SetActive(false);
    }
}