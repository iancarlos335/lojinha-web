using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto1.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto1.Controllers
{
    public class CidadeController : Controller
    {

        private readonly BdContext _context;
        public CidadeController(BdContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Cidades.ToListAsync());
        }

        //MétodoGet
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //MétodoPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cidade ci)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(ci);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados");
            }

            return View(ci);
        }

        
        //Criação do delete através do Get
        [HttpGet]
        public async Task<IActionResult> Delete(long? id) //tenho a impressão de q esse id q eu sempre uso na vdd é da outra model(Cliente) pois ele foi declarado como um elemento "public"
        {
            if (id == null)
            {
                return NotFound();
            }
            var cidade = await _context.Cidades.SingleOrDefaultAsync(ci => ci.CidaID == id);
            if (cidade == null)
            {
                return NotFound();
            }
            return View(cidade);
        }

        //Criação do delete através do Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var cidade = await _context.Cidades.SingleOrDefaultAsync(ci => ci.CidaID == id);//Criação do objeto de conexão com o BD
            _context.Cidades.Remove(cidade);
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
            var cidade = await _context.Cidades.SingleOrDefaultAsync(ci => ci.CidaID == id); //Criação do objeto de  conexão com o BD
            if (cidade == null)
            {
                return NotFound();
            }
            return View(cidade);
        }

        //Acondicional no meio do código, no começo esse tipo de declaração é comum, mas aqui no meio do código...
        //Se o Usuário já estiver sido cadastrado
        private bool CidadeExists(long? id)
        {
            return _context.Cidades.Any(e => e.CidaID == id); //olha isso direito pelo amor
        }


        //Criação do Edit quando for o método Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(long? id, [Bind("CidadeID, Nome, Sigla")] Cidade cid) //esse cid é mto importante
        {
            // tem q sempre referenciar o objeto(Nesse caso, ele é a variável pes) q vai efetivamente fazer as alteraçõe no BD

            if (id != cid.CidaID)// se os ids do banco e do informados forem diferentes, apresenta erro 404
            {
                return NotFound();
            }
            if (ModelState.IsValid) //Confere se os Caracteres tão de boa.
            {
                try
                {
                    _context.Update(cid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!CidadeExists(cid.CidaID))//se o id pessoas já existir, apresenta o erro 404 tbm
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
            return View(cid);
        }

        //Criação do Detais pelo Get
        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cidade = await _context.Cidades.SingleOrDefaultAsync(ci => ci.CidaID == id); //Criação do objeto de  conexão com o BD
            if (cidade == null)
            {
                return NotFound();
            }
            return View(cidade); // envia pro BD(n sei se é realmente enviar), e outra coisa, n precisa fazer esse mesmo processo com o método post, pois a view Details não tem uma propriedade de alteração, só tem de visualização. Tornando assim desnesessário a criação dela pelo método Post
        }

    }
}
