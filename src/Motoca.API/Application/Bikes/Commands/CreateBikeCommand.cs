namespace Motoca.API.Application.Bikes.Commands;

public record CreateBikeCommand(string Identificador, 
                                int Ano, 
                                string Modelo, 
                                string Placa) : IRequest<CreateBikeCommandResult>;