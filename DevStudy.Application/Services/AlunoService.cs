using AutoMapper;
using DevStudy.Application.DTOs;
using DevStudy.Application.Interfaces;
using DevStudy.Core.Models;
using DevStudy.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private IMapper _mapper;
        private ILogger<AlunoService> _logger;

        public AlunoService(IAlunoRepository alunoRepository, ILogger<AlunoService> logger, IMapper mapper)
        {
            _alunoRepository = alunoRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlunoDTO>> GetAlunos()
        {
            var alunosAll = await _alunoRepository.GetAlunos();
            
            if (!alunosAll.Any())
            {
                _logger.LogError("Não há alunos cadastrados");
                return null;
            }

            return _mapper.Map<IEnumerable<AlunoDTO>>(alunosAll);
        }

        public async Task<AlunoDTO> GetAluno(int id)
        {
            var buscarAlunoId = await _alunoRepository.GetAluno(id);

            if(buscarAlunoId == null)
            {
                _logger.LogError("Aluno não encontrado");
                return null;
            }

            return _mapper.Map<AlunoDTO>(buscarAlunoId);
        }

        public async Task<AlunoDTO> GetAlunoByEmail(string email)
        {
            var buscarAlunoEmail = await _alunoRepository.GetAlunoByEmail(email);

            if (buscarAlunoEmail == null)
            {
                _logger.LogError("Aluno não encontrado");
                return null;
            }

            return _mapper.Map<AlunoDTO>(buscarAlunoEmail);
        }

        public async Task<AlunoCreateDTO> AddAluno(AlunoCreateDTO aluno)
        {
            var alunoMapper = _mapper.Map<AlunoCreateDTO, Aluno>(aluno);

            var addAluno = await _alunoRepository.CreateAluno(alunoMapper);

            return _mapper.Map<AlunoCreateDTO>(addAluno);
        }

        public async Task<AlunoUpdateDTO> UpdateAluno(int id, AlunoUpdateDTO aluno)
        {
            var mapperAluno = _mapper.Map<AlunoUpdateDTO, Aluno>(aluno);

            var updateAluno = await _alunoRepository.UpdateAluno(id, mapperAluno);

            var updatedAlunoDTO = _mapper.Map<Aluno, AlunoUpdateDTO>(updateAluno);

            return updatedAlunoDTO;
        }

        public async Task<bool> DeleteAluno(int id)
        {
            var deleteAluno = await _alunoRepository.DeleteAluno(id);

            if(deleteAluno == false)
            {
                _logger.LogError("Aluno não encontrado");
                return false;
            } 
            return true;
        }   
    }
}
