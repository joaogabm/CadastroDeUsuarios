using CadastroDeUsuarios.Data;
using CadastroDeUsuarios.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeUsuarios.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataAcess _dataAcess;
        public HomeController(DataAcess dataAcess)
        {
            _dataAcess = dataAcess;
        }

        public IActionResult Index()
        {
            try
            {
                var usuarios = _dataAcess.ListarUsuarios();
                return View(usuarios);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Ocorreu um erro na criação do usuário!";
                return View();
            }
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            var usuario = _dataAcess.BuscarUsuarioPorId(id);

            return View(usuario);
        }

        public IActionResult Detalhes(int id)
        {
            var usuario = _dataAcess.BuscarUsuarioPorId(id);
            return View(usuario);
        }

        public IActionResult Remover(int id)
        {
            var result = _dataAcess.Remover(id);

            if(result)
            {
                TempData["MensagemSucesso"] = "Usuário removido com sucesso!";
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro na remoção do usuário!";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var result = _dataAcess.Cadastrar(usuario);

                if (result)
                {
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Ocorreu um erro na criação do usuário!";
                    return View(usuario);
                }
            }
            else
            {
                return View(usuario);
            }
        }

        [HttpPost]
        public IActionResult Editar(Usuario usuario)
        {
            if (ModelState.IsValid) 
            {
                var result = _dataAcess.Editar(usuario);

                if(result)
                {
                    TempData["MensagemSucesso"] = "Usuário editado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Ocorreu um erro na edição do usuário!";
                    return View(usuario);
                }
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro na edição do usuário!";
                return View(usuario);
            }
        }
    }
}
