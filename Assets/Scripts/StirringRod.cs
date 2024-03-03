using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StirringRod : MonoBehaviour
{
    private float _touchTime = 0f;
    private bool _isTouching = false;
    private Coroutine _agglutinationProcess;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Circle"))
        {
            var tabletCircle = other.GetComponent<TabletCircle>();
            if (!_isTouching)
            {
                _isTouching = true;
                _touchTime = Time.time;
            }
            else if (Time.time - _touchTime >= 3f && _agglutinationProcess == null && !tabletCircle.agglutinatedProcess)
            {
                Debug.Log("OnTriggerStay");
                _agglutinationProcess = StartCoroutine(AgglutinationRoutine(tabletCircle));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Circle"))
        {
            Debug.Log("OnTriggerExit ПАЛКА");
            ResetTouch();
        }
    }

    private IEnumerator AgglutinationRoutine(TabletCircle tabletCircle)
    {
        Debug.Log("CheckAgglutination");
        yield return new WaitForSeconds(5f);
        tabletCircle.CheckAgglutination();
        _agglutinationProcess = null;
    }

    private void ResetTouch()
    {
        _isTouching = false;
        _touchTime = 0f;
    }
}
