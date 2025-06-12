namespace TollFeeCalculator;

public class TollCalculator
{

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTollFee(DateTime[] dates, IVehicle vehicle)
    {
        DateTime intervalStart = dates[0];
        int highestFeeInHour = GetTollFee(intervalStart, vehicle);
        int totalFee = 0;

        foreach (DateTime nextDate in dates)
        {
            int nextFee = GetTollFee(nextDate, vehicle);

            double diffInMinutes = (nextDate - intervalStart).TotalMinutes;

            if (diffInMinutes <= 60)
            {
                if (totalFee > 0) totalFee -= highestFeeInHour;
                if (nextFee >= highestFeeInHour) highestFeeInHour = nextFee;
                totalFee += highestFeeInHour;
            }
            else
            {
                intervalStart = nextDate;
                highestFeeInHour = nextFee;
                totalFee += highestFeeInHour;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle == null) return false;
        String vehicleType = vehicle.GetVehicleType();        
        return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }

    public int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute <= 29) return 8;
        if (hour == 6 && minute >= 30) return 13;
        if (hour == 7) return 18;
        if (hour == 8 && minute <= 29) return 13;    
        if ((hour == 8 && minute >= 30) || (hour >= 9 && hour <= 14)) return 8;
        if (hour == 15 && minute <= 29) return 13;
        if ((hour == 15 && minute >= 30) || hour == 16) return 18;
        if (hour == 17) return 13;
        if (hour == 18 && minute <= 29) return 8;

        return 0;
    }

    private bool IsTollFreeDate(DateTime date)
    {
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        //If month is July then set day to zero for lookup in the hashset
        if (month == 7) day = 0;

        var holidays = new HashSet<(int month, int day)>
        {
            (1,1),
            (3,28),
            (3,29),
            (4,1),
            (4,30),
            (5,1),
            (5,8),
            (5,9),
            (6,5),
            (6,6),
            (6,21),
            (7,0),
            (11,1),
            (12,24),
            (12,25),
            (12,26),
            (12,31)
        };

        return holidays.Contains((month, day));
    }
}