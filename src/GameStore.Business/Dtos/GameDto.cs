namespace GameStore.Business.Dtos;

/*
 A DTO is a contract between the client and server since it represents a shared agreement about how
 data will be transferred and used. Record is ideal to represent simple data that is going to be covered 
 over from one point to another without the need of modification. And they also reduce the amount of 
 boilerplate code. 
*/

public record GameDto(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
): Entity;