namespace RetroArcade.API.DTOs
{
    public record BuildingUpdateDTO(Guid BuildingId, string Name, TimeSpan OpeningHour,TimeSpan ClosingHour, 
        string AddressStreet, string AddressNumber, string PostalCode, string City, string Country);
}
