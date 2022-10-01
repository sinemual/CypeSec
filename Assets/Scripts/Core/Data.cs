using System;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{

    #region Singleton Init
    private static Data _instance;

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static Data Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }

    static void Init() // Init script
    {
        _instance = FindObjectOfType<Data>();
        _instance.Initialize();
    }
    #endregion

    public bool panelIsOpen;

    [Header("Settings")]
    public bool s_sound;
    public bool s_music;
    public bool s_vibration;

    [Header("General")]
    public int level;

    public int cimsCounts;

    private int woody;
    private int stoney;
    private int metaly;
    private int mucus;
    private int meat;
    private int dungInviting;

    public int Woody
    {
        get => woody;
        set => woody = value;
    }
    public int Stoney
    {
        get => stoney;
        set => stoney = value;
    }
    public int Metaly
    {
        get => metaly;
        set => metaly = value;
    }
    public int Mucus
    {
        get => mucus;
        set => mucus = value;
    }
    public int Meat
    {
        get => meat;
        set => meat = value;
    }
    public int DungInviting
    {
        get => dungInviting;
        set { dungInviting = value; UIRefresh(); }
    }

    private void Initialize()
    {

    }

    private void UIRefresh()
    {
        UIManager.Instance.RefreshInfo();
    }

    public bool IsResourceAvaliable(Resource.Type _resource, int _amount)
    {
        if (_resource == Resource.Type.Woody)
            return Data.Instance.Woody >= _amount;
        if (_resource == Resource.Type.Stoney)
            return Data.Instance.Stoney >= _amount;
        if (_resource == Resource.Type.Metaly)
            return Data.Instance.Metaly >= _amount;
        if (_resource == Resource.Type.Meat)
            return Data.Instance.Meat >= _amount;
        if (_resource == Resource.Type.Mucus)
            return Data.Instance.Mucus >= _amount;
        return false;
    }

    public bool SetResource(Resource.Type _resourceType, int _amount)
    {
        if (_resourceType == Resource.Type.Woody)
        {
            if (_amount < 0 && !IsResourceAvaliable(_resourceType, _amount))
                return false;
            woody += _amount;
            return true;
        }
        if (_resourceType == Resource.Type.Stoney)
        {
            if (_amount < 0 && !IsResourceAvaliable(_resourceType, _amount))
                return false;
            stoney += _amount;
            return true;
        }
        if (_resourceType == Resource.Type.Metaly)
        {
            if (_amount < 0 && !IsResourceAvaliable(_resourceType, _amount))
                return false;
            metaly += _amount;
            return true;
        }
        if (_resourceType == Resource.Type.Mucus)
        {
            if (_amount < 0 && !IsResourceAvaliable(_resourceType, _amount))
                return false;
            mucus += _amount;
            return true;
        }
        if (_resourceType == Resource.Type.Meat)
        {
            if (_amount < 0 && !IsResourceAvaliable(_resourceType, _amount))
                return false;
            meat += _amount;
            return true;
        }
        UIRefresh();
        return false;
    }
}
