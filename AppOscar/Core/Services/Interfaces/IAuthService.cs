using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IAuthService
    { /// <summary>
      /// Flag indicando se o App está autenticado.
      /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Tenta restaurar as credenciais armazenadas no App.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task RestoreCredentialsAsync(CancellationToken ct = default);

        /// <summary>
        /// Realiza o Login.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task LoginAsync(CancellationToken ct = default);

        /// <summary>
        /// Realiza o Logout.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task LogoutAsync(CancellationToken ct = default);

        /// <summary>
        /// Recupera o usuário autenticado.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        /// <returns>O usuário autenticado</returns>
        Task<User> GetUserAsync(CancellationToken ct = default);
    }
}
