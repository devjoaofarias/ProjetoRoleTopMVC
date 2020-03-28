using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.Enums;
using RoleTopMVC.Models;
using RoleTopMVC.Repositories;
using RoleTopMVC.ViewModels;


namespace RoleTopMVC.Controllers {
    public class PedidoController : AbstractController {
     
        AluguelRepository aluguelRepository = new AluguelRepository();
        TipoEventoRepository tipoEventoRepository = new TipoEventoRepository();
        TipoPacoteRepository tipoPacoteRepository = new TipoPacoteRepository();
        PublicoPrivadoRepository publicoPrivadoRepository = new PublicoPrivadoRepository();
        ClienteRepository clienteRepository = new ClienteRepository();

        public IActionResult Pagamento () {
            FormaPagamentoViewModel fpv = new FormaPagamentoViewModel();
            fpv.tipoEvento = tipoEventoRepository.ObterTodos();
            fpv.tipoPacote = tipoPacoteRepository.ObterTodos();
            fpv.publicoPrivado = publicoPrivadoRepository.ObterTodos();
            
            var usuarioLogado = ObterUsuarioSession();
            var nomeUsuarioLogado = ObterUsuarioNomeSession();
            if (!string.IsNullOrEmpty(nomeUsuarioLogado))
            {
                fpv.UsuarioNome = nomeUsuarioLogado;
                var clienteLogado = clienteRepository.ObterPor(usuarioLogado);
                fpv.cliente = clienteLogado;
            }
            else
            {
                return View(fpv);
            }

            fpv.NomeView="Pagamento";
            fpv.UsuarioEmail = usuarioLogado;
            fpv.UsuarioNome = nomeUsuarioLogado;

            return View(fpv);

        }

        public IActionResult CadastrarAluguel (IFormCollection form) {

            FormaPagamento formaPagamento = new FormaPagamento () {
                NumeroCartao = form["numeroCartao"],
                NomeCartao = form["nomeCartao"],
                Email = form["email"],
                Telefone = form["telefone"],
                Cvv = form["Cvv"],
                DataValidade = DateTime.Parse (form["data-validade"]),
                DataEvento = DateTime.Parse (form["data-evento"]),
                tipoEvento = form["tipoEvento"],
                publicoPrivado = form["publicoPrivado"],
                tipoPacote = form["tipoPacote"],
                
            };
            if (aluguelRepository.Inserir(formaPagamento)) {
                return View ("Sucesso", new RespostaViewModel ("Pedido Concluido"));
            } else if (string.IsNullOrEmpty(form["numeroCartao"]) || string.IsNullOrEmpty(form["nomeCartao"]) || string.IsNullOrEmpty(form["Cvv"]) || string.IsNullOrEmpty(form["dataValidade"])) {
                return View ("Erro", new RespostaViewModel ("Pedido não realizado! Que pena"));
            } else {
                return View ("Erro", new RespostaViewModel ("Pedido não realizado! Que pena"));
            }
        }

         public IActionResult Aprovar(ulong id)
        {
            var aluguel = aluguelRepository.ObterPor(id);
            aluguel.Status = (uint) StatusAluguel.APROVADO;

            
            if(aluguelRepository.Atualizar(aluguel))
            {
                return RedirectToAction("Administrador", "Administrador");
                
            }
            else
            {
                return View("Erro", new RespostaViewModel("Não foi possível aprovar este pedido")
                {
                    NomeView = "Administrador",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()
                });
            }

        }



         public IActionResult Reprovar(ulong id)
        {
            var aluguel = aluguelRepository.ObterPor(id);
            aluguel.Status = (uint)StatusAluguel.REPROVADO;

            if(aluguelRepository.Atualizar(aluguel))
            {
                return RedirectToAction("Administrador", "Administrador");
            }
            else
            {
                return View("Erro", new RespostaViewModel("Não foi possível reprovar este pedido")
                {
                    NomeView = "administrador",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()
                });
            }

        }

            



        

    }

}