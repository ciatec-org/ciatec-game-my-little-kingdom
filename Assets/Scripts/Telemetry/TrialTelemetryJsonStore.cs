using System;
using System.IO;
using System.Security;
using System.Text;
using UnityEngine;

namespace MeuPequenoReino.Telemetry
{
    sealed class TrialTelemetryJsonStore
    {
        readonly string persistentDataPath;
        readonly string fileName;
        bool published;

        public TrialTelemetryJsonStore(string persistentDataPath, string sessionId)
        {
            this.persistentDataPath = persistentDataPath;
            fileName = $"session_{sessionId}.json";
        }

        public bool Save(TelemetrySessionRecord record)
        {
            string temporaryPath = null;
            try
            {
                if (string.IsNullOrEmpty(persistentDataPath))
                    throw new NotSupportedException("Application.persistentDataPath está vazio.");
                string directory = Path.Combine(persistentDataPath, "Telemetry");
                Directory.CreateDirectory(directory);
                string path = Path.Combine(directory, fileName);
                string pendingPath = path + "." + Guid.NewGuid().ToString("N") + ".tmp";
                byte[] json = Encoding.UTF8.GetBytes(JsonUtility.ToJson(record, true) + "\n");
                using (var stream = new FileStream(pendingPath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {
                    temporaryPath = pendingPath;
                    stream.Write(json, 0, json.Length);
                    stream.Flush(true);
                }

                if (published && File.Exists(path))
                    File.Replace(temporaryPath, path, null);
                else
                    File.Move(temporaryPath, path);
                published = true;
                temporaryPath = null;
                return true;
            }
            catch (Exception error) when (IsStorageError(error))
            {
                Debug.LogError($"[Telemetry] Falha ao salvar {fileName}. {error}");
                return false;
            }
            finally
            {
                if (temporaryPath != null)
                {
                    try
                    {
                        File.Delete(temporaryPath);
                    }
                    catch (Exception error) when (IsStorageError(error))
                    {
                        Debug.LogError($"[Telemetry] Falha ao remover temporário de {fileName}. {error}");
                    }
                }
            }
        }

        static bool IsStorageError(Exception error) =>
            error is IOException || error is UnauthorizedAccessException ||
            error is NotSupportedException || error is SecurityException;
    }
}
