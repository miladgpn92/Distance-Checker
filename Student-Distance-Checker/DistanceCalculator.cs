namespace DistanceChecker
{
    public class DistanceCalculator
    {
        public static double Calculate(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Earth's radius in km
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Asin(Math.Sqrt(a));
            return R * c;
        }

        private static double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static List<LatLng> Grouping(List<LatLng> list)
        {


            var orderedList = new List<LatLng>();

            LatLng currentlnglat = list.FirstOrDefault();
            int order = 1;
            while (list.Count > 4)
            {

                try
                {
                    List<LatLng> tempOderedList = Ordering(list, currentlnglat, order);
                    order++;
                    orderedList.Add(tempOderedList.OrderByDescending(item => item.distance).ToList()[0]);
                    orderedList.Add(tempOderedList.OrderByDescending(item => item.distance).ToList()[1]);
                    orderedList.Add(tempOderedList.OrderByDescending(item => item.distance).ToList()[2]);
                    orderedList.Add(tempOderedList.OrderByDescending(item => item.distance).ToList()[3]);
                    currentlnglat = tempOderedList.OrderByDescending(item => item.distance).ToList()[4];

                    list.Remove(tempOderedList.OrderByDescending(item => item.distance).ToList()[0]);
                    list.Remove(tempOderedList.OrderByDescending(item => item.distance).ToList()[1]);
                    list.Remove(tempOderedList.OrderByDescending(item => item.distance).ToList()[2]);
                    list.Remove(tempOderedList.OrderByDescending(item => item.distance).ToList()[3]);
                    list.Remove(tempOderedList.OrderByDescending(item => item.distance).ToList()[5]);
                    currentlnglat = tempOderedList.OrderByDescending(item => item.distance).ToList()[4];
                }
                catch (Exception)
                {

                   
                }
            
            }

            return orderedList;

        }
        public static List<LatLng> Ordering(List<LatLng> list, LatLng currentlnglat,int order)
        {
            var tempOderedList = new List<LatLng>();


            foreach (var item in list)
            {
                if (currentlnglat != item)
                {
                    item.distance = Calculate(currentlnglat.lat, currentlnglat.lng, item.lat, item.lng);
                    item.order = order;
                    tempOderedList.Add(item);

                }
            }
            return tempOderedList.OrderByDescending(item => item.distance).ToList();
        }

    }
}

public class LatLng
{
    public double lat { get; set; }
    public double lng { get; set; }
    public double distance { get; set; }

    public int order { get; set; }
}