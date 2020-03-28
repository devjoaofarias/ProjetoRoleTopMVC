using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.Controllers;
using RoleTopMVC.Enums;
using RoleTopMVC.ViewModels;
using RoleTopMVC.Repositories;

public class AdministradorController : AbstractController {
    AluguelRepository aluguelRepository = new AluguelRepository();
    public IActionResult Administrador () {
        var ninguemLogado = string.IsNullOrEmpty(ObterUsuarioTipoSession());

            if (!ninguemLogado && 
            (uint) TiposUsuario.ADMINISTRADOR == uint.Parse(ObterUsuarioTipoSession())) {

                var alugueis = aluguelRepository.ObterTodos ();
                AdministradorViewModel administradorViewModel = new AdministradorViewModel ();

                foreach (var aluguel in alugueis) {
                    switch (aluguel.Status) {
                        case (uint) StatusAluguel.APROVADO:
                            administradorViewModel.AlugueisAprovados++;
                            break;
                        case (uint) StatusAluguel.REPROVADO:
                            administradorViewModel.AlugueisReprovados++;
                            break;
                        default:
                            administradorViewModel.AlugueisPendentes++;
                            administradorViewModel.Alugueis.Add (aluguel);
                            break;
                    }
                }
                administradorViewModel.NomeView = "Administrador";
                administradorViewModel.UsuarioEmail = ObterUsuarioSession ();

                return View (administradorViewModel);
            } 
            else 
            {
                return View ("Erro", new RespostaViewModel(){
                    NomeView = "Administrador",
                    Mensagem = "Você não tem permissão para acessar o Dashboard"
                });

            }
        }
    }
