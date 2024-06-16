using CustomEventBus;
using Pypy;
using TMPro;
using UnityEngine;

public class Scin : MonoBehaviour
{
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private TMP_Text _textForNamePlayer;
    [SerializeField] private TMP_Text _textForCountSatrs;
    private DataOfPlayer[] _ArraydataOfPlayer = new DataOfPlayer[10];
    private DataOfPlayer _dataOfPlayer;

    private void Awake()
    {
        _textForCountSatrs.text = $"{EventBus.GetStars.Invoke()}";
        _ArraydataOfPlayer = Iinstance.instance._dataOfPlayer;
        EventBus.ChangeNameText = ChangeScins;
    }

    private void ChangeScins(GameObject scin)
    {
        _dataOfPlayer = scin.GetComponent<IInstansePlayer>().DataOfPlayer;
        _textForNamePlayer.text = _dataOfPlayer.NameOfScins;
        if (_dataOfPlayer.WasBuying == true)
            _buttonText.text = "Choose";
        else _buttonText.text = $"Buy {_dataOfPlayer.price}";
        if (_dataOfPlayer.WasChousing == true)
            _buttonText.text = "Selected";
    }

    public void ChooseScin()
    {
        if (_dataOfPlayer.WasBuying == false)
        {
            if (_dataOfPlayer.price <= EventBus.GetStars.Invoke())
            {
                EventBus.AddStars.Invoke(-_dataOfPlayer.price);
                _dataOfPlayer.WasBuying = true;
                _buttonText.text = "Choose";
                _textForCountSatrs.text = $"{EventBus.GetStars.Invoke()}";
            }
            else return;
        }
        else
        {
            if (_dataOfPlayer.WasChousing == false)
            {
                _buttonText.text = "Selected";
                for (int i = 0; i < _ArraydataOfPlayer.Length; i++)
                {
                    if (_ArraydataOfPlayer[i].NameOfScins == _dataOfPlayer.NameOfScins)
                        _dataOfPlayer.WasChousing = true;
                    else _ArraydataOfPlayer[i].WasChousing = false;
                }
            }
            else return;
        }
        EventBus.Save.Invoke();
    }
}
