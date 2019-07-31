using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{   
    [SerializeField] float delayTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
        
    }
    IEnumerator WaitAndLoad()
    {
       yield return new WaitForSeconds(delayTime);
       SceneManager.LoadScene("Game Over");
    }
        public void QuitGame()
    {
       Application.Quit();
    }
}
