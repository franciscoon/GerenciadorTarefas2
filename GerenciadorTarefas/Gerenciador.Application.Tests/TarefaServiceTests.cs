using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Gerenciador.Application.Dto;
using Gerenciador.Application.Interface;
using Gerenciador.Application.Service;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interface;
using Moq;

namespace Gerenciador.Application.Tests
{
    public class TarefaServiceTests
    {
        private readonly Mock<IBaseRepository<Tarefa>> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TarefaService _service;

        public TarefaServiceTests()
        {
            _mockRepo = new Mock<IBaseRepository<Tarefa>>();
            _mockMapper = new Mock<IMapper>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateTarefaDto, Tarefa>();
                cfg.CreateMap<UpdateTarefaDto, Tarefa>();
                cfg.CreateMap<Tarefa, TarefaDto>();
            });

            _mapper = config.CreateMapper();
            _service = new TarefaService(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task CreateAsync_DeveLancarExcecao_SeDataConclusaoForAnteriorHoje()
        {
            var dto = new CreateTarefaDto
            {
                Titulo = "Tarefa Teste",
                DataConclusao = DateTime.Now.AddDays(-1)
            };

            Func<Task> act = async () => await _service.CreateAsync(dto);

            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("A data de conclusão não pode ser anterior à data de criação.");
        }

        [Fact]
        public async Task CreateAsync_DeveLancarExcecao_SeTituloForMaiorQue100()
        {
            var dto = new CreateTarefaDto
            {
                Titulo = new string('a', 101),
                DataConclusao = DateTime.Now.AddDays(1)
            };

            Func<Task> act = async () => await _service.CreateAsync(dto);

            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("O título deve ter no máximo 100 caracteres");
        }

        [Fact]
        public async Task CreateAsync_DeveChamarRepositorio_SeDadosValidos()
        {
            var dto = new CreateTarefaDto
            {
                Titulo = "Tarefa Válida",
                DataConclusao = DateTime.Now.AddDays(1)
            };

            var tarefa = new Tarefa { Titulo = dto.Titulo, DataConclusao = dto.DataConclusao };

            _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Tarefa>()))
                .ReturnsAsync(tarefa);

            var result = await _service.CreateAsync(dto);

            result.Should().NotBeNull();
            result.Titulo.Should().Be(dto.Titulo);

            _mockRepo.Verify(r => r.CreateAsync(It.IsAny<Tarefa>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarTarefa_SeDadosValidos()
        {
            int id = 1;

            var dto = new UpdateTarefaDto
            {
                Titulo = "Tarefa Atualizada",
                DataConclusao = DateTime.Now.AddDays(2)
            };

            var tarefaExistente = new Tarefa
            {
                Id = id,
                Titulo = "Tarefa Antiga",
                DataConclusao = DateTime.Now.AddDays(1)
            };

            _mockRepo.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(tarefaExistente);

            _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Tarefa>()))
                .Returns(Task.CompletedTask);

            _mockMapper.Setup(m => m.Map(dto, tarefaExistente))
                .Callback<UpdateTarefaDto, Tarefa>((src, dest) =>
                {
                    dest.Titulo = src.Titulo;
                    dest.DataConclusao = src.DataConclusao;
                });

            await _service.UpdateAsync(id, dto);

            tarefaExistente.Titulo.Should().Be(dto.Titulo);
            tarefaExistente.DataConclusao.Should().Be(dto.DataConclusao);

            _mockRepo.Verify(r => r.UpdateAsync(It.Is<Tarefa>(t => t.Titulo == dto.Titulo)), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarTarefas()
        {
            var listaTarefas = new List<Tarefa>
            {
                new Tarefa { Id = 1, Titulo = "Tarefa 1", DataConclusao = DateTime.Now.AddDays(1) },
                new Tarefa { Id = 2, Titulo = "Tarefa 2", DataConclusao = DateTime.Now.AddDays(2) }
            };

            _mockRepo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(listaTarefas);

            var result = await _service.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().Titulo.Should().Be("Tarefa 1");
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarTarefa_SeTarefaExistir()
        {
            var tarefa = new Tarefa { Id = 1, Titulo = "Tarefa Teste", DataConclusao = DateTime.Now.AddDays(1) };

            _mockRepo.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(tarefa);

            var result = await _service.GetByIdAsync(1);

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Titulo.Should().Be("Tarefa Teste");
        }

        [Fact]
        public async Task DeleteAsync_DeveDeletarTarefa_SeTarefaExistir()
        {
            var tarefa = new Tarefa { Id = 1, Titulo = "Tarefa Teste", DataConclusao = DateTime.Now.AddDays(1) };

            _mockRepo.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(tarefa);

            _mockRepo.Setup(r => r.DeleteAsync(It.IsAny<Tarefa>()))
                .Returns(Task.CompletedTask);

            await _service.DeleteAsync(1);

            _mockRepo.Verify(r => r.DeleteAsync(It.Is<Tarefa>(t => t.Id == 1)), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_DeveLancarExcecao_SeTarefaNaoExistir()
        {
            var idInvalido = 999;

            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await _service.GetByIdAsync(idInvalido));

            Assert.Equal("Objeto não encontrado", exception.Message);
        }


    }
}
