using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using USCG.Core.Telemetry;

public class PlayerMetricRecord : MonoBehaviour
{
    //private MetricId _deltaTimeMetric = default;
    public static PlayerMetricRecord Instance;
    private MetricId _spaceBarMetric = default;
    private MetricId _TimesDied = default;
    private MetricId _Level1FinishTime = default;

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _spaceBarMetric = TelemetryManager.instance.CreateAccumulatedMetric("SpaceBarMetric");
        _TimesDied = TelemetryManager.instance.CreateAccumulatedMetric("TimesDiedMetric");
        _Level1FinishTime = TelemetryManager.instance.CreateSampledMetric<float>("Level1FinishTimeMetric");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            TelemetryManager.instance.AccumulateMetric(_spaceBarMetric, 1);
        }
    }

    public void PlayerDied()
    {
        TelemetryManager.instance.AccumulateMetric(_TimesDied, 1);
    }

    public void SetLevel1FinishTime(float finishtime)
    {
        TelemetryManager.instance.AddMetricSample<float>(_Level1FinishTime, finishtime);
    }
}
