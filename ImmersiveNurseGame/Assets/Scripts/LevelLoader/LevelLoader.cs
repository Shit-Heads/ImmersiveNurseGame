using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] private float _transitionTime = 1f;
    [SerializeField] private float _waitBeforeAnim = 0;
    [SerializeField] private int _sceneIndex;

    IEnumerator switchScene(){
        yield return new WaitForSeconds(_waitBeforeAnim);
        _animator.SetTrigger("transition");
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(_sceneIndex);
    }

    IEnumerator quitGame(){
        yield return new WaitForSeconds(_transitionTime);
        Application.Quit();
    }

    public void loadScene(){
        StartCoroutine(switchScene());
    }

    public void quitApplcation(){
        StartCoroutine(quitGame());
    }
}
