using System;

namespace Immowert4You.Domain.Properties
{
    public class PropertyDto
    {
        public string Id { get; set; }
        public bool IsAgreed { get; set; }
        public bool IsEstimated { get; set; }
        public bool IsClosed { get; set; }
        public bool IsDenied { get; set; }
        public bool HasContract { get; set; }
        public bool HasMet { get; set; }
        public PropertyType Type { get; set; }
        public bool IsNotParcel => Type != PropertyType.Parcel;
        public bool IsUrgent { get; set; }
        public string SubCategory { get; set; }
        public float Size { get; set; }
        public float LivingSurface { get; set; }
        public int YearOfBuilt { get; set; }
        public bool HasBasement { get; set; }
        public bool HasLift { get; set; }
        public int Floors { get; set; }
        public string FloorsString => $"Stockwerke: {Floors}";
        public int Floor { get; set; }
        public int Rooms { get; set; }
        public int ConditionRate { get; set; }
        public int IntentionToSell { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public bool HasBalcony { get; set; }
        public bool HasFloors => Floors > 0;
        public bool HasLoggia { get; set; }
        public bool HasGarden { get; set; }
        public bool HasCarport { get; set; }
        public bool HasGarage { get; set; }
        public bool HasParking { get; set; }
        public bool HasBasementCloset { get; set; }
        public bool HasEnergyCertificate { get; set; }
        public string HeatingSystem { get; set; } = "nicht gegeben";
        public bool IsOpenOffert { get; set; }
        public bool IsObsolete { get; set; }
        public float MonthlyCosts { get; set; }
        public bool HasMonthlyCosts => MonthlyCosts > 0;
        public string HeatingCosts { get; set; }
        public string Extras { get; set; }
        public float Price { get; set; }
        public string[] Photos { get; set; }
        public virtual DateTime? DisplayedToBroker { get; set; }
        public virtual DateTime? RemoveFromBrokerAt { get; set; }
        public string CompanyName { get; set; }
        public string Logo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }
    }
}
