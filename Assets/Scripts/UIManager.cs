using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] marks;
    [SerializeField]
    private AnimationCurve animationCurve;
    [SerializeField]
    private Vector2 startPos;
    [SerializeField]
    private Vector2 stayPos;
    [SerializeField]
    private Vector2 endPos;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private float speedRate;
    private bool isRunning;

    #region event
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Test());

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    private void ReceiveMark(Mark receivedMark)
    {
        GameObject markObj = null;
        switch (receivedMark)
        {
            case Mark.Circle: markObj = marks[0]; break;
            case Mark.Cross: markObj = marks[1]; break;
            case Mark.Square: markObj = marks[2]; break;
            case Mark.Triangle: markObj = marks[3]; break;
            default: Debug.Log("no_mark"); break;
        }
        StartCoroutine(Go(markObj));
    }
    private IEnumerator Go(GameObject mark)
    {
        if (!isRunning) {
            isRunning = true;
            bool flag = false;
            while (!flag)
            {
                if (Vector2.Distance(mark.transform.localPosition, stayPos) < 0.01f)
                {
                    flag = true;
                }
                float curvePos = animationCurve.Evaluate(speedRate);
                mark.transform.localPosition = Vector2.Lerp(mark.transform.localPosition, stayPos, curvePos);
                yield return null;
            }
            flag = false;
            yield return new WaitForSeconds(waitTime);
            while (!flag)
            {
                if (Vector2.Distance(mark.transform.localPosition, endPos) < 0.01f)
                {
                    flag = true;
                }
                float curvePos = animationCurve.Evaluate(speedRate);
                mark.transform.localPosition = Vector2.Lerp(mark.transform.localPosition, endPos, curvePos);
                yield return null;
            }
            mark.transform.localPosition = startPos;
            isRunning = false;
        }

    }

    private IEnumerator Test()
    {
        int rand = 0;
        while (true)
        {
            rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0: ReceiveMark(Mark.Circle); break;
                case 1: ReceiveMark(Mark.Cross); break;
                case 2: ReceiveMark(Mark.Square); break;
                case 3: ReceiveMark(Mark.Triangle); break;
            }
               
            yield return null;
        }
    }

}
