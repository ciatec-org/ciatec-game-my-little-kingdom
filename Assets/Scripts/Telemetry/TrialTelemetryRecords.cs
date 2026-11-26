using System;
using System.Collections.Generic;

namespace MeuPequenoReino.Telemetry
{
    [Serializable]
    public sealed class TelemetrySessionRecord
    {
        public string schema_version = "1.0";
        public string session_id;
        public string game_version;
        public string started_at_utc;
        public List<TelemetryTrialRecord> trials = new List<TelemetryTrialRecord>();
    }

    [Serializable]
    public sealed class TelemetryTrialRecord
    {
        public string trial_id;
        public int trial_index;
        public string building_id;
        public int model_id;
        public string condition;
        public float rotation_angle_degrees;
        public string rotation_axis;
        public string expected_answer;
        public List<TelemetryAttemptRecord> attempts = new List<TelemetryAttemptRecord>();
        public long paused_duration_ms;
        public string status;
    }

    [Serializable]
    public sealed class TelemetryAttemptRecord
    {
        public int attempt_index;
        public string player_answer;
        public bool is_correct;
        public long response_time_ms;
    }
}
