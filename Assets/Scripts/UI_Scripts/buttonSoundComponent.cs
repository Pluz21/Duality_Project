using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class buttonSoundComponent : MonoBehaviour
{
    [SerializeField] GameObject parentMenuElement = null;
    [SerializeField] List<Button> allButtons = new List<Button>();
    //Audio    
    //[SerializeField] AudioClip clickSound = null;
    [SerializeField] AudioSource soundSource = null;

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    public void Init()
    {
        soundSource = GetComponent<AudioSource>();

        InitButtonSounds(parentMenuElement);

    }
    void InitButtonSounds(GameObject _parentToAddButtonsFroms)
    {
        allButtons = _parentToAddButtonsFroms.GetComponentsInChildren<Button>().ToList(); //
        int _size = allButtons.Count;
        for (int i = 0; i < _size; i++)
        {
            allButtons[i].onClick.AddListener(PlayClickSound);
        }
    }


    public  void PlayClickSound()
    {
        soundSource.Play();
    }
}
