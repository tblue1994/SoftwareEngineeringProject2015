using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace WindowsPhoneApp.Models
{
    public class Path
    {
        public long Id { get; set; }

        public long ActivityId { get; set; }

        public List<byte> Data { get; set; }

        public Path()
        {
            Data = new List<byte>();
        }

        public void Update(double latitude, double longitude)
        {
            Data.AddRange(BitConverter.GetBytes(latitude));
            Data.AddRange(BitConverter.GetBytes(longitude));
        }

        public int Count
        {
            get
            {
                return Data.Count / 16;
            }
        }

        public double Distance
        {
            get
            {
                if (Count < 2)
                {
                    return 0.0;
                }

                double accumulator = 0.0;
                double previousLatitude = this[0].Latitude;
                double previousLongitude = this[0].Longitude;
                for (int i = 1; i < Count; i += 1)
                {
                    double latitude = this[i].Latitude;
                    double longitude = this[i].Longitude;

                    double deltaLat = (latitude - previousLatitude);
                    double deltaLong = (longitude - previousLongitude);
                    double miLat = deltaLat * (60.0 * 1852.0 / 1609.0);
                    double miLong = deltaLong * (60.0 * 1852.0 / 1609.0);

                    accumulator += Math.Sqrt(miLat * miLat + miLong * miLong);

                    previousLatitude = latitude;
                    previousLongitude = longitude;
                }
                return Math.Round(accumulator, 2);
            }
        }

        public BasicGeoposition this[int i]
        {
            get
            {
                return new BasicGeoposition
                {
                    Latitude = BitConverter.ToDouble(Data.GetRange(i * 16, 8).ToArray(), 0),
                    Longitude = BitConverter.ToDouble(Data.GetRange(i * 16 + 8, 8).ToArray(), 0)
                };
            }
            /*set
            {
                var latitude = BitConverter.GetBytes(value.Latitude);
                var longitude = BitConverter.GetBytes(value.Longitude);
                for (int j = 0; j < 8; ++j)
                {
                    Data[i * 8 + j] = latitude[j];
                    Data[i * 8 + 8 + j] = longitude[j];
                }
            }*/
        }

        public IEnumerable<BasicGeoposition> Coordinates
        {
            get
            {
                for (int i = 0; i < Count; i += 2)
                {
                    yield return this[i];
                }
            }
        }

        public BasicGeoposition Last()
        {
            return this[Count - 1];
        }
    }
}
