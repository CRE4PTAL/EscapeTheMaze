using UnityEngine;

public class Summoner : MonoBehaviour
{
    public GameObject wall;  // Przeci¹gnij œcianê do tego pola w Inspectorze
    public float moveSpeed = 20f;  // Prêdkoœæ ruchu
    private Vector3 targetPosition;  // Docelowa pozycja œciany
    private bool isMoving = false;  // Flaga wskazuj¹ca, czy œciana jest w ruchu
    private bool isDone;

    public void Start()
    {
        isDone = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isDone)
        {
            if (other.CompareTag("Player"))  // SprawdŸ, czy to gracz
            {
                targetPosition = wall.transform.position + new Vector3(50, 0, 0);  // Ustal now¹ pozycjê
                isMoving = true;  // Rozpocznij ruch
                isDone = false;
            }
        }
    }

    void Update()
    {
        if (isMoving)
        {
            // Przesuwamy œcianê w stronê targetPosition
            wall.transform.position = Vector3.MoveTowards(wall.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Sprawdzamy, czy œciana osi¹gnê³a cel
            if (wall.transform.position == targetPosition)
            {
                isMoving = false;  // Zatrzymujemy ruch
            }
        }
    }
}
