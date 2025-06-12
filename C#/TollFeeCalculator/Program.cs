// See https://aka.ms/new-console-template for more information
using TollFeeCalculator;

var calc = new TollCalculator();

var car = new Car();
var accountingDateTime = new DateTime(2013, 5, 10, 6, 30, 0);
int tollFee = calc.GetTollFee(accountingDateTime, car);

Console.WriteLine($"Vehicle: {car.GetVehicleType()}");
Console.WriteLine($"Passing date: {accountingDateTime.ToLongDateString()} - {accountingDateTime.ToShortTimeString()}");
Console.WriteLine($"Calculated fees: {tollFee}");

var motorbike = new Motorbike();
var bikeTollFee = calc.GetTollFee(accountingDateTime, motorbike);

Console.WriteLine($"--------------------");
Console.WriteLine($"Vehicle: {motorbike.GetVehicleType()}");
Console.WriteLine($"Passing date: {accountingDateTime.ToLongDateString()} - {accountingDateTime.ToShortTimeString()}");
Console.WriteLine($"Calculated fees: {bikeTollFee}");

