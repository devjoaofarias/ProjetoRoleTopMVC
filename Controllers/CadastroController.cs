using System;
using RoleTopMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.Controllers;
using RoleTopMVC.Enums;
using RoleTopMVC.Models;
using RoleTopMVC.Repositories;


public class CadastroController : AbstractController {
    ClienteRepository clienteRepository = new ClienteRepository ();
    public IActionResult Cadastro () {
        return View (new BaseViewModel () {
                NomeView = "Cadastro",
                UsuarioEmail = ObterUsuarioSession (),
                UsuarioNome = ObterUsuarioNomeSession ()
        });
    }

    public IActionResult CadastrarCliente (IFormCollection form) {
        try {
            Cliente cliente = new Cliente (
                form["cliente_nome"],
                form["cliente_email"],
                form["cliente_cpf"],
                form["cliente_telefone"],
                form["cliente_senha"]);


            cliente.TipoUsuario = (uint) TiposUsuario.CLIENTE;
            if(!string.IsNullOrEmpty(form["cliente_nome"]) && !string.IsNullOrEmpty(form["cliente_email"]) && !string.IsNullOrEmpty(form["cliente_cpf"]) && !string.IsNullOrEmpty(form["cliente_telefone"]) && !String.IsNullOrEmpty(form["cliente_senha"])) {
                clienteRepository.Inserir (cliente);

            return View ("Sucesso", new RespostaViewModel () {
                NomeView = "Cadastro",
                    UsuarioEmail = ObterUsuarioSession (),
                    UsuarioNome = ObterUsuarioNomeSession ()});
            } else {
                return View ("Erro", new RespostaViewModel() {
                    NomeView = "Cadastro",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()
                });
            }

        } catch (Exception e) {
            System.Console.WriteLine (e.StackTrace);
            return View ("Erro", new RespostaViewModel () {
                NomeView = "Cadastro",
                    UsuarioEmail = ObterUsuarioSession (),
                    UsuarioNome = ObterUsuarioNomeSession ()
            });
        }
    }

    

}