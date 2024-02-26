using TMPro;
using UnityEngine;

public class UpdateText : MonoBehaviour
{
    public GameManager.GameManagerVariables variable;

    private TMP_Text textComponent;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(variable == GameManager.GameManagerVariables.TIME)
        //{
        //    textComponent.text = "Time: " + GameManager.instance.GetTime();
        //}
        //else if(variable == GameManager.GameManagerVariables.POINTS)
        //{
        //    textComponent.text = "Points: " + GameManager.instance.GetPoints();
        //}
        //else
        //{

        //}
        switch (variable)
        {
            case GameManager.GameManagerVariables.TIME:
                textComponent.text = "Time: " + GameManager.instance.GetTime().ToString("0:00");
                break;
            case GameManager.GameManagerVariables.POINTS: 
                textComponent.text = "Points: " + GameManager.instance.GetPoints();
                break;
            default:
                break;
        }
    }
}
