using Ddd.Infrastructure;
using System;
using System.Globalization;
using System.Linq;

namespace Ddd.Taxi.Domain
{
    public class DriversRepository
    {
        public static Driver GetDriver(int driverId)
        {
            if (driverId == 15)
            {
                return new Driver(driverId, new PersonName("Drive", "Driverson"),
                    new Car("Lada sedan", "Baklazhan", "A123BT 66"));
            }
            throw new Exception("Unknown driver id " + driverId);
        }
    }

    public class Driver : Entity<int>
    {
        public int Id { get; }
        public PersonName DriverName { get; }
        public Car Car { get; }

        public Driver(int id, PersonName driverName, Car car) : base(id)
        {
            Id = id;
            DriverName = driverName;
            Car = car;
        }
    }

    public class Car : ValueType<Car>
    {
        public string Model { get; }
        public string Color { get; }
        public string PlateNumber { get; }

        public Car(string model, string color, string plateNumber)
        {
            Model = model;
            Color = color;
            PlateNumber = plateNumber;
        }
    }

    public class TaxiApi : ITaxiApi<TaxiOrder>
    {
        private readonly DriversRepository _DriversRepo;
        private readonly Func<DateTime> _CurrentTime;
        private int _IdCounter;

        public TaxiApi(DriversRepository driversRepo, Func<DateTime> currentTime)
        {
            _DriversRepo = driversRepo;
            _CurrentTime = currentTime;
        }

        public TaxiOrder CreateOrderWithoutDestination(string firstName, string lastName, string street, string building) =>
            new TaxiOrder(_IdCounter++, new PersonName(firstName, lastName), new Address(street, building),
                _CurrentTime());

        public void UpdateDestination(TaxiOrder order, string street, string building) =>
            order.UpdateDestination(new Address(street, building));

        public void AssignDriver(TaxiOrder order, int driverId) =>
            order.AssignDriver(driverId, _DriversRepo, _CurrentTime());

        public void UnassignDriver(TaxiOrder order) => order.UnassignDriver();

        public void Cancel(TaxiOrder order) => order.Cancel(_CurrentTime());

        public void StartRide(TaxiOrder order) => order.StartRide(_CurrentTime());

        public void FinishRide(TaxiOrder order) => order.FinishRide(_CurrentTime());

        public string GetDriverFullInfo(TaxiOrder order)
        {
            if (order.Status == TaxiOrderStatus.WaitingForDriver) return null;
            return string.Join(" ",
                "Id: " + order.Driver.Id,
                "DriverName: " + FormatName(order.Driver.DriverName.FirstName, order.Driver.DriverName.LastName),
                "Color: " + order.Driver.Car.Color,
                "CarModel: " + order.Driver.Car.Model,
                "PlateNumber: " + order.Driver.Car.PlateNumber);
        }

        public string GetShortOrderInfo(TaxiOrder order)
        {
            return string.Join(" ",
                "OrderId: " + order.Id,
                "Status: " + order.Status,
                "Client: " + FormatName(order.ClientName.FirstName, order.ClientName.LastName),
                "Driver: " + FormatName(order.Driver?.DriverName.FirstName, order.Driver?.DriverName.LastName),
                "From: " + FormatAddress(order.Start.Street, order.Start.Building),
                "To: " + FormatAddress(order.Destination?.Street, order.Destination?.Building),
                "LastProgressTime: " + GetLastProgressTime(order).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
        }

        private static string FormatName(string firstName, string lastName) =>
            string.Join(" ", new[] { firstName, lastName }.Where(n => n != null));

        private static string FormatAddress(string street, string building) =>
            string.Join(" ", new[] { street, building }.Where(n => n != null));
        private static DateTime GetLastProgressTime(TaxiOrder order)
        {
            switch (order.Status)
            {
                case TaxiOrderStatus.WaitingForDriver:
                    return order.CreationTime;
                case TaxiOrderStatus.WaitingCarArrival:
                    return order.DriverAssignmentTime;
                case TaxiOrderStatus.InProgress:
                    return order.StartRideTime;
                case TaxiOrderStatus.Finished:
                    return order.FinishRideTime;
                case TaxiOrderStatus.Canceled:
                    return order.CancelTime;
                default:
                    throw new NotSupportedException(order.Status.ToString());
            }
        }
    }

    public class TaxiOrder : Entity<int>
    {
        public int Id { get; }
        public PersonName ClientName { get; private set; }
        public Address Start { get; private set; }
        public Address Destination { get; private set; }
        public Driver Driver { get; private set; }
        public TaxiOrderStatus Status { get; private set; }
        public DateTime CreationTime { get; private set; }
        public DateTime DriverAssignmentTime { get; private set; }
        public DateTime CancelTime { get; private set; }
        public DateTime StartRideTime { get; private set; }
        public DateTime FinishRideTime { get; private set; }
        public TaxiOrder(int id, PersonName clientName, Address star, DateTime dateTime) : base(id)
        {
            ClientName = clientName;
            Start = star;
            CreationTime = dateTime;
        }
        public void UpdateDestination(Address destination) =>
            Destination = destination;

        public void AssignDriver(int driverId, DriversRepository repository, DateTime dateTime)
        {
            if (Driver != null) throw new InvalidOperationException();
            Driver = DriversRepository.GetDriver(driverId);
            DriverAssignmentTime = dateTime;
            Status = TaxiOrderStatus.WaitingCarArrival;
        }
        public void UnassignDriver()
        {
            if (Driver == null || Status == TaxiOrderStatus.InProgress) throw new InvalidOperationException(TaxiOrderStatus.WaitingForDriver.ToString());
            Driver = null;
            Status = TaxiOrderStatus.WaitingForDriver;
        }
        public void Cancel(DateTime dateTime)
        {
            if (Status == TaxiOrderStatus.InProgress)
                throw new InvalidOperationException();
            Status = TaxiOrderStatus.Canceled;
            CancelTime = dateTime;
        }

        public void StartRide(DateTime dateTime)
        {
            if (Driver == null) throw new InvalidOperationException();
            Status = TaxiOrderStatus.InProgress;
            StartRideTime = dateTime;
        }

        public void FinishRide(DateTime dateTime)
        {
            if (Status != TaxiOrderStatus.InProgress || Driver == null) throw new InvalidOperationException();
            Status = TaxiOrderStatus.Finished;
            FinishRideTime = dateTime;
        }
    }
}