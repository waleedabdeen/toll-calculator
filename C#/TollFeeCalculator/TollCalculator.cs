namespace TollFeeCalculator;

public class TollCalculator
{
	const int MAXIMUM_DAILY_CHARGE = 60;
	const int L1_CHARGES = 8;
	const int L2_CHARGES = 13;
	const int L3_CHARGES = 18;

	/**
     * Calculate the total toll fee for one day
     *
     *
     * @param dates   - date and time of all passes on one day
     * @param vehicle - the vehicle
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
		return Math.Min(MAXIMUM_DAILY_CHARGE, totalFee);
	}

	public int GetTollFee(DateTime date, IVehicle vehicle)
	{
		if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

		int hour = date.Hour;
		int minute = date.Minute;

		if (hour == 6 && minute <= 29) return L1_CHARGES;
		if (hour == 6 && minute >= 30) return L2_CHARGES;
		if (hour == 7) return L3_CHARGES;
		if (hour == 8 && minute <= 29) return L2_CHARGES;
		if ((hour == 8 && minute >= 30) || (hour >= 9 && hour <= 14)) return L1_CHARGES;
		if (hour == 15 && minute <= 29) return L2_CHARGES;
		if ((hour == 15 && minute >= 30) || hour == 16) return L3_CHARGES;
		if (hour == 17) return L2_CHARGES;
		if (hour == 18 && minute <= 29) return L1_CHARGES;

		return 0;
	}

	private bool IsTollFreeVehicle(IVehicle vehicle)
	{
		if (vehicle == null) return false;
		string vehicleType = vehicle.GetVehicleType();

		if (vehicleType.Equals(TollPayingVehiclesEnum.Car.ToString()) ||
				vehicleType.Equals(TollPayingVehiclesEnum.Other.ToString()))
			return false;

		if (vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
				vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
				vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
				vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
				vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
				vehicleType.Equals(TollFreeVehicles.Military.ToString()))
			return true;

		//Log unknown vehicles to handle later
		Console.WriteLine($"Unknown vehicle type: {vehicleType}");
		return false;
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