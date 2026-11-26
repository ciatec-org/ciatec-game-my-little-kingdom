using System;
using System.Globalization;

namespace MeuPequenoReino.Telemetry
{
    /// <summary>Records observations from one game session on the game thread.</summary>
    public sealed class TrialTelemetrySession
    {
        readonly TelemetrySessionRecord record;
        readonly TrialTelemetryJsonStore store;
        TelemetryTrialRecord activeTrial;
        double? pauseStarted;
        double pausedSeconds;
        bool ended, endSaved;

        public TrialTelemetrySession(string gameVersion, string persistentDataPath)
        {
            record = new TelemetrySessionRecord
            {
                session_id = Guid.NewGuid().ToString("N"),
                game_version = gameVersion,
                started_at_utc = DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture)
            };
            store = new TrialTelemetryJsonStore(persistentDataPath, record.session_id);
            store.Save(record);
        }

        public void BeginTrial(string buildingId, int modelId, bool structuralMatch, float rotation,
            string rotationAxis, double now)
        {
            if (ended)
                return;
            InterruptActiveTrial(now);
            pausedSeconds = 0;
            activeTrial = new TelemetryTrialRecord
            {
                trial_id = Guid.NewGuid().ToString("N"),
                trial_index = record.trials.Count + 1,
                building_id = buildingId,
                model_id = modelId,
                condition = structuralMatch ? "rotated" : "mirrored_rotated",
                rotation_angle_degrees = rotation,
                rotation_axis = rotationAxis,
                expected_answer = Answer(structuralMatch),
                status = "in_progress"
            };
            record.trials.Add(activeTrial);
            store.Save(record);
        }

        public void RecordAttempt(bool serves, bool correct, double elapsedSeconds)
        {
            if (activeTrial == null || pauseStarted.HasValue)
                return;
            activeTrial.attempts.Add(new TelemetryAttemptRecord
            {
                attempt_index = activeTrial.attempts.Count + 1,
                player_answer = Answer(serves),
                is_correct = correct,
                response_time_ms = Milliseconds(elapsedSeconds)
            });
            if (correct)
            {
                activeTrial.status = "completed";
                activeTrial = null;
            }
            store.Save(record);
        }

        public void PauseTrial(double now)
        {
            if (activeTrial != null && !pauseStarted.HasValue)
                pauseStarted = now;
        }

        public void ResumeTrial(double now)
        {
            if (!pauseStarted.HasValue)
                return;
            AccumulatePause(now);
            store.Save(record);
        }

        public void End(double now)
        {
            if (!ended)
            {
                InterruptActiveTrial(now);
                ended = true;
            }
            if (!endSaved)
                endSaved = store.Save(record);
        }

        void InterruptActiveTrial(double now)
        {
            if (activeTrial == null)
                return;
            AccumulatePause(now);
            activeTrial.status = "interrupted";
            activeTrial = null;
        }

        void AccumulatePause(double now)
        {
            if (!pauseStarted.HasValue)
                return;
            pausedSeconds += now - pauseStarted.Value;
            activeTrial.paused_duration_ms = Milliseconds(pausedSeconds);
            pauseStarted = null;
        }

        static string Answer(bool serves) => serves ? "SERVE" : "NAO_SERVE";

        static long Milliseconds(double seconds) => (long)Math.Round(seconds * 1000, MidpointRounding.AwayFromZero);
    }
}
