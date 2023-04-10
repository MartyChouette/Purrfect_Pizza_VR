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
    private bool _isSelected = false;
    
    void Start()
    {
        _buttonImage = GetComponent<Image>();
        _unselectedColor = _buttonImage.color;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(select);
    }

    private void select()
    {
        _isSelected = !_isSelected;
        _buttonImage.color = (_isSelected)? _selectedColor : _unselectedColor;
    }

    public bool selected
    {
        get
        {
            return _isSelected;
        }
    }
}
