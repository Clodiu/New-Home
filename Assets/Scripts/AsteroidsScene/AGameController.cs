using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script realizat de Oprea Vlad Ovidiu
public static class AGameControllerInitData
{
    public static float WaveLength = 25;
    public static int AsteroidWaveCount = 1;
    public static int AsteroidsPerWave = 35;
}

public class AGameController : MonoBehaviour
{
    [SerializeField]
    public GameObject asteroidPrefab;

    private float StartMinigameTime = 0;
    private float EndMinigameTime = 0;

    [SerializeField]
    public TextMeshProUGUI ui_text;

    void Start()
    {
        StartCoroutine(StartGameCo());
        StartMinigameTime  = Time.timeSinceLevelLoad;
        EndMinigameTime = Time.timeSinceLevelLoad + AGameControllerInitData.WaveLength;
    }

    void Update()
    {
        ui_text.text = string.Format("Ajungi în {0:0.0}s", EndMinigameTime - Time.timeSinceLevelLoad);
    }

    private IEnumerator StartGameCo()
    {
        // Codul pentru spawnarea asteroizilor.
        for(int i = 0; i < AGameControllerInitData.AsteroidWaveCount; i++)
        {
            // Cati asteroizi mai avem de spawnat in acest val.
            int AsteroidsToSpawn = AGameControllerInitData.AsteroidsPerWave;
            while(AsteroidsToSpawn > 0) {
                GameObject asteroid = Instantiate(asteroidPrefab, new Vector3(Random.Range(6f, 16f), Random.Range(-2f, 2f)), Quaternion.identity);
                float scale = Random.Range(1.5f, 3.5f);
                asteroid.transform.localScale = new Vector3(scale, scale, 1);
                AsteroidsToSpawn--;
            }

            // Asteptam pana la urmatorul val.
            yield return new WaitForSeconds(AGameControllerInitData.WaveLength / AGameControllerInitData.AsteroidWaveCount);
        }

        Debug.Log("MINIGAME DONE!");
        SceneManager.LoadScene(sceneName: TargetPlanet.PlanetName);
    }
}
