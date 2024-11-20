using WattEco.DTOs;
using WattEco.Models;
using WattEco.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WattEco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissaoController : ControllerBase
    {
        private readonly IMissaoService _missaoService;

        public MissaoController(IMissaoService missaoService)
        {
            _missaoService = missaoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MissaoDTO>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetAllMissoes()
        {
            return await ExecuteAsync(async () =>
            {
                var missoes = await _missaoService.GetAllMissoesAsync();
                if (missoes == null || !missoes.Any())
                {
                    return Ok(new { Message = "Nenhuma missão encontrada." });
                }
                return Ok(missoes);
            });
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MissaoDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetMissaoById(int id)
        {
            return await ExecuteAsync(async () =>
            {
                var missao = await _missaoService.GetMissaoByIdAsync(id);
                if (missao == null)
                {
                    return NotFound(new ErrorResponse { Message = "Missão não encontrada." });
                }
                return Ok(missao);
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MissaoDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateMissao(MissaoDTO missaoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await ExecuteAsync(async () =>
            {
                await _missaoService.AddMissaoAsync(missaoDTO);
                return CreatedAtAction(nameof(GetMissaoById), new { id = missaoDTO.Id }, new { Message = "Missão criada com sucesso.", Missao = missaoDTO });
            });
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateMissao(int id, MissaoDTO missaoDTO)
        {
            if (missaoDTO.UsuarioId == 0)
            {
                return BadRequest(new ErrorResponse { Message = "O campo UsuarioId é obrigatório." });
            }

            if (id != missaoDTO.Id)
            {
                return BadRequest(new ErrorResponse { Message = "ID da missão não corresponde." });
            }

            return await ExecuteAsync(async () =>
            {
                var missaoExistente = await _missaoService.GetMissaoByIdAsync(id);
                if (missaoExistente == null)
                {
                    return NotFound(new ErrorResponse { Message = "Missão não encontrada." });
                }

                await _missaoService.UpdateMissaoAsync(missaoDTO);
                return Ok(new { Message = "Missão atualizada com sucesso.", Missao = missaoDTO });
            });
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteMissao(int id)
        {
            return await ExecuteAsync(async () =>
            {
                var missaoExistente = await _missaoService.GetMissaoByIdAsync(id);
                if (missaoExistente == null)
                {
                    return NotFound(new ErrorResponse { Message = "Missão não encontrada." });
                }

                await _missaoService.DeleteMissaoAsync(id);
                return Ok(new { Message = "Missão deletada com sucesso." });
            });
        }


        // Método auxiliar para tratamento de exceções
        private async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Erro interno do servidor." });
            }
        }
    }
}
