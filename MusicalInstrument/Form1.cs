using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicalInstrument
{
    public partial class Form1 : Form
    {
        SignalGenerator Sine = new SignalGenerator()
        {
            Type = SignalGeneratorType.Sin,
            Gain = 0.2
        };

        WaveOutEvent player = new WaveOutEvent();
        private Point MousePressedPosition;
        private bool IsDown = false;

        public Form1()
        {
            InitializeComponent();

            player.Init(Sine);

            trackFrequency.ValueChanged += (s, e) => Sine.Frequency = trackFrequency.Value;
            trackFrequency.Value = 600;
            
            trackVolume.ValueChanged += (s, e) => player.Volume = trackVolume.Value == 0 ? player.Volume = 0 : player.Volume = trackVolume.Value / 100F;
            trackVolume.Value = 50;
        }

        private void TheMouseDown(object sender, MouseEventArgs e)
        {
            player.Play();
            IsDown = true;
            MousePressedPosition = e.Location;
        }

        private void TheMouseUp(object sender, MouseEventArgs e)
        {
            player.Stop();
            IsDown = false;
        }

        private void TrackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            var dX = e.X - MousePressedPosition.X;
            var vol = player.Volume + (dX / 500F);

            var dY = MousePressedPosition.Y - e.Y;
            var freq = Sine.Frequency + dY;

            if (IsDown)
            {
                player.Volume = (vol > 0) ? (vol < 1) ? vol: 1 : 0;
                Sine.Frequency = (freq > 100) ? (freq < 1000) ? freq : 1000 : 100;

                trackFrequency.Value = (int)Math.Round(Sine.Frequency);
                trackVolume.Value = (int)Math.Round(player.Volume * 100);
            }

            Text = $"Musical Instrument ({dX}, {dY}) ({vol}, {freq})";
        }
    }
}
