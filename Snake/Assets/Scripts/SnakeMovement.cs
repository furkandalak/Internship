using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float radius = 1.0f;
    
    public Food food;
    public int initialSize = 4;
    private List<Vector3> positions = new List<Vector3>();
    private List<int> positionsIndex = new List<int>();

    public float deltaDistance;
    
    private List<List<Vector3>> positionHistory = new List<List<Vector3>>();
    public LineRenderer lineRenderer;
    
    private List<Vector3> mousePositions = new List<Vector3>();
    
    public float segmentSpacing = 2.0f; // Segmentler arasındaki mesafe
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Yılanın başlangıç konumlarını ayarla
        for (int i = 0; i < initialSize; i++)
        {
            positionsIndex.Add(0);
            //positions.Add(new Vector3(-i * segmentSpacing, 0, 0));
            positions.Add(new Vector3(0, 0, 0));
            mousePositions.Add(transform.position);
        }
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }
    void Update()
    {
        // Fare konumunu al ve düzelt
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        
        if (Input.GetMouseButton(0)) // Farenin sol tuşu basılı ise
        {
            // Konumu listeye ekle
            transform.position = mousePosition;
            mousePositions.Add(transform.position);
            positions[0] = transform.position;
            // Hedef konuma git
            Vector3 direction = (mousePosition - transform.position).normalized;
            direction.Normalize();
        }
        // Yemi kontrol et
        if (Vector3.Distance(transform.position, food.transform.position) < 1.0f)
        {
            Grow();
            food.Start();
        }
        // Yılanın geri kalan bölümlerini güncelle
        for (int i = 1; i < positions.Count; i++)
        {
            // Olması gereken mesafeyi hesapla
            float myLength = segmentSpacing * i;
            float dist = 0;
            Vector3 myPos = new Vector3(0, 0, 0);
            
            for (int j = mousePositions.Count - 1; j > 0; j--)
            {
                dist += Vector3.Distance(mousePositions[j], mousePositions[j - 1]);
                // Mesafeden fazlaysa noktaları bul
                if (dist > myLength)
                {
                    dist -= myLength;
                    float curL = Vector3.Distance(mousePositions[j], mousePositions[j - 1]);
                    // Bulunan iki konum arasındaki gerçek konumu hesapla
                    Vector3 bonus = Vector3.Lerp(mousePositions[j - 1], mousePositions[j], dist/curL);
                    myPos = bonus;
                    break;
                }
            }

            positions[i] = myPos;
        }
        lineRenderer.SetPositions(positions.ToArray());
        for (int i = 0; i < mousePositions.Count - 1; i++)
        {
            Debug.DrawLine(mousePositions[i], mousePositions[i + 1]);
        }
        
    }

    void OnDrawGizmos()
    {
        // Her bir segment için bir çember çiz
        Gizmos.color = Color.white;
        foreach (Vector3 position in positions)
        {
            Gizmos.DrawWireSphere(position, radius);
        }
    }
    
    void Grow()
    {
        // Yeni bir bölüm oluştur ve listenin sonuna ekle
        Vector3 newPosition = positions[positions.Count - 1];
        positions.Add(newPosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPositions(positions.ToArray());
    }
}