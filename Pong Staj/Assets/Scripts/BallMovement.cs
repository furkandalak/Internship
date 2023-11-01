using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Random = UnityEngine.Random;

public class BallMovement : MonoBehaviour
{
    public Transform ball;
    public Transform paddle1;
    public Transform paddle2;

    public float edgeFollow = 11f;
    public float speed = 5f;
    public Vector2 direction;

    private int score1 = 0;
    private int score2 = 0;
    
    private Vector3 ballVelocity;
    private List<LineSegment> lineSegments;
   

    enum SegmentName
    {
        LeftPaddle = 0,
        RightPaddle = 1,
        TopWall = 2,
        BottomWall = 3,
    }

    void Start()
    {
        //Rastgele direction ayarla
        direction = new Vector3(Random.Range(0, 2) == 0 ? -1 : 1, Random.Range(0, 2) == 0 ? -1 : 1, 0);
        
        //Collision lineları ayarla
        Vector3 paddle1TopEdge = paddle1.position - new Vector3(0, paddle1.transform.localScale.y / 2, 0);
        Vector3 paddle1BottomEdge = paddle1.position + new Vector3(0, paddle1.transform.localScale.y / 2, 0);
        Vector3 paddle2TopEdge = paddle2.position - new Vector3(0, paddle2.transform.localScale.y / 2, 0);
        Vector3 paddle2BottomEdge = paddle2.position + new Vector3(0, paddle2.transform.localScale.y / 2, 0);

        Vector3 topLeft = new Vector3(+edgeFollow, 5, 0);
        Vector3 topRight = new Vector3(-edgeFollow, 5, 0);

        Vector3 bottomLeft = new Vector3(+edgeFollow, -5, 0);
        Vector3 bottomRight = new Vector3(-edgeFollow, -5, 0);
        
        Vector3 leftScoreTop = new Vector3(-11, -5, 0);
        Vector3 leftScoreBottom = new Vector3(-11, 5, 0);
        
        Vector3 rightScoreTop = new Vector3(11, -5, 0);
        Vector3 rightScoreBottom = new Vector3(11, 5, 0);
        
        lineSegments = new List<LineSegment>
        {
            new LineSegment(paddle1BottomEdge, paddle1TopEdge, Vector3.right),
            new LineSegment(paddle2BottomEdge, paddle2TopEdge, Vector3.left),
            new LineSegment(topLeft, topRight, Vector3.down),
            new LineSegment(bottomLeft, bottomRight, Vector3.up),
            new LineSegment(leftScoreTop, leftScoreBottom, Vector3.right),
            new LineSegment(rightScoreTop, rightScoreBottom, Vector3.left),
        };
    }

    void Update()
    {
        // Topun bir sonraki pozisyonunu tahmin et
        Vector3 ballVelocity = direction * speed;

        Vector3 nextPosition = ball.position + ballVelocity * Time.deltaTime;

        // Paddle'ın kenarlarını ve ekranın kenarlarını temsil eden çizgi parçalarını oluştur

        var leftPaddleSegment = lineSegments[(int)SegmentName.LeftPaddle];
        leftPaddleSegment.start = paddle1.position - new Vector3(0, paddle1.transform.localScale.y / 2, 0);
        leftPaddleSegment.end = paddle1.position + new Vector3(0, paddle1.transform.localScale.y / 2, 0);
        lineSegments[(int)SegmentName.LeftPaddle] = leftPaddleSegment;

        var rightPaddleSegment = lineSegments[(int)SegmentName.RightPaddle];
        rightPaddleSegment.start =  paddle2.position - new Vector3(0, paddle2.transform.localScale.y / 2, 0);
        rightPaddleSegment.end = paddle2.position + new Vector3(0, paddle2.transform.localScale.y / 2, 0);
        lineSegments[(int)SegmentName.RightPaddle] = rightPaddleSegment;
        
        
        // Topun hareketini temsil eden çizgi parçasını oluştur
        Vector3 ballStart = ball.position;
        Vector3 ballEnd = nextPosition;

        Debug.DrawLine(ballStart, ballEnd, Color.red);


        // Topun hareket çizgisi ile paddle'ın kenarları ve ekranın kenarları arasında kesişme kontrolü yap

        foreach (var lineSegment in lineSegments)
        {
            Debug.DrawLine(lineSegment.start, lineSegment.end, Color.red);
        }


        foreach (var lineSegment in lineSegments)
        {
            if (LineSegmentIntersection(ballStart, ballEnd, lineSegment.start, lineSegment.end, out Vector3 intersectionPoint))
            {
                direction = Vector3.Reflect(direction, lineSegment.normal);
                if (intersectionPoint.x <= -11f)
                {
                    score1 += 1;
                    intersectionPoint = new Vector3(0, 0, 0);
                    direction = new Vector3(1, Random.Range(0, 2) == 0 ? -1 : 1, 0);
                }
                
                if (intersectionPoint.x >= 11f)
                {
                    score2 += 1;
                    intersectionPoint = new Vector3(0, 0, 0);
                    direction = new Vector3(-1, Random.Range(0, 2) == 0 ? -1 : 1, 0);
                }
                
                Debug.DrawLine(lineSegment.start, lineSegment.end, Color.green);

                Debug.DrawLine(ball.position, intersectionPoint, Color.blue);
                

                // Çarpışma noktasının biraz ötesine yerleştirerek, topun çarpışma noktasında sıkışmasını önler
                float distanceToMove = .1f;
                nextPosition = intersectionPoint + (Vector3)lineSegment.normal * distanceToMove;
                // Topu hareket ettir
                Debug.DrawLine(nextPosition, intersectionPoint, Color.green);
                break;
            }
        }
        
        


        // Topu hareket ettir
        ball.position = nextPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(ball.transform.position, direction);
    }


    public struct LineSegment
    {
        public Vector3 start;
        public Vector3 end;
        public Vector3 normal;

        public LineSegment(Vector3 start, Vector3 end, Vector3 normal)
        {
            this.start = start;
            this.end = end;
            this.normal = normal;
        }
    }

    bool LineSegmentIntersection(Vector3 p1, Vector3 p2, Vector3 q1, Vector3 q2, out Vector3 intersectionPoint)
    {
        Vector3 r = p2 - p1;
        Vector3 s = q2 - q1;

        float d = r.x * s.y - r.y * s.x;
        float u = ((q1.x - p1.x) * r.y - (q1.y - p1.y) * r.x) / d;
        float t = ((q1.x - p1.x) * s.y - (q1.y - p1.y) * s.x) / d;
        if ((0 <= u && u <= 1 && 0 <= t && t <= 1))
        {
            intersectionPoint = p1 + t * r;
            return true;
        }

        intersectionPoint = default;
        return false;
    }
    
    bool LineSegmentIntersectionOriginal(Vector3 p1, Vector3 p2, Vector3 q1, Vector3 q2, out Vector3 intersectionPoint)
    {
        float A1 = p2.y - p1.y;
        float B1 = p1.x - p2.x;
        float C1 = A1 * p1.x + B1 * p1.y;

        float A2 = q2.y - q1.y;
        float B2 = q1.x - q2.x;
        float C2 = A2 * q1.x + B2 * q1.y;

        float det = A1 * B2 - A2 * B1;

        if (Mathf.Abs(det) < 0.0001f)
        {
            // Çizgiler paralelse
            intersectionPoint = Vector3.zero;
            return false;
        }

        float x = (B2 * C1 - B1 * C2) / det;
        float y = (A1 * C2 - A2 * C1) / det;
        //Debug.Log(x);
        //Debug.Log(y);
        if ((x >= Mathf.Min(p1.x, p2.x) && x <= Mathf.Max(p1.x, p2.x)) &&
            (y >= Mathf.Min(p1.y, p2.y) && y <= Mathf.Max(p1.y, p2.y)) &&
            (x >= Mathf.Min(q1.x, q2.x) && x <= Mathf.Max(q1.x, q2.x)) &&
            (y >= Mathf.Min(q1.y, q2.y) && y <= Mathf.Max(q1.y, q2.y)))
        {
            intersectionPoint = new Vector3(x, y, 0);
            return true;
        }

        intersectionPoint = Vector3.zero;
        return false;
    }

    public Font font;
    private void OnGUI()
    {
        GUI.skin.font = font;
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;
        
        GUI.Label(new Rect(Screen.width / 2 - 125 - 50, -50, 250, 250), score2.ToString(), style);
        GUI.Label(new Rect(Screen.width / 2 - 125 + 50, -50, 250, 250), score1.ToString(), style);
    }
}