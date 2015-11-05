using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using Windows.System.Display;

namespace WindowsPhoneApp
{
    public class Pedometer
    {
        private Accelerometer accelerometer;
        private bool started = false;
        private bool stepping = false;
        private int steps;

        public event EventHandler<int> Stepped;

        public int Steps { get { return steps; } }

        public Pedometer(Accelerometer accelerometer)
        {
            this.accelerometer = accelerometer;
        }

        public void Start()
        {
            if (started) return;
            started = true;
            accelerometer.ReadingChanged += accelerometer_ReadingChanged;
        }

        public void Stop()
        {
            if (!started) return;
            started = false;
            accelerometer.ReadingChanged -= accelerometer_ReadingChanged;
        }

        void accelerometer_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            double magnitude = Math.Sqrt(
                args.Reading.AccelerationX * args.Reading.AccelerationX +
                args.Reading.AccelerationY * args.Reading.AccelerationY +
                args.Reading.AccelerationZ * args.Reading.AccelerationZ);

            if (magnitude > 1.01 && !stepping)
            {
                stepping = true;
                steps += 1;

                // Prevent a race condition of the last subscriber unsubscribing
                // before we get to the null check.
                EventHandler<int> handler = Stepped;

                // Event will be null if there are no subscribers 
                if (handler != null)
                {
                    handler(this, steps);
                }
            }
            else if (magnitude < 1.0)
            {
                stepping = false;
            }
        }
    }
}
