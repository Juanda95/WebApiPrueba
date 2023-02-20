namespace Application.Feauties.Clientes.Commands.CreateClienteCommand
{
    using Application.Interfaces;
    using Application.Wrappers;
    using AutoMapper;
    using Domain.entities;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
  
    public class CreatePersonaCommand: IRequest<Response<int>>
    {
        public string? Nombre { get; set; }
        public string? Genero { get; set; }
        public int? Edad { get; set; }
        public string? Identificacion { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
    }

    public class CreateClienteCommand : CreatePersonaCommand, IRequest<Response<int>>
    {
        public string? Clienteid { get; set; }
        public string? Contrasena { get; set; }
        public bool? Estado { get; set; }
    }

    public class CreateClienteCommandHandlers : IRequestHandler<CreateClienteCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateClienteCommandHandlers(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<int>> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var nuevoCliente = _mapper.Map<Cliente>(request);
            var data = await _repositoryAsync.AddAsync(nuevoCliente);
            return new Response<int>(data.Id);
        }
    }
}
