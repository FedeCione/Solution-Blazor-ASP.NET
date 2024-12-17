using System.Net.Http.Json;
using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly HttpClient _http;

        public WorkerService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<WorkerDTO>> List()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<WorkerDTO>>>("api/Worker/List");

            if (result!.IsCorrect)
            {
                return result.Value!;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        public async Task<WorkerDTO> Search(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<WorkerDTO>>($"api/Worker/Search/{id}");

            if (result!.IsCorrect)
            {
                return result.Value!;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        public async Task<int> Create(WorkerDTO worker)
        {
            var result = await _http.PostAsJsonAsync("api/Worker/Create", worker);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.IsCorrect)
            {
                return response.Value!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<int> Update(WorkerDTO worker)
        {
            var result = await _http.PutAsJsonAsync($"api/Worker/Update/{worker.IdWorker}", worker);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.IsCorrect)
            {
                return response.Value!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _http.DeleteAsync($"api/Worker/Delete/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.IsCorrect)
            {
                return response.IsCorrect!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }
    }
}
