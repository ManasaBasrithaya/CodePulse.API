using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController(ICategoryRepository _categoryRepository)
        {
           categoryRepository = _categoryRepository;
        }

        private readonly ICategoryRepository categoryRepository;

        //
        [HttpPost]
        public async Task<IActionResult> CreateCategory (CreateCategoryRequestDto request)
        {
            //Now map the dto to domain model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await categoryRepository.CreateAsync(category);

             // to return we need to map the domain to DTO again for security
             var response = new CategoryDto
            {
                Id = category.Id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            
            return Ok(response);
        }

        //GET:/api/categories

        [HttpGet]

        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();
            //Map domain model to dto
            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }
            return Ok(response);
        }

        //GET:
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid Id)
        {
            var existingCategory = await categoryRepository.GetCategoryById(Id);
            if (existingCategory == null)
                return NotFound();
            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };
            return Ok(response);
        }



        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryRequestDto request)
        {
            //convert dto to domain model
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle=request.UrlHandle

            };
            var updatedCategory = await categoryRepository.UpdateCategoryById(category);
            if(updatedCategory ==null)
            {
                return NotFound();
            }

            //convert domain to dto
            var response = new CategoryDto
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                UrlHandle = updatedCategory.UrlHandle
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategoryById([FromRoute] Guid id)
        {
            var deletedCategory = await categoryRepository.DeleteCategoryById(id);
            if(deletedCategory is null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                Id = deletedCategory.Id,
                Name = deletedCategory.Name,
                UrlHandle = deletedCategory.UrlHandle
            };
            return Ok(response);
        }
    }
}
