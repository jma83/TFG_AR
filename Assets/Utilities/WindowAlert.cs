using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowAlert : Singleton<WindowAlert>
{
    [SerializeField] GameObject alert;
    [SerializeField] Text infoText;
    [SerializeField] Button bt1;
    [SerializeField] Button bt2;
    [SerializeField] Button bt3;
    public delegate void DelegateFunctionParam(GameObject gm=null,int j=0);
    public delegate void DelegateFunction();
    public DelegateFunctionParam funcion_param;
    public DelegateFunction funcion;
    // Use this for initialization
    void Start()
    {
        bt1.onClick.AddListener(TaskOnClick2);
        bt2.onClick.AddListener(TaskOnClick1);
        bt3.onClick.AddListener(TaskOnClick2);

    }

    public void CreateInfoWindow(string infoText1, bool active)
    {
        alert.SetActive(active);

        bt1.enabled = false;
        bt1.gameObject.SetActive(bt1.enabled);
        bt2.enabled = false;
        bt2.gameObject.SetActive(bt2.enabled);
        bt3.enabled = true;
        bt3.gameObject.SetActive(bt3.enabled);

        infoText.text = infoText1;

    }

    public void CreateConfirmWindow(string confirmText, bool active, DelegateFunction f = null, DelegateFunctionParam f2 =null)
    {
        alert.SetActive(active);
        bt1.enabled = false;
        bt1.gameObject.SetActive(bt1.enabled);
        bt2.enabled = false;
        bt2.gameObject.SetActive(bt2.enabled);
        bt3.enabled = true;
        bt3.gameObject.SetActive(bt3.enabled);

        infoText.text = confirmText;
        funcion_param = f2;
        funcion = f;


    }

    public void CreateSelectWindow(string confirmText, bool active, DelegateFunction f = null, DelegateFunctionParam f2 = null)
    {
        alert.SetActive(active);
        bt3.enabled = false;
        bt3.gameObject.SetActive(bt3.enabled);
        bt1.enabled = true;
        bt1.gameObject.SetActive(bt1.enabled);
        bt2.enabled = true;
        bt2.gameObject.SetActive(bt2.enabled);

        infoText.text = confirmText;
        funcion = f;
        funcion_param = f2;



    }

    public void SetDelegateFunction(DelegateFunction f, DelegateFunctionParam f2)
    {
        funcion = f;
        funcion_param = f2;
    }

    public DelegateFunctionParam GetDelegateFunctionParam()
    {
        return funcion_param;
    }

    public DelegateFunction GetDelegateFunction()
    {
        return funcion;
    }

    public void SetActiveAlert()
    {
        alert.SetActive(true);
    }

    void TaskOnClick1()
    {
        /*if (funcion_param != null)
            funcion_param();
        if (funcion != null)
            funcion();*/

        alert.SetActive(false);

    }
    void TaskOnClick2()
    {
        if (funcion_param != null)
            funcion_param();
        if (funcion != null)
            funcion();
        //disable alert and window alert
        alert.SetActive(false);
    }
}
