namespace GameStore.Business.Dtos;

public abstract record Entity
{
    public Guid Id { get; init; } = Guid.NewGuid();
};