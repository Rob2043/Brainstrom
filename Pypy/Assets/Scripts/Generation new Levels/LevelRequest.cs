using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LevelRequest : MonoBehaviour
{
    // URL вашего API
    private string apiUrl = "https://api.yourserver.com/generateLevel";

    void Start()
    {
        StartCoroutine(RequestLevelData("medium"));
    }

    IEnumerator RequestLevelData(string difficulty)
    {
        // Подготавливаем JSON-запрос
        string json = JsonUtility.ToJson(new LevelRequestData(difficulty));

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Отправляем запрос и ждем ответа
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Получаем ответ в виде JSON
            string jsonResponse = request.downloadHandler.text;
            LevelData levelData = JsonUtility.FromJson<LevelData>(jsonResponse);

            // Генерируем уровень на основе данных
            GenerateLevel(levelData);
        }
        else
        {
            Debug.LogError("Ошибка при запросе данных уровня: " + request.error);
        }
    }

    // Данные запроса (например, сложность уровня)
    [System.Serializable]
    public struct LevelRequestData
    {
        public string difficulty;
        public LevelRequestData(string difficulty)
        {
            this.difficulty = difficulty;
        }
    }

    // Структура данных для уровня, возвращаемая API
    [System.Serializable]
    public struct LevelData
    {
        public int width;
        public int height;
        public int[] tiles; // Массив данных тайлов уровня (0 - пол, 1 - стена)
    }

    void GenerateLevel(LevelData levelData)
    {
        // Пример генерации уровня на основе данных из API
        for (int x = 0; x < levelData.width; x++)
        {
            for (int y = 0; y < levelData.height; y++)
            {
                int tileType = levelData.tiles[y * levelData.width + x];
                
                if (tileType == 0)
                {
                    // Генерируем пол
                    Instantiate(floorPrefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                else if (tileType == 1)
                {
                    // Генерируем стену
                    Instantiate(wallPrefab, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }
}
