using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Organization.Domain.ValueObjects;

public class Address : ValueObject
{
    public string Street { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public string PostalCode { get; private set; } = string.Empty;
    public decimal? Latitude { get; private set; }
    public decimal? Longitude { get; private set; }

    private Address() { }

    public Address(
        string street,
        string city,
        string state,
        string country,
        string postalCode,
        decimal? latitude = null,
        decimal? longitude = null)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        PostalCode = postalCode;
        Latitude = latitude;
        Longitude = longitude;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return Country;
        yield return PostalCode;
        yield return Latitude;
        yield return Longitude;
    }

    public static Address Empty => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

    public Address Update(
        string? street = null,
        string? city = null,
        string? state = null,
        string? country = null,
        string? postalCode = null,
        decimal? latitude = null,
        decimal? longitude = null)
    {
        return new Address(
            street ?? Street,
            city ?? City,
            state ?? State,
            country ?? Country,
            postalCode ?? PostalCode,
            latitude ?? Latitude,
            longitude ?? Longitude);
    }
}
