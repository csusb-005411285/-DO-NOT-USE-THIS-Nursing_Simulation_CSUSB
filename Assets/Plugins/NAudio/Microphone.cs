using System;
using NAudio.Wave;

namespace microphone
{
    public class Microphone
    {
        public WaveIn waveSource = null;
        public WaveFileWriter waveFile = null;

        private void StartRecording()
        {
            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat(44100, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            //TODO temp folder? maybe a static class for getting temp file storage location
            waveFile = new WaveFileWriter(@"micInput.wav", waveSource.WaveFormat);

            waveSource.StartRecording();
        }

        private void StopRecording()
        {
            waveSource.StopRecording();
            waveFile.Close();
        }

        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
        }
    }
}
