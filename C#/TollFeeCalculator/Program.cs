// See https://aka.ms/new-console-template for more information
using TollFeeCalculator;

Console.WriteLine($"----------------------------------");
Console.WriteLine($"| Welcome to the Toll Calculator |");
Console.WriteLine($"----------------------------------\n");

var calc = new TollCalculator();
var car = new Car();
var passingDateTime = DateTime.Now;
int tollFee = calc.GetTollFee(passingDateTime, car);

Console.WriteLine($"Vehicle: {car.GetVehicleType()}");
Console.WriteLine($"Passing date: {passingDateTime.ToLongDateString()} - {passingDateTime.ToShortTimeString()}");
Console.WriteLine($"Calculated fees: {tollFee}");

var motorbike = new Motorbike();
var bikeTollFee = calc.GetTollFee(passingDateTime, motorbike);

Console.WriteLine($"-------------------\n");
Console.WriteLine($"Vehicle: {motorbike.GetVehicleType()}");
Console.WriteLine($"Passing date: {passingDateTime.ToLongDateString()} - {passingDateTime.ToShortTimeString()}");
Console.WriteLine($"Calculated fees: {bikeTollFee}\n");

