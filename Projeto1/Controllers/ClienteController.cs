using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Projeto1.Controllers
{
    public class ClienteController : Controller
    {
        private readonly BdContext _context;
        public ClienteController(BdContext context)
        {
            _context = context;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.Include(i => i.Cidades).OrderBy(cd => cd.Nome).ToListAsync());
        }

        //MétodoGet
        [HttpGet]
        public IActionResult Create()
        {
            var cidade = _context.Cidades.OrderBy(i => i.Nome).ToList();
            cidade.Insert(0, new Cidade()
            {
                CidaID = 0,
                Nome = "Selecione a Cidade"
            });
            ViewBag.Cidades = cidade;
            return View();
    }
    #endregion



    //MétodoPost
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cliente c)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Add(c);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError("", "Não foi possível inserir os dados");
        }

        return View(c);
    }

    //Criação do delete através do Get
    [HttpGet]
    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var cliente = await _context.Clientes.SingleOrDefaultAsync(c => c.ClienteID == id);
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente);
    }

    //Criação do delete através do Post
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long? id)
    {
        var cliente = await _context.Clientes.SingleOrDefaultAsync(c => c.ClienteID == id);//Criação do objeto de conexão com o BD
        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index)); //envia pro index que cadastro do cliente foi deletado
    }

    //Criação do Edit quando for o método Get
    [HttpGet]
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var cliente = await _context.Clientes.SingleOrDefaultAsync(c => c.ClienteID == id); //Criação do objeto de  conexão com o BD
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente);
    }

    //Se o Usuário já estiver sido cadastrado
    private bool ClienteExists(long? id)
    {
        return _context.Clientes.Any(e => e.ClienteID == id);
    }


    //Criação do Edit quando for o método Post
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Edit(long? id, [Bind("ClienteID, Nome, Endereço, Email, Telefone")] Cliente pes) //esse pes é mto importante
    {
        // tem q sempre referenciar o objeto(Nesse caso, ele é a variável pes) q vai efetivamente fazer as alteraçõe no BD

        if (id != pes.ClienteID)// se os ids do banco e do informados forem diferentes, apresenta erro 404
        {
            return NotFound();
        }
        if (ModelState.IsValid) //Confere se os Caracteres tão de boa.
        {
            try
            {
                _context.Update(pes);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!ClienteExists(pes.ClienteID))//se o id pessoas já existir, apresenta o erro 404 tbm
                {
                    return NotFound();
                }

                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(pes);
    }

    //Criação do Detais pelo Get
    [HttpGet]

    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var cliente = await _context.Clientes.SingleOrDefaultAsync(c => c.ClienteID == id); //Criação do objeto de  conexão com o BD
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente); // envia pro BD(n sei se é realmente enviar), e outra coisa, n precisa fazer esse mesmo processo com o método post, pois a view Details não tem uma propriedade de alteração, só tem de visualização. Tornando assim desnesessário a criação dela pelo método Post
    }


}
}
