using AutoMapper;
using BeeFor.Application.Models;
using BeeFor.Domain.Entities;

namespace BeeFor.Application.AutoMapper
{
    public class DomainToModelMapping : Profile
    {
        public DomainToModelMapping()
        {
            CreateMap<Projeto, ProjetoViewModel>().ReverseMap();
            CreateMap<Quadro, QuadroViewModel>().ReverseMap(); ;
            CreateMap<QuadroColuna, QuadroColunaViewModel>().ReverseMap(); ;
            CreateMap<QuadroColunaCard, QuadroColunaCardViewModel>().ReverseMap();           
            CreateMap<ConfiguracaoIntegracao, ConfiguracaoIntegracaoViewModel>().ReverseMap();
        }
    }
}
