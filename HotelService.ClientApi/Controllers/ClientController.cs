using HotelService.ClientApi.Entities;
using HotelService.ClientApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.ClientApi.Controllers
{
    [ApiController]
    [Route("client")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Zwraca listę wszystkich klientów.
        /// </summary>
        /// <returns>Lista obiektów typu Client.</returns>
        [HttpGet]
        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _clientService.Get();
        }

        /// <summary>
        /// Pobiera klienta na podstawie identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator klienta.</param>
        /// <returns>Obiekt Client lub 404 jeśli nie znaleziono.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _clientService.GetById(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        /// <summary>
        /// Tworzy nowego klienta.
        /// </summary>
        /// <param name="dto">Dane klienta do utworzenia.</param>
        /// <returns>Kod 200 po sukcesie lub 400 jeśli dane są nieprawidłowe.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Client dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _clientService.Add(dto);
            return Ok();
        }

        /// <summary>
        /// Usuwa klienta na podstawie identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator klienta do usunięcia.</param>
        /// <returns>Kod 204 jeśli usunięto lub 404 jeśli nie znaleziono.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _clientService.GetById(id);

            if (client == null)
            {
                return NotFound();
            }

            await _clientService.Delete(id);
            return NoContent();
        }

        /// <summary>
        /// Aktualizuje dane klienta (pełna aktualizacja).
        /// </summary>
        /// <param name="id">Identyfikator klienta.</param>
        /// <param name="updatedClient">Zaktualizowany obiekt klienta.</param>
        /// <returns>Kod 200 po sukcesie lub 404 jeśli nie znaleziono.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Client updatedClient)
        {
            var existingClient = await _clientService.GetById(id);

            if (existingClient == null)
            {
                return NotFound();
            }

            existingClient.FirstName = updatedClient.FirstName;
            existingClient.LastName = updatedClient.LastName;
            existingClient.Email = updatedClient.Email;
            existingClient.PhoneNumber = updatedClient.PhoneNumber;

            await _clientService.Update(existingClient);
            return Ok(existingClient);
        }

        /// <summary>
        /// Częściowo aktualizuje dane klienta (np. tylko e-mail lub numer telefonu).
        /// </summary>
        /// <param name="id">Identyfikator klienta.</param>
        /// <param name="patchDoc">Dokument zawierający operacje do wykonania.</param>
        /// <returns>Kod 200 z obiektem po aktualizacji lub 404 jeśli nie znaleziono.</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Client> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var client = await _clientService.GetById(id);
            if (client == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(client, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _clientService.Update(client);
            return Ok(client);
        }
    }
}
