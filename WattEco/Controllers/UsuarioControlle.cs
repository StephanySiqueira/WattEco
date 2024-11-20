using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WattEco.DTOs;
using WattEco.Models;
using WattEco.Services;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetAllUsuarios()
    {
        return await ExecuteAsync(async () =>
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            if (usuarios == null || !usuarios.Any())
            {
                return Ok(new { Message = "Nenhum usuário encontrado." });
            }
            return Ok(usuarios);
        });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetUsuarioById(int id)
    {
        return await ExecuteAsync(async () =>
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound(new ErrorResponse { Message = "Usuário não encontrado." });
            }
            return Ok(usuario);
        });
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> CreateUsuario(UsuarioDTO usuarioDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = "Dados inválidos fornecidos." });
        }

        return await ExecuteAsync(async () =>
        {
            await _usuarioService.AddUsuarioAsync(usuarioDTO);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuarioDTO.Id },
                new { Message = "Usuário criado com sucesso.", Usuario = usuarioDTO });
        });
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> UpdateUsuario(int id, UsuarioDTO usuarioDTO)
    {
        if (id != usuarioDTO.Id)
        {
            return BadRequest(new ErrorResponse { Message = "ID do usuário não corresponde." });
        }

        return await ExecuteAsync(async () =>
        {
            var existingUsuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (existingUsuario == null)
            {
                return NotFound(new ErrorResponse { Message = "Usuário não encontrado." });
            }

            await _usuarioService.UpdateUsuarioAsync(usuarioDTO);
            return Ok(new { Message = "Usuário atualizado com sucesso.", Usuario = usuarioDTO });
        });
    }


    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        return await ExecuteAsync(async () =>
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound(new ErrorResponse { Message = "Usuário não encontrado." });
            }

            await _usuarioService.DeleteUsuarioAsync(id);
            return Ok(new { Message = "Usuário deletado com sucesso." });
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
