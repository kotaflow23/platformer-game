using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    [SerializeField] public string sceneName;
     int count = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    { // Update is called every frame.
        if (count >= 250)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    // Fixed Update is called every .02 seconds
    void FixedUpdate()
    {
        count += 1;
    }
}
