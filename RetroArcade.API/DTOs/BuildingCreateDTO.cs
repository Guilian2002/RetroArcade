namespace RetroArcade.API.DTOs
{
    public record BuildingCreateDTO(string Name, TimeSpan OpeningHour, TimeSpan ClosingHour,
        string AddressStreet, string AddressNumber, string PostalCode, string City, string Country);
}
