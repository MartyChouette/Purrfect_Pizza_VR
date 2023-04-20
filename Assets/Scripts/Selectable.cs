using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selectable : MonoBehaviour
{   
    [SerializeField] private Color _selectedColor;
    private Image _buttonImage;
    private Color _unselectedColor;
    private Button _button;
    private Dough _dough;
    private bool _isSelected = false;
    private string _id;
    
    /*
    void Start()
    {
        _buttonImage = GetComponent<Image>();
        _unselectedColor = _buttonImage.color;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(select);
    }

    public void Init(string id, Dough dough)
    {
        _id = id;
        _dough = dough;
        GetComponentInChildren<Text>().text = dough.name;
    }

    public bool selected
    {
        get
        {
            return _isSelected;
        }
    }

    private void select()
    {
        if (!_isSelected)
        {
            TaskManager.Instance.TaskSelected(_id, _dough);
        }
        else
        {
            TaskManager.Instance.TaskUnselected(_id);
        }
        _isSelected = !_isSelected;
        _buttonImage.color = (_isSelected)? _selectedColor : _unselectedColor;
    }
    */
}
