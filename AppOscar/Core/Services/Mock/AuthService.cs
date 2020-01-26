using Bogus;
using Core.Models;
using Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services.Mock
{
    public class AuthService
    {
        private readonly Faker<User> _userFaker;
        private readonly ISecureStorageService _secureStorageService;
        private User _client;

        public AuthService(ISecureStorageService secureStorageService)
        {
            _secureStorageService = secureStorageService ?? throw new ArgumentNullException(nameof(secureStorageService));

            _userFaker = new Faker<User>("pt_BR")
                .RuleFor(c => c.nomeUsuario, f => f.Name.FullName())
                .RuleFor(p => p.emailUsuario, (f, p) => f.Internet.Email(firstName: p.nomeUsuario))
                .RuleFor(p => p.senhaUsuario, f => f.Internet.Password());
        }

        public bool IsAuthenticated => _client != null;

        public async Task<User> GetUserAsync(CancellationToken ct)
        {
            if (_client == null)
                await RestoreCredentialsAsync(ct);

            return _client;
        }

        public async Task LoginAsync(CancellationToken ct)
        {
            // Limpar os Tokens que temos armazenados
            await _secureStorageService.PurgeTokensAsync(ct);

            // Em um mundo ideal: Encaminhar para o Login, pegar os tokens de retorno e armazenar
            // Estamos em um Mock, logo vamos gerar isso aleatóriamente :shrug:

            _client = _userFaker.Generate();        // Gera um usuário Random
            await _secureStorageService.SetAuthTokenAsync(new Randomizer().Guid().ToString());
            await _secureStorageService.SetRefreshTokenAsync(new Randomizer().Guid().ToString());
            await _secureStorageService.SetIdentityTokenAsync(new Randomizer().Guid().ToString());
        }

        public async Task LogoutAsync(CancellationToken ct)
        {
            await _secureStorageService.PurgeTokensAsync(ct);
            _client = null;
        }

        public async Task RestoreCredentialsAsync(CancellationToken ct)
        {
            string authToken = await _secureStorageService.GetAuthTokenAsync(ct);
            if (!string.IsNullOrWhiteSpace(authToken))
            {
                // 1. Verificar se o token ainda é válido...
                // 2. Se não for, tente usar o Refresh
                string refreshToken = await _secureStorageService.GetRefreshTokenAsync(ct);
                // 3. Se não conseguir refreshar, definir o _client como null para garantir o fluxo normal, saindo deste metodo

                // 4. Tudo OK? Recupere a identidade do usuário
                string identityToken = await _secureStorageService.GetIdentityTokenAsync(ct);

                // Como estamos em um Mock, vamos simplesmente atribuir um cliente e meh :shrug:
                _client = _userFaker.Generate();        // Gera um usuário Random
            }
        }
    }
}
