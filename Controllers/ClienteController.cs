using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.Controllers;
using RoleTopMVC.Enums;
using RoleTopMVC.Repositories;
using RoleTopMVC.ViewModels;

public class ClienteController : AbstractController {
    private AluguelRepository aluguelRepository = new AluguelRepository ();
    private ClienteRepository clienteRepository = new ClienteRepository ();

    [HttpGet]
    public IActionResult Login () {
        return View (new BaseViewModel () {
            NomeView = "Login",
                UsuarioEmail = ObterUsuarioSession (),
                UsuarioNome = ObterUsuarioNomeSession ()
        });
    }

    [HttpPost]

    public IActionResult Login (IFormCollection form) {
        try {
            System.Console.WriteLine ("==================");
            System.Console.WriteLine (form["cliente_email"]);
            System.Console.WriteLine (form["cliente_senha"]);
            System.Console.WriteLine ("==================");

            var usuario = form["cliente_email"];
            var senha = form["cliente_senha"];

            if (!string.IsNullOrEmpty (usuario) && !string.IsNullOrEmpty (senha)) {
                var cliente = clienteRepository.ObterPor (usuario);

                if (cliente != null) {
                    if (cliente.Senha.Equals (senha)) {
                        switch (cliente.TipoUsuario) {
                            case (uint) TiposUsuario.CLIENTE:
                                HttpContext.Session.SetString (SESSION_CLIENTE_EMAIL, usuario);
                                HttpContext.Session.SetString (SESSION_CLIENTE_NOME, cliente.Nome);
                                HttpContext.Session.SetString (SESSION_CLIENTE_TIPO, cliente.TipoUsuario.ToString ());

                                return RedirectToAction ("Usuario", "Cliente");

                            default:
                                HttpContext.Session.SetString (SESSION_CLIENTE_EMAIL, usuario);
                                HttpContext.Session.SetString (SESSION_CLIENTE_NOME, cliente.Nome);
                                HttpContext.Session.SetString (SESSION_CLIENTE_TIPO, cliente.TipoUsuario.ToString ());

                                return RedirectToAction ("Administrador", "Administrador");

                        }
                    } else {
                        return View ("Erro", new RespostaViewModel ("Senha incorreta"));
                    }

                } else {
                    return View ("Erro", new RespostaViewModel ($"Usuário {usuario} não encontrado"));
                }
            } else {
                return View ("Erro", new RespostaViewModel ("Não é possível logar sem seus dados!"));
            }

        } catch (Exception e) {
            System.Console.WriteLine (e.StackTrace);
            return View ("Erro");
        }
    }

    public IActionResult Logoff () {
        HttpContext.Session.Remove (SESSION_CLIENTE_EMAIL);
        HttpContext.Session.Remove (SESSION_CLIENTE_NOME);
        HttpContext.Session.Clear ();
        return RedirectToAction ("Index", "Home");
    }

    public IActionResult Pagamento () {
        return View (new BaseViewModel () {
            NomeView = "Pagamento",
                UsuarioEmail = ObterUsuarioSession (),
                UsuarioNome = ObterUsuarioNomeSession ()
        });
    }

     public IActionResult Usuario ()
        {
            var emailCliente = ObterUsuarioSession();
            var alugueisCliente = aluguelRepository.ObterTodosPorCliente(emailCliente);

            return View(new HistoricoViewModel()
            {
                formaPagamento = alugueisCliente,
                NomeView = "Usuario",
                UsuarioEmail = ObterUsuarioSession(),
                UsuarioNome = ObterUsuarioNomeSession()
            });
        }
}
