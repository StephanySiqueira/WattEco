using WattEco.DTOs;
using WattEco.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WattEco.Models;

namespace WattEco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecompensaController : ControllerBase
    {
        private readonly IRecompensaService _recompensaService;

        public RecompensaController(IRecompensaService recompensaService)
        {
            _recompensaService = recompensaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RecompensaDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetAllRecompensas()
        {
            return await ExecuteAsync(async () =>
            {
                var recompensas = await _recompensaService.GetAllRecompensasAsync();
                if (recompensas == null || !recompensas.Any())
                {
                    return NotFound(new ErrorResponse { Message = "Nenhuma recompensa encontrada." });
                }
                return Ok(recompensas);
            });
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RecompensaDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetRecompensaById(int id)
        {
            return await ExecuteAsync(async () =>
            {
                var recompensa = await _recompensaService.GetRecompensaByIdAsync(id);
                if (recompensa == null)
                {
                    return NotFound(new ErrorResponse { Message = "Recompensa não encontrada." });
                }
                return Ok(recompensa);
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RecompensaDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateRecompensa(RecompensaDTO recompensaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await ExecuteAsync(async () =>
            {
                await _recompensaService.AddRecompensaAsync(recompensaDTO);
                return CreatedAtAction(nameof(GetRecompensaById), new { id = recompensaDTO.Id }, new
                {
                    Message = "Recompensa criada com sucesso.",
                    Recompensa = recompensaDTO
                });
            });
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RecompensaDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateRecompensa(int id, RecompensaDTO recompensaDTO)
        {
            if (id != recompensaDTO.Id)
            {
                return BadRequest(new ErrorResponse { Message = "ID da recompensa não corresponde." });
            }

            return await ExecuteAsync(async () =>
            {
                var recompensaExistente = await _recompensaService.GetRecompensaByIdAsync(id);
                if (recompensaExistente == null)
                {
                    return NotFound(new ErrorResponse { Message = "Recompensa não encontrada." });
                }

                await _recompensaService.UpdateRecompensaAsync(recompensaDTO);
                return Ok(new
                {
                    Message = "Recompensa atualizada com sucesso.",
                    Recompensa = recompensaDTO
                });
            });
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteRecompensa(int id)
        {
            return await ExecuteAsync(async () =>
            {
                var recompensaExistente = await _recompensaService.GetRecompensaByIdAsync(id);
                if (recompensaExistente == null)
                {
                    return NotFound(new ErrorResponse { Message = "Recompensa não encontrada." });
                }

                await _recompensaService.DeleteRecompensaAsync(id);
                return Ok(new { Message = "Recompensa deletada com sucesso." });
            });
        }

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
