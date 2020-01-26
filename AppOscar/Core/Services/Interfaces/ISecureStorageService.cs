using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ISecureStorageService
    {
        /// <summary>
        /// Obtem um valor armazenado no SecureStorage.
        /// </summary>
        /// <param name="key">A chave onde está armazenado</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        /// <returns>O objeto encontrado ou Null</returns>
        Task<string> GetAsync(string key, CancellationToken ct = default);

        /// <summary>
        /// Armazena um valor no SecureStorage.
        /// </summary>
        /// <param name="key">A chave onde será armazenado</param>
        /// <param name="value">O valor a ser armazenado</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task SetAsync(string key, string value, CancellationToken ct = default);

        /// <summary>
        /// Obtem o AuthorizationToken.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        /// <returns>O AuthToken encontrado ou Null</returns>
        /// <remarks>Este método é um atalho para o método de GetAsync</remarks>
        Task<string> GetAuthTokenAsync(CancellationToken ct = default);

        /// <summary>
        /// Armazena um Auth Token.
        /// </summary>
        /// <param name="token">Token a ser armazenado</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task SetAuthTokenAsync(string token, CancellationToken ct = default);

        /// <summary>
        /// Obtem o RefreshToken.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        /// <returns>O RefreshToken encontrado ou Null</returns>
        /// <remarks>Este método é um atalho para o método de GetAsync</remarks>
        Task<string> GetRefreshTokenAsync(CancellationToken ct = default);

        /// <summary>
        /// Armazena um Refresh Token.
        /// </summary>
        /// <param name="token">Token a ser armazenado</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task SetRefreshTokenAsync(string token, CancellationToken ct = default);

        /// <summary>
        /// Obtem o IdentityToken.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        /// <returns>O IdentityToken encontrado ou Null</returns>
        /// <remarks>Este método é um atalho para o método de GetAsync</remarks>
        Task<string> GetIdentityTokenAsync(CancellationToken ct = default);

        /// <summary>
        /// Armazena o IdentityToken.
        /// </summary>
        /// <param name="token">Token a ser armazenado</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task SetIdentityTokenAsync(string token, CancellationToken ct = default);

        /// <summary>
        /// Remove um objeto do SecureStorage.
        /// </summary>
        /// <param name="key">Chave onde o objeto está armazenado</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task PurgeAsync(string key, CancellationToken ct = default);

        /// <summary>
        /// Remove os três tokens (Auth, Refresh, Identity) do SecureStorage.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        /// <remarks>Este método é um atalho para o método de PurgeAsync</remarks>
        Task PurgeTokensAsync(CancellationToken ct = default);

        /// <summary>
        /// Remove todos os objetos do SecureStorage.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task PurgeAllAsync(CancellationToken ct = default);
    }
}
