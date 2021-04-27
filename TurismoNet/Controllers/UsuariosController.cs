using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TurismoNet.Models;

namespace TurismoNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDBContext context;

        public UserManager<ApplicationUser> UserManager { get; }

        public UsuariosController(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.UserManager = userManager;
        }

        [HttpPost("AsignarUsuarioRol")]
        public async Task<ActionResult> AsignarRolUsuario(EditarRolDTO editarRolDTO)
        {
            var usuario = await UserManager.FindByIdAsync(editarRolDTO.UserId);
            if (usuario == null) { return NotFound(); }
            await UserManager.AddClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRolDTO.RoleName));
            await UserManager.AddToRoleAsync(usuario, editarRolDTO.RoleName);
            return Ok();
        }

        [HttpPost("RemoverUsuarioRol")]
        public async Task<ActionResult> RemoverRolUsuario(EditarRolDTO editarRolDTO)
        {
            var usuario = await UserManager.FindByIdAsync(editarRolDTO.UserId);
            if (usuario == null) { return NotFound(); }
            await UserManager.RemoveClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRolDTO.RoleName));
            await UserManager.RemoveFromRoleAsync(usuario, editarRolDTO.RoleName);
            return Ok();
        }
    }
}
