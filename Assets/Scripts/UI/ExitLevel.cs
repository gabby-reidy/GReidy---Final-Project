using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OpenExit()
    {
        gameObject.SetActive(true);
        //sfx
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered exit door");
            SceneManager.LoadScene("LevelTwo"); // change this to iterate so it can be reused
        }
    }
}
